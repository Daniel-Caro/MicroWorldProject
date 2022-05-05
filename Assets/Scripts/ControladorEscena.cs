using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ControladorEscena : MonoBehaviour
{
    public GameObject canvasPerdiste;
    private bool coroutineCalled = false;
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
        
        puntajeScript.meterMonedas();
        canvasPerdiste.SetActive(true);
        
        Time.timeScale = 1f;
        if (!coroutineCalled)
        {
            StartCoroutine(changeScene());
            Debug.Log("after calling corot");
            coroutineCalled = true;
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

    public void restart(){
        SceneManager.LoadScene(1);
    }
}
