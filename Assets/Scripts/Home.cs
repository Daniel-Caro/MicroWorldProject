using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}