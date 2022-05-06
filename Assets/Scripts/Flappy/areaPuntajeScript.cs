using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class areaPuntajeScript : MonoBehaviour
{
    public  GameObject area;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision){
        puntajeScript.monedasGanadas +=1;
       // Debug.Log("Se debe desactivar");
        area.SetActive(false);
    }
}
