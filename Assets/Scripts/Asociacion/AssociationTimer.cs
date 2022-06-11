using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AssociationTimer : MonoBehaviour
{
    Image timerBar;
    public float maxTime = 5f;
    float timeLeft;
    public GameObject timesUpText;
    public GameObject secondChanceText;
    private bool coroutineCalled = false;

    // Start is called before the first frame update
    void Start()
    {
        timesUpText.SetActive(false);
        timerBar = GetComponent<Image>();
        timeLeft = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
        } else{
            if (Globals.restartGameBoost)
            {
                timeLeft += maxTime;
                Globals.restartGameBoost = false;
                StartCoroutine(secondChance());
            }
            else
            {
                GameObject.Find("Board").GetComponent<AssociationBoard>().currentState = GameState.WAIT;
                if (Globals.doubleCoinsBoost)
                {
                    Globals.gameResources["Coins"].currentR += Globals.obtainedCoins * 2;
                    Globals.doubleCoinsBoost = false;
                }
                else Globals.gameResources["Coins"].currentR += Globals.obtainedCoins;
                Debug.Log("Monedas obtenidas: " + Globals.obtainedCoins);
                Globals.obtainedCoins = 0;
                Time.timeScale = 1f;
                timesUpText.SetActive(true);
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
        SceneManager.UnloadSceneAsync("minijuego-asociacion");
    }

    IEnumerator secondChance() {
        Debug.Log("Empieza la corrutina");
        secondChanceText.SetActive(true);
        yield return new WaitForSeconds(2f);
        secondChanceText.SetActive(false);
        Debug.Log("Termina el tiempo");
    }

}
