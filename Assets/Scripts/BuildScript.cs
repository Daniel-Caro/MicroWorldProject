using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildScript : MonoBehaviour//, IClick
{

    public GameObject panel;
    private GameObject building;
    public int level;
    public int cost;

    private void Start() {
        //panel = GameObject.Find("InfoBuildingPanel");
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                /*if(hit.collider.tag == "Player"){
                    //building = hit.collider.gameObject;
                    IClick click = hit.collider.gameObject.GetComponent<IClick>();
                    if(click != null){
                        click.onClickAction();
                    }
                }*/
                //building = hit.collider.gameObject.GetComponent<Building>();
                Debug.Log("Target Name: " + hit.collider.gameObject.tag);
                panel.SetActive(true);
                building = hit.collider.gameObject.transform.parent.gameObject;
                Debug.Log("Se ha clicado el edificio: " + building.name);
                //Debug.Log("Nivel del edificio: " + building.level);
            } 
        } 
    }

    /*public void onClickAction(){
        Debug.Log("clicaste la casa");
        Debug.Log("Se ha clicado el edificio: " + building.gameObject.name);
        Debug.Log("Está el panel: " + panel.name);
    }*/

    /*public void showPanel(){
        GameObject text = this.transform.Find("LevelUpText").gameObject;
        text.GetComponent<UnityEngine.UI.Text>().text = "Ayuntamiento nivel: " + level.ToString();
        this.transform.Find("Canvas").gameObject.SetActive(true);
    }*/
}
