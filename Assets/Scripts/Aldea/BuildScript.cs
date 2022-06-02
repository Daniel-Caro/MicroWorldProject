using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using TMPro;
public class BuildScript : MonoBehaviour//, IClick
{
    public GameObject panel;
    public GameObject thisBuilding;
    private GameObject building;
    public int id;
    public string type;
    public int level;
    public int cost;
    public double factor;
    public AudioSource coinSound;
    public AudioSource minionSound;
    private GameObject infoText;
    private GameObject lvlUpButton; 
    public AudioSource lvlUpSound;
    public AudioSource noSound;
    public BankProduction bankProduction;
    public MinionProduction minionProduction;
    private Coroutine lastRoutine = null;
    private GameObject buildingPanel;
    private GameObject factoryPanel;
    private GameObject housePanel;
    private GameObject infoPanel;
    private GameObject savePanel;
    private GameObject settingsPanel;
    private bool isPressed;
    void Start() {
        GameObject uiuiuiui = GameObject.Find("UI").gameObject;
        buildingPanel = FindObject(uiuiuiui,"InfoBuildingPanel").gameObject;
        factoryPanel = FindObject(uiuiuiui,"InfoMinionPanel").gameObject;
        housePanel = FindObject(uiuiuiui,"InfoHousePanel").gameObject;
        infoPanel = FindObject(uiuiuiui, "InfoPanel").gameObject;
        savePanel = FindObject(uiuiuiui, "SaveGame").gameObject;
        settingsPanel = FindObject(uiuiuiui, "SettingsPanel").gameObject;
    }

    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            //Debug.Log(hit.collider.gameObject.transform.parent.gameObject.GetComponent<BuildScript>().id);
            if (hit.collider != null && !buildingPanel.activeSelf && !factoryPanel.activeSelf && !housePanel.activeSelf && !infoPanel.activeSelf && !settingsPanel.activeSelf && !savePanel.activeSelf && (Globals.tutorialStep>=14 || Globals.tutorialStep==10 || Globals.tutorialStep==11))
            {
                if(hit.collider.gameObject.tag == "building"){
                    isPressed = true;
                    building = hit.collider.gameObject.transform.parent.gameObject;
                    if (building.GetComponent<BuildScript>().id != thisBuilding.GetComponent<BuildScript>().id) return;
                    type = Globals.buildingDataDic[building.GetComponent<BuildScript>().id]["Type"];
                    level = Int32.Parse(Globals.buildingDataDic[building.GetComponent<BuildScript>().id]["Level"]);
                    if (level!=10) cost = Globals.buildingCostsDic[type].ElementAt(level);
                    if (type == "Bank"){
                        if (bankProduction.HarvestResource(building)){
                            if (Globals.bankDataDic[building.GetComponent<BuildScript>().id]["Accumulated"] == 0) building.transform.Find("money").gameObject.SetActive(false);
                            coinSound.Play();

                            Debug.Log("Comienza la rutina");
                            lastRoutine = StartCoroutine(HoldTimer());
                        } else showPanel();
                    }else if (type == "Factory"){
                        if(minionProduction.HarvestResource(building) == true){
                            building.transform.Find("pngwing.com (2)").gameObject.SetActive(false);
                            Dictionary<int,int> buildingFactory = Globals.factoryDataDic[building.GetComponent<BuildScript>().id];
                            if(Globals.houseDataDic.Count == 0  ){
                                StartCoroutine(Globals.popInfoMessage("No hay casas aun construidas"));
                                Debug.Log("No hay casas aun construidas");
                            }else{
                                int totalNivelCasas = 0;
                                int totalMinions = 0;
                                foreach(KeyValuePair<int,int> kv4 in Globals.houseDataDic){
                                    totalNivelCasas += kv4.Value;
                                }
                                foreach(KeyValuePair<int,int> kv5 in Globals.minionsQuantity){
                                    totalMinions += kv5.Value;
                                }
                                Debug.Log(totalNivelCasas+" lll "+totalMinions);
                                for(int i = 1; i < buildingFactory.Count+1; i++){
                                    if(totalNivelCasas + Globals.minionCapacity >= totalMinions){
                                        if(Globals.minionsQuantity.ContainsKey(i)){
                                            Globals.minionsQuantity[i] += buildingFactory[i];
                                            totalMinions += buildingFactory[i];
                                        }
                                        else{
                                            Globals.minionsQuantity.Add(i,buildingFactory[i]);
                                            totalMinions+= buildingFactory[i];
                                        } 
                                        Globals.factoryDataDic[building.GetComponent<BuildScript>().id][i] -= buildingFactory[i];
                                        coinSound.Play();
                                        Debug.Log("Se recoge el minion");
                                    }else{
                                        StartCoroutine(Globals.popInfoMessage("No tienes espacio para recoger esta cantidad de minions"));
                                        Debug.Log("No tienes espacio para recoger esta cantidad de minions");
                                    }
                                        
                                }
                                
                                }
                                
                            }else showPanelMinion();
                            //minionSoung.Play();
                            
                            lastRoutine = StartCoroutine(HoldTimer());
                        }else if (type == "House"){
                        showPanelInfoMinion(building); 
                        }else showPanel();
                    }   
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isPressed = false;
        }
        
    }
    private int getKeyByValue(Dictionary<int,int> value, Dictionary<int ,Dictionary<int,int>> dictionary){

        foreach (int keyVar in dictionary.Keys){
            if (dictionary[keyVar] == value)
            {
                return keyVar;
            }
        }
        return 0;

    }
    public static GameObject getObjectById(int id)
    {
        Dictionary<int, GameObject> m_instanceMap = new Dictionary<int, GameObject>();
        //record instance map

        m_instanceMap.Clear();
        List<GameObject> gos = new List<GameObject>();
        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            if (gos.Contains(go))
            {
                continue;
            }
            gos.Add(go);
            m_instanceMap[go.GetInstanceID()] = go;
        }

        if (m_instanceMap.ContainsKey(id))
        {
            return m_instanceMap[id];
        }
        else
        {
            return null;
        }
    }
       
    IEnumerator HoldTimer()
    {
        yield return new WaitForSeconds(1);
        if (isPressed) showPanel();

    }

    public void showPanel(){
        infoText = panel.transform.Find("InfoText").gameObject;
        switch(type){
            case("TownHall"):
                if (Globals.tutorialStep == 10) TutorialScript.townHallExplain(panel.transform.Find("ClosePanel").gameObject, panel.transform.Find("MinigamesButton").gameObject);
                infoText.GetComponent<TextMeshProUGUI>().text = "Ayuntamiento nivel: " + level.ToString();
                panel.transform.Find("MinigamesButton").gameObject.SetActive(true);
                break;
            case("House"):
                infoText.GetComponent<TextMeshProUGUI>().text = "Casa nivel: " + level.ToString();
                panel.transform.Find("MinigamesButton").gameObject.SetActive(false);
                break;
            case("Bank"):
                infoText.GetComponent<TextMeshProUGUI>().text = "Banco nivel: " + level.ToString();
                panel.transform.Find("MinigamesButton").gameObject.SetActive(false);
                break;
            case("Factory"):
                infoText.GetComponent<TextMeshProUGUI>().text = "Fábrica nivel: " + level.ToString();
                break;
        }
        
        lvlUpButton = panel.transform.Find("LevelUpButton").gameObject;
        GameObject buttonText = lvlUpButton.transform.Find("LevelUpText").gameObject;
        lvlUpButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        if(level >= 10)
        {
            buttonText.GetComponent<TextMeshProUGUI>().text = "Nivel Máximo";
        }
        else
        {
            lvlUpButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
                AdditionalPanels();
            });
            buttonText.GetComponent<TextMeshProUGUI>().text = "Subir de nivel\n" + cost.ToString();
        }
        panel.SetActive(true);
    }
    public void showPanelMinion(){
        infoText = panel.transform.Find("InfoText").gameObject;
        infoText.GetComponent<TextMeshProUGUI>().text = "Fábrica nivel: " + level.ToString();
        lvlUpButton = panel.transform.Find("LevelUpButton").gameObject;
        GameObject buttonText = lvlUpButton.transform.Find("LevelUpText").gameObject;
        buttonText.GetComponent<TextMeshProUGUI>().text = "Subir de nivel\n" + cost.ToString();
        GameObject choosePanelMinion =panel.transform.Find("ChoosingMinion").gameObject;
        if(Globals.style == Style.Princess){
            choosePanelMinion.transform.Find("minionspirata").gameObject.SetActive(false);
            choosePanelMinion.transform.Find("minionsfuture").gameObject.SetActive(false);
        }else if(Globals.style == Style.Pirate){
            choosePanelMinion.transform.Find("minionsprincesa").gameObject.SetActive(false);
            choosePanelMinion.transform.Find("minionsfuture").gameObject.SetActive(false);
        }else if(Globals.style == Style.Future){
            choosePanelMinion.transform.Find("minionspirata").gameObject.SetActive(false);
            choosePanelMinion.transform.Find("minionsprincesa").gameObject.SetActive(false);
        }
        GameObject groupMinion1 = null;
        GameObject tier1button = null;
        if(Globals.style == Style.Princess){
            groupMinion1 = choosePanelMinion.transform.Find("minionsprincesa").gameObject;
            tier1button = groupMinion1.transform.Find("minionprincesa").gameObject;
            GameObject text1button = tier1button.transform.Find("Text").gameObject;
            text1button.GetComponent<TextMeshProUGUI>().text = "Plebeyo: " + 10; 
        }else if(Globals.style == Style.Pirate){
            groupMinion1 = choosePanelMinion.transform.Find("minionspirata").gameObject;
            tier1button = groupMinion1.transform.Find("minionpirata").gameObject;
            GameObject text1button = tier1button.transform.Find("Text").gameObject;
            text1button.GetComponent<TextMeshProUGUI>().text = "Mono:  " + 10; 
        }else if(Globals.style == Style.Future){
            groupMinion1 = choosePanelMinion.transform.Find("minionsfuture").gameObject;
            tier1button = groupMinion1.transform.Find("minionfuture").gameObject;
            GameObject text1button = tier1button.transform.Find("Text").gameObject;
            text1button.GetComponent<TextMeshProUGUI>().text = "Robot: " + 10; 
        }
        
        lvlUpButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        lvlUpButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { 
            AdditionalPanels();
        });
        
        
        tier1button.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        tier1button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
            if(Globals.gameResources["Coins"].currentR >= 10){
                Globals.gameResources["Coins"].currentR -= 10;
                minionProduction.Produce(building, "Tier1");
            }else{
                StartCoroutine(Globals.popInfoMessage("No tienes recursos/nivel de fabrica suficientes para fabricar este minion"));
                Debug.Log("No tienes recursos/nivel de fabrica suficientes para fabricar este minion tier 1");
            }
            
        });
        GameObject groupMinion2= null;
        GameObject tier2button = null;
        if(Globals.style == Style.Princess){
            groupMinion2 = choosePanelMinion.transform.Find("minionsprincesa").gameObject;
            tier2button = groupMinion2.transform.Find("minionprincesa2").gameObject;
            GameObject text2button = tier2button.transform.Find("Text").gameObject;
            text2button.GetComponent<TextMeshProUGUI>().text = "Curandero: " + 30; 
        }else if(Globals.style == Style.Pirate){
            groupMinion2 = choosePanelMinion.transform.Find("minionspirata").gameObject;
            tier2button = groupMinion2.transform.Find("minionpirata2").gameObject;
            GameObject text2button = tier2button.transform.Find("Text").gameObject;
            text2button.GetComponent<TextMeshProUGUI>().text = "Mono con flotador: " + 30;
        }else if(Globals.style == Style.Future){
            groupMinion2 = choosePanelMinion.transform.Find("minionsfuture").gameObject;
            tier2button = groupMinion2.transform.Find("minionfuture2").gameObject;
            GameObject text2button = tier2button.transform.Find("Text").gameObject;
            text2button.GetComponent<TextMeshProUGUI>().text = "Cyborg: " + 30;
        }
        tier2button.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        tier2button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
            if(Globals.gameResources["Coins"].currentR >= 30&& Int32.Parse(Globals.buildingDataDic[building.GetComponent<BuildScript>().id]["Level"]) >= 4 && minionProduction.storageComplete() == false){
                Globals.gameResources["Coins"].currentR -= 30;
                minionProduction.Produce(building, "Tier2");
            }else{
                StartCoroutine(Globals.popInfoMessage("No tienes recursos/nivel de fabrica suficientes para fabricar este minion"));
                Debug.Log("No tienes recursos/nivel de fabrica suficientes para fabricar este minion tier 2");
            }
        });
        GameObject groupMinion3= null;
        GameObject tier3button = null;
        if(Globals.style == Style.Princess){
            groupMinion3 = choosePanelMinion.transform.Find("minionsprincesa").gameObject;
            tier3button = groupMinion3.transform.Find("minionprincesa3").gameObject;
            GameObject text3button = tier3button.transform.Find("Text").gameObject;
            text3button.GetComponent<TextMeshProUGUI>().text = "Noble: " + 30; 
        }else if(Globals.style == Style.Pirate){
            groupMinion3 = choosePanelMinion.transform.Find("minionspirata").gameObject;
            tier3button = groupMinion3.transform.Find("minionpirata3").gameObject;
            GameObject text3button = tier3button.transform.Find("Text").gameObject;
            text3button.GetComponent<TextMeshProUGUI>().text = "Mono con pistolas: " + 30; 
        }else if(Globals.style == Style.Future){
            groupMinion3 = choosePanelMinion.transform.Find("minionsfuture").gameObject;
            tier3button = groupMinion3.transform.Find("minionfuture3").gameObject;
            GameObject text3button = tier3button.transform.Find("Text").gameObject;
            text3button.GetComponent<TextMeshProUGUI>().text = "Cryptobro: " + 30; 
        }
        
        tier3button.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        tier3button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
            if(Globals.gameResources["Coins"].currentR >= 30 && Int32.Parse(Globals.buildingDataDic[building.GetComponent<BuildScript>().id]["Level"]) >= 7 && minionProduction.storageComplete() == false){
                Globals.gameResources["Coins"].currentR -= 30;
                minionProduction.Produce(building, "Tier3");
            }else{
                StartCoroutine(Globals.popInfoMessage("No tienes recursos/nivel de fabrica suficientes para fabricar este minion"));
                Debug.Log("No tienes recursos/nivel de fabrica suficientes para fabricar este minion tier 3");
            }
        });
        GameObject groupMinion4= null;
        GameObject tier4button = null;
        if(Globals.style == Style.Princess){
            groupMinion4 = choosePanelMinion.transform.Find("minionsprincesa").gameObject;
            tier4button = groupMinion4.transform.Find("minionprincesa4").gameObject;
            GameObject text4button = tier4button.transform.Find("Text").gameObject;
            text4button.GetComponent<TextMeshProUGUI>().text = "Principe: " + 50; 
        }else if(Globals.style == Style.Pirate){
            groupMinion4 = choosePanelMinion.transform.Find("minionspirata").gameObject;
            tier4button = groupMinion4.transform.Find("minionpirata4").gameObject;
            GameObject text4button = tier4button.transform.Find("Text").gameObject;
            text4button.GetComponent<TextMeshProUGUI>().text = "Rey Mono: " + 50; 
        }else if(Globals.style == Style.Future){
            groupMinion4 = choosePanelMinion.transform.Find("minionsfuture").gameObject;
            tier4button = groupMinion4.transform.Find("minionfuture4").gameObject;
            GameObject text4button = tier4button.transform.Find("Text").gameObject;
            text4button.GetComponent<TextMeshProUGUI>().text = "Marc Sukenberg; " + 50; 
        }
        tier4button.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        tier4button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
             if(Globals.gameResources["Coins"].currentR >= 50 && Int32.Parse(Globals.buildingDataDic[building.GetComponent<BuildScript>().id]["Level"]) >= 10 && minionProduction.storageComplete() == false){
                Globals.gameResources["Coins"].currentR -= 50;
                minionProduction.Produce(building, "Tier4");
            }else{
                StartCoroutine(Globals.popInfoMessage("No tienes recursos/nivel de fabrica suficientes para fabricar este minion"));
                Debug.Log("No tienes recursos/nivel de fabrica suficientes para fabricar este minion tier 4");
            }
        });
        panel.SetActive(true);
    }
    public void showPanelInfoMinion(GameObject bulding){
        infoText = panel.transform.Find("InfoText").gameObject;
        infoText.GetComponent<TextMeshProUGUI>().text = "Casa nivel: " + level.ToString();
        lvlUpButton = panel.transform.Find("LevelUpButton").gameObject;
        GameObject buttonText = lvlUpButton.transform.Find("LevelUpText").gameObject;
        buttonText.GetComponent<TextMeshProUGUI>().text = "Subir de nivel\n" + cost.ToString();
        lvlUpButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        lvlUpButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
            
            AdditionalPanels();
        });
        
        //Globals.minionsQuantity[1];
        GameObject minionInfoHouse = panel.transform.Find("ChoosingMinion").gameObject;
        GameObject qminion1 = null;
        GameObject qminion2 = null;
        GameObject qminion3 = null;
        GameObject qminion4 = null;
        if(Globals.style == Style.Princess){
            qminion1 = minionInfoHouse.transform.Find("qminion1").gameObject;
            qminion1.GetComponent<TextMeshProUGUI>().text = "Plebeyos: "+ Globals.minionsQuantity[1];
            qminion2 = minionInfoHouse.transform.Find("qminion2").gameObject;
            qminion2.GetComponent<TextMeshProUGUI>().text = "Curanderos: "+ Globals.minionsQuantity[2];
            qminion3 = minionInfoHouse.transform.Find("qminion3").gameObject;
            qminion3.GetComponent<TextMeshProUGUI>().text = "Nobles: "+ Globals.minionsQuantity[3];
            qminion4 = minionInfoHouse.transform.Find("qminion4").gameObject;
            qminion4.GetComponent<TextMeshProUGUI>().text = "Principes: "+ Globals.minionsQuantity[4];
        }else if(Globals.style == Style.Pirate){
            qminion1 = minionInfoHouse.transform.Find("qminion1").gameObject;
            qminion1.GetComponent<TextMeshProUGUI>().text = "Mono: "+ Globals.minionsQuantity[1];
            qminion2 = minionInfoHouse.transform.Find("qminion2").gameObject;
            qminion2.GetComponent<TextMeshProUGUI>().text = "Mono con flotador: "+ Globals.minionsQuantity[2];
            qminion3 = minionInfoHouse.transform.Find("qminion3").gameObject;
            qminion3.GetComponent<TextMeshProUGUI>().text = "Mono con pistolas: "+ Globals.minionsQuantity[3];
            qminion4 = minionInfoHouse.transform.Find("qminion4").gameObject;
            qminion4.GetComponent<TextMeshProUGUI>().text = "Rey Mono: "+ Globals.minionsQuantity[4];
        }else if(Globals.style == Style.Future){
            qminion1 = minionInfoHouse.transform.Find("qminion1").gameObject;
            qminion1.GetComponent<TextMeshProUGUI>().text = "Robot: "+ Globals.minionsQuantity[1];
            qminion2 = minionInfoHouse.transform.Find("qminion2").gameObject;
            qminion2.GetComponent<TextMeshProUGUI>().text = "Cyborg: "+ Globals.minionsQuantity[2];
            qminion3 = minionInfoHouse.transform.Find("qminion3").gameObject;
            qminion3.GetComponent<TextMeshProUGUI>().text = "Cryptobro: "+ Globals.minionsQuantity[3];
            qminion4 = minionInfoHouse.transform.Find("qminion4").gameObject;
            qminion4.GetComponent<TextMeshProUGUI>().text = "Marc Sukenberg: "+ Globals.minionsQuantity[4];
        }
        
        panel.SetActive(true);
        
        
    }
    public void AdditionalPanels() {
        if(Globals.gameResources["Coins"].currentR >= cost) {
            if (type == "TownHall") LevelUp();
            else if (level + 1 <= Int32.Parse(Globals.buildingDataDic[Globals.townHallId]["Level"]))
            {
                if (Globals.buildingDataDic[building.GetComponent<BuildScript>().id]["Type"] == "Bank") bankProduction.chooseAttribute(building, panel, this);
                else LevelUp();
            }
            else
            {
                StartCoroutine(Globals.popInfoMessage("No tienes nivel de ayuntamiento suficiente para hacer esto"));
                noSound.Play();
            }
        }
        else
        {
            noSound.Play();
        }
    }

    public void LevelUp(){
        level++;
        Globals.buildingDataDic[building.GetComponent<BuildScript>().id]["Level"] = level.ToString();
        if(Globals.buildingDataDic[building.GetComponent<BuildScript>().id]["Type"] == "House"){
            Globals.houseDataDic[building.GetComponent<BuildScript>().id] += 1;
        }
        Globals.gameResources["Coins"].DedactResources(cost);

        switch(type){
            case("TownHall"):
                infoText.GetComponent<TextMeshProUGUI>().text = "Ayuntamiento nivel: " + level.ToString();
                break;
            case("House"):
                infoText.GetComponent<TextMeshProUGUI>().text = "Casa nivel: " + level.ToString();
                break;
            case("Bank"):
                infoText.GetComponent<TextMeshProUGUI>().text = "Banco nivel: " + level.ToString();
                break;
            case("Factory"):
                infoText.GetComponent<TextMeshProUGUI>().text = "Fábrica nivel: " + level.ToString();
                break;
        }
        lvlUpSound.Play();

        lvlUpButton = panel.transform.Find("LevelUpButton").gameObject;
        GameObject buttonText = lvlUpButton.transform.Find("LevelUpText").gameObject;
        lvlUpButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        if(level >= 10)
        {
            buttonText.GetComponent<TextMeshProUGUI>().text = "Nivel Máximo";
        }
        else
        {
            cost = Globals.buildingCostsDic[type].ElementAt(level);
            lvlUpButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
                AdditionalPanels();
            });
            buttonText.GetComponent<TextMeshProUGUI>().text = "Subir de nivel\n" + cost.ToString();
        }
    }
    public  GameObject FindObject(GameObject parent, string name)
    {
        Transform[] trs= parent.GetComponentsInChildren<Transform>(true);
        foreach(Transform t in trs){
            if(t.name == name){
                return t.gameObject;
            }
        }
        return null;
    }
}
