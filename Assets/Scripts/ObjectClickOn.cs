using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClickOn : MonoBehaviour
{
    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if(hit.collider.tag == "Player"){
                    IClick click = hit.collider.gameObject.GetComponent<IClick>();
                    if(click != null){
                        click.onClickAction();
                    }
                }
                Debug.Log("Target Name: " + hit.collider.gameObject.tag);
            } 
        } 
    }
}
