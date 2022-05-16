using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
public class Timer : MonoBehaviour 
{
    Image timeBar;
    public float maxTime = 5f;
    float timeLeft;
    private bool coroutineCalled = false;
    public GameObject segundaOportunidad;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("memoriaScene"));
        timeBar = GetComponent<Image>();
        timeLeft = maxTime;
        Globals.doubleCoinsBoost = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeLeft > 0){
            timeLeft-=Time.deltaTime;
            timeBar.fillAmount = timeLeft/maxTime;
        }else{
            if (Globals.restartGameBoost)
            {
                segundaOportunidad.SetActive(true);
                timeLeft += maxTime;
                Globals.restartGameBoost = false;
            }
            else{
                if (Globals.doubleCoinsBoost)
                {
                    Globals.gameResources["Coins"].currentR += Globals.obtainedCoins * 2;
                    Globals.doubleCoinsBoost = false;
                }
                else Globals.gameResources["Coins"].currentR += Globals.obtainedCoins;
                Debug.Log("Monedas obtenidas: " + Globals.obtainedCoins);
                Globals.obtainedCoins = 0;
                Time.timeScale = 1f;
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
        SceneManager.UnloadSceneAsync("memoriaScene");
    }
}
