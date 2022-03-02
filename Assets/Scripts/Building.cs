using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool Placed { get; private set; }
    public BoundsInt area;
    public int level;
  
  // Update is called once per frame    
    void Update() {  
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("clicaste la casa");
            GameObject text = this.transform.Find("InfoText").gameObject;
            text.GetComponent<UnityEngine.UI.Text>().text = "Ayuntamiento nivel: " + level.ToString();
            this.transform.Find("Panel").gameObject.SetActive(true);
        } 
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

    public void Place()
    {
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        Placed = true;
        GridBuildingSystem.current.TakeArea(areaTemp);
    }
}
