using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class saltosVerticalesEndLineScript : MonoBehaviour
{    
    public GameObject gameOverText;
    public GameObject secondChanceText;
    private bool coroutineCalled = false;
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        Debug.Log("Algo ha cruzado la linea de fin");
        GameObject interceptedObject = collider.gameObject;
        if (interceptedObject.name == "Player")
        {
            if (Globals.restartGameBoost)
            {
                Globals.restartGameBoost = false;
                StartCoroutine(secondChance());
            }
            else
            {
                Debug.Log("Monedas obtenidas: " + Globals.obtainedCoins);
                if (Globals.doubleCoinsBoost)
                {
                    Globals.gameResources["Coins"].currentR += Globals.obtainedCoins * 2;
                    Globals.doubleCoinsBoost = false;
                }
                else Globals.gameResources["Coins"].currentR += Globals.obtainedCoins;
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

    IEnumerator secondChance() {
        Debug.Log("Empieza la corrutina");
        secondChanceText.SetActive(true);
        yield return new WaitForSeconds(2f);
        secondChanceText.SetActive(false);
        SceneManager.UnloadSceneAsync("saltosVerticalesScene");
        SceneManager.LoadScene("saltosVerticalesScene", LoadSceneMode.Additive);
        Debug.Log("Termina el tiempo");
    }
}
