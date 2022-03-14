using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class BuildScript : MonoBehaviour//, IClick
{

    public GameObject panel;
    public GameObject thisBuilding;
    private GameObject building;
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
    void Start() {
        GameObject uiuiuiui = GameObject.Find("UI").gameObject;
        buildingPanel = FindObject(uiuiuiui,"InfoBuildingPanel").gameObject;
        factoryPanel = FindObject(uiuiuiui,"InfoMinionPanel").gameObject;
        housePanel = FindObject(uiuiuiui,"InfoHousePanel").gameObject;
    }

    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            //Debug.Log(hit.collider.gameObject.transform.parent.gameObject.GetInstanceID());
            if (hit.collider != null && !buildingPanel.activeSelf &&!factoryPanel.activeSelf && !housePanel.activeSelf)
            {
                if(hit.collider.gameObject.tag == "building"){
                    building = hit.collider.gameObject.transform.parent.gameObject;
                    if (building.GetInstanceID() != thisBuilding.GetInstanceID()) return;
                    type = Globals.buildingDataDic[building.GetInstanceID()]["Type"];
                    level = Int32.Parse(Globals.buildingDataDic[building.GetInstanceID()]["Level"]);
                    cost = Globals.buildingCostsDic[type].ElementAt(level);
                    if (type == "Bank"){
                        if (bankProduction.HarvestResource(building)){
                            building.transform.Find("money").gameObject.SetActive(false);
                            coinSound.Play();

                            Debug.Log("Comienza la rutina");
                            lastRoutine = StartCoroutine(HoldTimer());
                        } else showPanel();
                    }else if (type == "Factory"){
                        if(minionProduction.HarvestResource(building) == true){
                            building.transform.Find("pngwing.com (2)").gameObject.SetActive(false);
                            Dictionary<int,int> buildingFactory = Globals.factoryDataDic[building.GetInstanceID()];
                            if(Globals.houseDataDic.Count == 0  ){
                                Debug.Log("No hay casas aun construidas");
                            }else{
                                bool todascompletas = minionProduction.storageComplete();
                                if(todascompletas == true){
                                    Debug.Log("Todas las casas están completas");
                                }else{
                                    
                                    for(int i = 1; i < buildingFactory.Count+1; i++){
                                        if(Globals.minionsQuantity.ContainsKey(i)){
                                            Globals.minionsQuantity[i] += buildingFactory[i];
                                        }
                                        else Globals.minionsQuantity.Add(i,buildingFactory[i]);
                                        Globals.factoryDataDic[building.GetInstanceID()][i] -= buildingFactory[i];
                                    }
                                    Debug.Log("Se recoge el minion");
                                }
                                
                            }
                            //minionSoung.Play();
                            
                            lastRoutine = StartCoroutine(HoldTimer());
                        } else showPanelMinion();
                        
                    }else if (type == "House"){
                        showPanelInfoMinion(building);
                    }
                    else showPanel();
                   
                    
                }
            }
        }
    }

    IEnumerator HoldTimer()
    {
        yield return new WaitForSeconds(1);
        showPanel();
    }

    public void showPanel(){
        infoText = panel.transform.Find("InfoText").gameObject;
        switch(type){
            case("TownHall"):
                infoText.GetComponent<UnityEngine.UI.Text>().text = "Ayuntamiento nivel: " + level.ToString();
                break;
            case("House"):
                infoText.GetComponent<UnityEngine.UI.Text>().text = "Casa nivel: " + level.ToString();
                break;
            case("Bank"):
                infoText.GetComponent<UnityEngine.UI.Text>().text = "Banco nivel: " + level.ToString();
                break;
            case("Factory"):
                infoText.GetComponent<UnityEngine.UI.Text>().text = "Fábrica nivel: " + level.ToString();
                break;
        }
        
        lvlUpButton = panel.transform.Find("LevelUpButton").gameObject;
        GameObject buttonText = lvlUpButton.transform.Find("LevelUpText").gameObject;
        lvlUpButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        if(level >= 10)
        {
            buttonText.GetComponent<UnityEngine.UI.Text>().text = "Nivel Máximo";
        }
        else
        {
            lvlUpButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
                AdditionalPanels();
            });
            buttonText.GetComponent<UnityEngine.UI.Text>().text = "Subir de nivel\n" + cost.ToString();
        }
        panel.SetActive(true);
    }
    public void showPanelMinion(){
        infoText = panel.transform.Find("InfoText").gameObject;
        infoText.GetComponent<UnityEngine.UI.Text>().text = "Fabrica nivel: " + level.ToString();
        lvlUpButton = panel.transform.Find("LevelUpButton").gameObject;
        GameObject buttonText = lvlUpButton.transform.Find("LevelUpText").gameObject;
        buttonText.GetComponent<UnityEngine.UI.Text>().text = "Subir de nivel\n" + cost.ToString();
        GameObject choosePanelMinion =panel.transform.Find("ChoosingMinion").gameObject;
        GameObject tier1button = choosePanelMinion.transform.Find("miniontier1").gameObject;
        GameObject text1button = tier1button.transform.Find("Text").gameObject;
        lvlUpButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        lvlUpButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { 
            LevelUp();
        });
        
        text1button.GetComponent<UnityEngine.UI.Text>().text = "Tier 1: " + 250; 
        tier1button.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        tier1button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
            if(Globals.gameResources["Coins"].currentR >= 250 && minionProduction.storageComplete() == false){
                Globals.gameResources["Coins"].currentR -= 250;
                minionProduction.Produce(building, "Tier1");
            }else{
                Debug.Log("No tienes recursos/nivel de fabrica suficientes para fabricar este minion tier 1");
            }
            
        });
        GameObject tier2button = choosePanelMinion.transform.Find("miniontier2").gameObject;
        GameObject text2button = tier2button.transform.Find("Text").gameObject;
        text2button.GetComponent<UnityEngine.UI.Text>().text = "Tier 2: " + 500; 
        tier2button.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        tier2button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
            if(Globals.gameResources["Coins"].currentR >= 500&& Int32.Parse(Globals.buildingDataDic[building.GetInstanceID()]["Level"]) >= 4 && minionProduction.storageComplete() == false){
                Globals.gameResources["Coins"].currentR -= 500;
                minionProduction.Produce(building, "Tier2");
            }else{
                Debug.Log("No tienes recursos/nivel de fabrica suficientes para fabricar este minion tier 2");
            }
        });
        GameObject tier3button = choosePanelMinion.transform.Find("miniontier3").gameObject;
        GameObject text3button = tier3button.transform.Find("Text").gameObject;
        text3button.GetComponent<UnityEngine.UI.Text>().text = "Tier 3: " + 1000; 
        tier3button.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        tier3button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
            if(Globals.gameResources["Coins"].currentR >= 1000 && Int32.Parse(Globals.buildingDataDic[building.GetInstanceID()]["Level"]) >= 7 && minionProduction.storageComplete() == false){
                Globals.gameResources["Coins"].currentR -= 1000;
                minionProduction.Produce(building, "Tier3");
            }else{
                Debug.Log("No tienes recursos/nivel de fabrica suficientes para fabricar este minion tier 3");
            }
        });
        GameObject tier4button = choosePanelMinion.transform.Find("miniontier4").gameObject;
        GameObject text4button = tier4button.transform.Find("Text").gameObject;
        text4button.GetComponent<UnityEngine.UI.Text>().text = "Tier 4: " + 2000; 
        tier4button.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        tier4button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
             if(Globals.gameResources["Coins"].currentR >= 2000 && Int32.Parse(Globals.buildingDataDic[building.GetInstanceID()]["Level"]) >= 10 && minionProduction.storageComplete() == false){
                Globals.gameResources["Coins"].currentR -= 2000;
                minionProduction.Produce(building, "Tier4");
            }else{
                Debug.Log("No tienes recursos/nivel de fabrica suficientes para fabricar este minion tier 4");
            }
        });
        panel.SetActive(true);
    }
    public void showPanelInfoMinion(GameObject bulding){
        infoText = panel.transform.Find("InfoText").gameObject;
        infoText.GetComponent<UnityEngine.UI.Text>().text = "Casa nivel: " + level.ToString();
        lvlUpButton = panel.transform.Find("LevelUpButton").gameObject;
        GameObject buttonText = lvlUpButton.transform.Find("LevelUpText").gameObject;
        buttonText.GetComponent<UnityEngine.UI.Text>().text = "Subir de nivel\n" + cost.ToString();
        lvlUpButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        lvlUpButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
            
            LevelUp();
        });
        
        //Globals.minionsQuantity[1];
        GameObject minionInfoHouse = panel.transform.Find("ChoosingMinion").gameObject;
        GameObject qminion1 = minionInfoHouse.transform.Find("qminion1").gameObject;
        qminion1.GetComponent<UnityEngine.UI.Text>().text = "Minions Tier 1: "+ Globals.minionsQuantity[1];
        GameObject qminion2 = minionInfoHouse.transform.Find("qminion2").gameObject;
        qminion2.GetComponent<UnityEngine.UI.Text>().text = "Minions Tier 2: "+ Globals.minionsQuantity[2];
        GameObject qminion3 = minionInfoHouse.transform.Find("qminion3").gameObject;
        qminion3.GetComponent<UnityEngine.UI.Text>().text = "Minions Tier 3: "+ Globals.minionsQuantity[3];
        GameObject qminion4 = minionInfoHouse.transform.Find("qminion4").gameObject;
        qminion4.GetComponent<UnityEngine.UI.Text>().text = "Minions Tier 4: "+ Globals.minionsQuantity[4];
        panel.SetActive(true);
        
        
    }
    public void AdditionalPanels() {
        if(Globals.gameResources["Coins"].currentR >= cost) {
            if (type == "TownHall") LevelUp();
            else if (level+1 <= Int32.Parse(Globals.buildingDataDic[Globals.townHallId]["Level"]))
            {
                if (Globals.buildingDataDic[building.GetInstanceID()]["Type"] == "Bank") bankProduction.chooseAttribute(building, panel, this);
                else LevelUp();
            }
            else noSound.Play();
        }
        else
        {
            noSound.Play();
        }
    }

    public void LevelUp(){
        level++;
        Globals.buildingDataDic[building.GetInstanceID()]["Level"] = level.ToString();
        if(Globals.buildingDataDic[building.GetInstanceID()]["Type"] == "House"){
            Globals.houseDataDic[building.GetInstanceID()] += 1;
        }
        Globals.gameResources["Coins"].DedactResources(cost);

        switch(type){
            case("TownHall"):
                infoText.GetComponent<UnityEngine.UI.Text>().text = "Ayuntamiento nivel: " + level.ToString();
                break;
            case("House"):
                infoText.GetComponent<UnityEngine.UI.Text>().text = "Casa nivel: " + level.ToString();
                break;
            case("Bank"):
                infoText.GetComponent<UnityEngine.UI.Text>().text = "Banco nivel: " + level.ToString();
                break;
            case("Factory"):
                infoText.GetComponent<UnityEngine.UI.Text>().text = "Fábrica nivel: " + level.ToString();
                break;
        }
        lvlUpSound.Play();

        lvlUpButton = panel.transform.Find("LevelUpButton").gameObject;
        GameObject buttonText = lvlUpButton.transform.Find("LevelUpText").gameObject;
        lvlUpButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        if(level >= 10)
        {
            buttonText.GetComponent<UnityEngine.UI.Text>().text = "Nivel Máximo";
        }
        else
        {
            cost = Globals.buildingCostsDic[type].ElementAt(level);
            lvlUpButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
                AdditionalPanels();
            });
            buttonText.GetComponent<UnityEngine.UI.Text>().text = "Subir de nivel\n" + cost.ToString();
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
