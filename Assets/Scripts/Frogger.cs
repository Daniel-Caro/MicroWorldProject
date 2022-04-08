using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frogger : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) ){
            Vector3 direction = new Vector3(0,0.19f,0);
            transform.rotation = Quaternion.Euler(0f,0f,0f);
            Move(direction);
        }
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) ){
            Vector3 direction = new Vector3(0,-0.19f,0);
            transform.rotation = Quaternion.Euler(0f,0f,180f);
            Move(direction);
        }
        else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) ){
            Vector3 direction = new Vector3(-0.19f,0,0);
            transform.rotation = Quaternion.Euler(0f,0f,90f);
            Move(direction);
        }
        else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) ){
            Vector3 direction = new Vector3(0.19f,0,0);
            transform.rotation = Quaternion.Euler(0f,0f,-90f);
            Move(direction);
        }
    }
    private void Move(Vector3 direction){
        transform.position += direction;
    }
}
