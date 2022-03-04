using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool Placed { get; private set; }
    public BoundsInt area;
    public int level;
    public int cost;
  
    void Update() { 
        /*
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {   
                Debug.Log("clicaste la casa");
                GameObject text = this.transform.Find("InfoText").gameObject;
                text.GetComponent<UnityEngine.UI.Text>().text = "Ayuntamiento nivel: " + level.ToString();
                this.transform.Find("Panel").gameObject.SetActive(true);
                Debug.Log("Target Name: " + hit.collider.gameObject.name);
            } 
        } */
    }

    public void onClick(){
        Debug.Log("Clicky puta");
    }


    public void showPanel(){
        GameObject text = this.transform.Find("LevelUpText").gameObject;
        text.GetComponent<UnityEngine.UI.Text>().text = "Ayuntamiento nivel: " + level.ToString();
        this.transform.Find("Canvas").gameObject.SetActive(true);
    }

    public bool CanBePlaced()
    {
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        if (GridBuildingSystem.current.CanTakeArea(areaTemp))
        {
            return true;
        }
        return false;
    }

    public void PrintName(GameObject go){
        print(go.name);
    }

    public void Place()
    {
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        Placed = true;
        GridBuildingSystem.current.TakeArea(areaTemp);
    }
}
