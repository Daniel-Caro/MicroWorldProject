using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool Placed { get; private set; }
    public BoundsInt area;
    
  
    private void Start() {
        //panel = GameObject.Find("InfoBuildingPanel");
    }
    void Update() { 
        /*
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {   
                Debug.Log("clicaste la casa");
                GameObject text = this.transform.Find("InfoText").gameObject;
                text.GetComponent<TextMeshProUGUI>().text = "Ayuntamiento nivel: " + level.ToString();
                this.transform.Find("Panel").gameObject.SetActive(true);
                Debug.Log("Target Name: " + hit.collider.gameObject.name);
            } 
        } */
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
        this.transform.parent = GameObject.Find("SampleSceneObject").transform;
        GridBuildingSystem.current.TakeArea(areaTemp);
    }
}
