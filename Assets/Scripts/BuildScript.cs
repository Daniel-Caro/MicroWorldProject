using System;
using System.Collections;
using System.Collections.Generic;
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
    private GameObject infoText;
    private GameObject lvlUpButton; 
    public AudioSource lvlUpSound;
    public AudioSource noSound;
    public BankProduction bankProduction;

    private Coroutine lastRoutine = null;

    private void Start() {
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && !panel.activeSelf)
            {
                if(hit.collider.gameObject.tag == "building"){
                    building = hit.collider.gameObject.transform.parent.gameObject;
                    if (building.GetInstanceID() != thisBuilding.GetInstanceID()) return;
                    type = Globals.buildingTypesDic[building.GetInstanceID()];
                    
                    level = Globals.buildingLevelsDic[building.GetInstanceID()];
                    cost = Globals.buildingCostsDic[building.GetInstanceID()];
                    if (type == "Bank"){
                        if (bankProduction.HarvestResource(building)){
                            building.transform.Find("money").gameObject.SetActive(false);
                            coinSound.Play();

                            Debug.Log("Comienza la rutina");
                            lastRoutine = StartCoroutine(HoldTimer());
                        } else showPanel();
                    } 
                    else showPanel();
                    
                }
            } 
        }

        if(Input.GetKeyUp(KeyCode.Mouse0)){
            Debug.Log("Parar la rutina");
            if (lastRoutine != null) StopCoroutine(lastRoutine);
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
            case("Bank"):
                infoText.GetComponent<UnityEngine.UI.Text>().text = "Banco nivel: " + level.ToString();
                break;
            case("Factory"):
                infoText.GetComponent<UnityEngine.UI.Text>().text = "F??brica nivel: " + level.ToString();
                break;
        }
        
        lvlUpButton = panel.transform.Find("LevelUpButton").gameObject;
        GameObject buttonText = lvlUpButton.transform.Find("LevelUpText").gameObject;
        
        lvlUpButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        lvlUpButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
            AdditionalPanels();
        });
        buttonText.GetComponent<UnityEngine.UI.Text>().text = "Subir de nivel\n" + cost.ToString();
        panel.SetActive(true);
    }

    public void AdditionalPanels() {
        if(Globals.gameResources["Coins"].currentR >= cost) {
            if (Globals.buildingTypesDic[building.GetInstanceID()] == "Bank") bankProduction.chooseAttribute(building, panel, this);
            else LevelUp();
        }
        else
        {
            noSound.Play();
        }
    }

    public void LevelUp(){
        level++;
        Globals.buildingLevelsDic[building.GetInstanceID()] = level;

        Globals.gameResources["Coins"].DedactResources(cost);
        cost = RoundOff(cost + Convert.ToInt32(Math.Pow(10, level*factor)));
        Globals.buildingCostsDic[building.GetInstanceID()] = cost;

        switch(type){
            case("TownHall"):
                infoText.GetComponent<UnityEngine.UI.Text>().text = "Ayuntamiento nivel: " + level.ToString();
                break;
            case("Bank"):
                infoText.GetComponent<UnityEngine.UI.Text>().text = "Banco nivel: " + level.ToString();
                break;
            case("Factory"):
                infoText.GetComponent<UnityEngine.UI.Text>().text = "F??brica nivel: " + level.ToString();
                break;
        }
        lvlUpSound.Play();

        lvlUpButton = panel.transform.Find("LevelUpButton").gameObject;
        GameObject buttonText = lvlUpButton.transform.Find("LevelUpText").gameObject;
        lvlUpButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        lvlUpButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
            AdditionalPanels();
        });
        buttonText.GetComponent<UnityEngine.UI.Text>().text = "Subir de nivel\n" + cost.ToString();
    }

    

    private int RoundOff (int i)
    {
        return ((int)Math.Round(i / 10.0)) * 10;
    }
}
