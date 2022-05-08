using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ControladorEscena : MonoBehaviour
{
    public GameObject canvasPerdiste;
    private bool coroutineCalled = false;
    public GameObject secondChanceText;
    public GameObject gameOverText;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void youLose(){
        cambiarEscena(false);
        puntajeScript.meterMonedas();
        canvasPerdiste.SetActive(true);
        
        Time.timeScale = 1f;
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


    IEnumerator changeScene() {
        Debug.Log("Empieza la corrutina");
        yield return new WaitForSeconds(3f);
        Debug.Log("Termina el tiempo");
        Scene mainScene = SceneManager.GetSceneByName("SampleScene");
        SceneManager.SetActiveScene(mainScene);
        mainScene.GetRootGameObjects().First().gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync("minijuegovuelo");
    }
    IEnumerator secondChance() {
        Debug.Log("Empieza la corrutina");
        secondChanceText.SetActive(true);
        yield return new WaitForSeconds(2f);
        secondChanceText.SetActive(false);
        SceneManager.UnloadSceneAsync("minijuegovuelo");
        SceneManager.LoadScene("minijuegovuelo", LoadSceneMode.Additive);
        Debug.Log("Termina el tiempo");
    }
    public void restart(){
        SceneManager.LoadScene(1);
    }
    private void cambiarEscena(bool atributo){
        if(Globals.style == Style.Princess){
            GameObject personajePrincesa = GameObject.Find("/SceneController/personajes/personajePrincesa");
            personajePrincesa.SetActive(atributo);
            GameObject sueloPrincesa = GameObject.Find("/SceneController/suelos/sueloPrincesa");
            sueloPrincesa.GetComponent<Animator>().enabled = atributo;
        }else if(Globals.style == Style.Pirate){
            GameObject personajePirata = GameObject.Find("/SceneController/personajes/personajePirata");
            personajePirata.SetActive(atributo);
            GameObject sueloPirata = GameObject.Find("/SceneController/suelos/sueloPirata");
            sueloPirata.GetComponent<Animator>().enabled = atributo;
        }else if (Globals.style == Style.Future){
            GameObject personajeFuturo = GameObject.Find("/SceneController/personajes/personajeFuturo");
            personajeFuturo.SetActive(atributo);
            GameObject sueloFuturo = GameObject.Find("/SceneController/suelos/sueloFuturo");
            sueloFuturo.GetComponent<Animator>().enabled = atributo;
        }
        obstaculoScript.enMovimiento = atributo;
    }
}
