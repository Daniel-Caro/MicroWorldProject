using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TimerFrogger : MonoBehaviour 
{
    //
    Image timeBar;
    public float maxTime;
    float timeLeft;
    public GameObject timeUpText;
    public GameObject frogger;
    private bool coroutineCalled = false;
    // Start is called before the first frame update
    void Start()
    {
        timeUpText.SetActive(false);
        timeBar = GetComponent<Image>();
        timeLeft = maxTime;

    }

    // Update is called once per frame
    void Update()
    {
        if(timeLeft > 0){
            timeLeft-=Time.deltaTime;
            timeBar.fillAmount = timeLeft/maxTime;
        }else{
            timeUpText.SetActive(true);
            frogger.SetActive(false);
            Time.timeScale = 1;
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
        SceneManager.UnloadSceneAsync("froggerScene");
    }
}