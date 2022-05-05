using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour 
{
    Image timeBar;
    public float maxTime = 5f;
    float timeLeft;
    public GameObject timeUpText;
    private bool coroutineCalled = false;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("memoriaScene"));
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
        SceneManager.UnloadSceneAsync("memoriaScene");
    }
}
