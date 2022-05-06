using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClickOn : MonoBehaviour
{
    private GameObject panel;
    private Building building;

    private void Start() {
        panel = GameObject.Find("InfoBuildingPanel");
    }

    /*void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if(hit.collider.tag == "Player"){
                    //building = hit.collider.gameObject;
                    IClick click = hit.collider.gameObject.GetComponent<IClick>();
                    if(click != null){
                        click.onClickAction();
                    }
                }
                Debug.Log("Target Name: " + hit.collider.gameObject.tag);
            } 
        } 
    }*/
}
