using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BuildScript : MonoBehaviour//, IClick
{

    public GameObject panel;
    private GameObject building;
    public string type;
    public int level;
    public int cost;
    public double factor;
    private GameObject infoText;
    private GameObject lvlUpButton; 
    public AudioSource lvlUpSound;
    public AudioSource noSound;

    private void Start() {
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if(hit.collider.gameObject.tag == "building"){
                    building = hit.collider.gameObject.transform.parent.gameObject;
                    Debug.Log(building.GetInstanceID());
                    type = Globals.buildingTypesDic[building.GetInstanceID()];
                    level = Globals.buildingLevelsDic[building.GetInstanceID()];
                    cost = Globals.buildingCostsDic[building.GetInstanceID()]; 
                    showPanel();
                }
            } 
            
        } 
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
                infoText.GetComponent<UnityEngine.UI.Text>().text = "Fábrica nivel: " + level.ToString();
                break;
        }
        
        lvlUpButton = panel.transform.Find("LevelUpButton").gameObject;
        GameObject buttonText = lvlUpButton.transform.Find("LevelUpText").gameObject;
        
        lvlUpButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        lvlUpButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
            LevelUp();
        });
        buttonText.GetComponent<UnityEngine.UI.Text>().text = "Subir de nivel\n" + cost.ToString();
        panel.SetActive(true);
    }

    public void LevelUp(){
        if(Globals.gameResources["Coins"].currentR >= cost) { //Condicion de coste (recursos >= coste)
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
                    infoText.GetComponent<UnityEngine.UI.Text>().text = "Fábrica nivel: " + level.ToString();
                    break;
            }
            GameObject buttonText = lvlUpButton.transform.Find("LevelUpText").gameObject;
            buttonText.GetComponent<UnityEngine.UI.Text>().text = "Subir de nivel\n" + cost.ToString();
            lvlUpSound.Play();
        }
        else
        {
            noSound.Play();
        }
    }

    private int RoundOff (int i)
    {
        return ((int)Math.Round(i / 10.0)) * 10;
    }
}
