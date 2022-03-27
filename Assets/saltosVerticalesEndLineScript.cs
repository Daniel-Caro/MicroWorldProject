using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saltosVerticalesEndLineScript : MonoBehaviour
{    
    public GameObject gameOverText;
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        Debug.Log("Algo ha cruzado el collider");
        GameObject interceptedObject = collider.gameObject;
        if (interceptedObject.name == "Player")
        {
            gameOverText.SetActive(true);
        }
    }
}
