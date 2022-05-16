using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tardarEmpezarScript : MonoBehaviour
{
    private GameObject genObs;
    

    // Start is called before the first frame update
    void Start()
    {
        genObs = GameObject.Find("/SceneController/generadorObstaculos");
        
        genObs.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            genObs.SetActive(true);
        }
    }
}
