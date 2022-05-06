using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class saltosVerticalesEndLineScript : MonoBehaviour
{    
    public GameObject gameOverText;
    private bool coroutineCalled = false;
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
            if (!coroutineCalled)
            {
                StartCoroutine(changeScene());
                Debug.Log("after calling corot");
                coroutineCalled = true;
            }
        }
    }

    IEnumerator changeScene() {
        Debug.Log("Empieza la corrutina");
        yield return new WaitForSeconds(3f);
        Debug.Log("Termina el tiempo");
        Scene mainScene = SceneManager.GetSceneByName("SampleScene");
        SceneManager.SetActiveScene(mainScene);
        mainScene.GetRootGameObjects().First().gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync("saltosVerticalesScene");
    }
}
