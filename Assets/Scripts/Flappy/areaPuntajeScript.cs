using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class areaPuntajeScript : MonoBehaviour
{
    public  GameObject area;
    public GameObject audioMoneda;
    // Start is called before the first frame update
    void Start()
    {
        audioMoneda = GameObject.Find("/SceneController/audiosFondo/audioMoneda");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision){
        puntajeScript.monedasGanadas +=1;
        audioMoneda.SetActive(true);
        desactivarSonido();
       // Debug.Log("Se debe desactivar");
        area.SetActive(false);
    }
    private async Task desactivarSonido(){
        await Task.Delay(100);
        audioMoneda.SetActive(false);
    }
}
