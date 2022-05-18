using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(BoxCollider2D))]
public class Home : MonoBehaviour
{
    public GameObject frog;
    private BoxCollider2D boxCollider;
    public GameObject frogger;
    
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();

    }
    
    private void activar()
    {  
        frog.SetActive(true);
        frogger.SetActive(false);
        boxCollider.enabled = false;
        GameObject.Find("/Canvas/Image").SetActive(false);
        StartCoroutine(changeScene());
    }

    private void OnDisable()
    {
        frog.SetActive(false);
        boxCollider.enabled = true;
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("La rana llega");
            
            activar();
            //FindObjectOfType<GameManager>().HomeOccupied();
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