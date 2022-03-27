using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saltosVerticalesEndLineScript : MonoBehaviour
{    
    public GameObject gameOverText;
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        Debug.Log("Algo ha cruzado la linea de fin");
        GameObject interceptedObject = collider.gameObject;
        if (interceptedObject.name == "Player")
        {
            //Se tiene que añadir las monedas obtenidas a la cantidad de monedas del jugador (10 monedas por cada una? 100?)
            Debug.Log("Monedas obtenidas: " + Globals.obtainedCoins);
            Globals.obtainedCoins = 0;
            gameOverText.SetActive(true);
        }
    }
}
