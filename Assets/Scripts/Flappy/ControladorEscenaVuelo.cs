using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorEscenaVuelo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("minijuegovuelo"));
        obstaculoScript.enMovimiento = true;
        if(Globals.style == Style.Princess){
            GameObject fondoPrincesa = GameObject.Find("/SceneController/fondos/fondoPrincesa");
            fondoPrincesa.SetActive(true);
            GameObject sueloPrincesa = GameObject.Find("/SceneController/suelos/sueloPrincesa");
            sueloPrincesa.SetActive(true);
            GameObject personajePrincesa = GameObject.Find("/SceneController/personajes/personajePrincesa");
            personajePrincesa.SetActive(true);
            GameObject audioPrincesaFondo = GameObject.Find("/SceneController/audiosFondo/audioPrincesaFondo");
            audioPrincesaFondo.SetActive(true);
        }else if(Globals.style == Style.Pirate){
            GameObject fondoPirata = GameObject.Find("/SceneController/fondos/fondoPirata");
            fondoPirata.SetActive(true);
            GameObject sueloPirata = GameObject.Find("/SceneController/suelos/sueloPirata");
            sueloPirata.SetActive(true);
            GameObject personajePirata = GameObject.Find("/SceneController/personajes/personajePirata");
            personajePirata.SetActive(true);
            GameObject audioPirataFondo = GameObject.Find("/SceneController/audiosFondo/audioPirataFondo");
            audioPirataFondo.SetActive(true);
        }else if(Globals.style == Style.Future){
            GameObject fondoFuturo = GameObject.Find("/SceneController/fondos/fondoFuturo");
            fondoFuturo.SetActive(true);
            GameObject sueloFuturo = GameObject.Find("/SceneController/suelos/sueloFuturo");
            sueloFuturo.SetActive(true);
            GameObject personajeFuturo = GameObject.Find("/SceneController/personajes/personajeFuturo");
            personajeFuturo.SetActive(true);
            GameObject audioFuturoFondo = GameObject.Find("/SceneController/audiosFondo/audioFuturoFondo");
            audioFuturoFondo.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
