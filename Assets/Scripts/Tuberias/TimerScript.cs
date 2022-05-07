using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    Image timerBar;
    public float maxTime = 5f;
    float timeLeft;
    public GameObject timesUpText;
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
            timesUpText.SetActive(true);
            Debug.Log("Monedas obtenidas: " + Globals.obtainedCoins);
            if (Globals.doubleCoinsBoost)
            {
                Globals.gameResources["Coins"].currentR += Globals.obtainedCoins * 2;
                Globals.doubleCoinsBoost = false;
            }
            else Globals.gameResources["Coins"].currentR += Globals.obtainedCoins;
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

    IEnumerator changeScene() {
        Debug.Log("Empieza la corrutina");
        yield return new WaitForSeconds(3f);
        Debug.Log("Termina el tiempo");
        Scene mainScene = SceneManager.GetSceneByName("SampleScene");
        SceneManager.SetActiveScene(mainScene);
        mainScene.GetRootGameObjects().First().gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync("Tuberias");
    }
}
