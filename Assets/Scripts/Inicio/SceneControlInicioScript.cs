using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading.Tasks;

public class SceneControlInicioScript : MonoBehaviour
{
    
    GameObject notificacion;
    GameObject videoIntro;
    GameObject objetoDialogo; 
    GameObject buttonNotificacion;
    GameObject botonCorreo;
    GameObject movilCorreo;
    GameObject movilInteraccion;
    GameObject backRoom;
    VideoPlayer componenteVideoIntro;
    // Start is called before the first frame update
    void Start()
    {
        SavedData savedData = SaveManager.LoadGameData();
        if (savedData != null) Globals.tutorialStep = savedData.tutorialStep;
        if (Globals.tutorialStep != 0)
        {
            changeScene();
        }
        else
        {
            videoIntro = GameObject.Find("/SceneControl/Controlador/VideoIntro");
            componenteVideoIntro = videoIntro.GetComponent<VideoPlayer>();
            notificacion = GameObject.Find("/SceneControl/Controlador/MovilNotificacion");
            movilCorreo = GameObject.Find("/SceneControl/Controlador/MovilCorreo");
            movilInteraccion = GameObject.Find("/SceneControl/Controlador/MovilInteraccion");
            buttonNotificacion = GameObject.Find("/SceneControl/Controlador/MovilNotificacion/Canvas/BotonNotificacion");
            botonCorreo = GameObject.Find("/SceneControl/Canvas/ProbarBoton/");
            backRoom = GameObject.Find("/SceneControl/Controlador/BackRoom");
            objetoDialogo = GameObject.Find("/SceneControl/Controlador/ObjetoDialogo");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((componenteVideoIntro.frame) > 0 && (componenteVideoIntro.isPlaying == false)){
            videoIntro.SetActive(false);
            notificacion.SetActive(true);
            backRoom.SetActive(true);
        }
        Button compoButtonNotificacion = buttonNotificacion.GetComponent<Button>();
        Button compoBotonCorreo = botonCorreo.GetComponent<Button>();
        compoButtonNotificacion.onClick.AddListener(() =>{
            notificacion.SetActive(false);
            movilCorreo.SetActive(true);
            botonCorreo.SetActive(true);
        });
        compoBotonCorreo.onClick.AddListener(()=>{
            movilCorreo.SetActive(false);
            botonCorreo.SetActive(false);
            movilInteraccion.SetActive(true);
            objetoDialogo.SetActive(true);
        });

    }

    void changeScene() {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
    
    
}
