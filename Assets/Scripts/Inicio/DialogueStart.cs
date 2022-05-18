using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading.Tasks;
public class DialogueStart : MonoBehaviour
{
    [SerializeField, TextArea(4,6)] private string[] dialogueLines;
    [SerializeField] private GameObject panel;
    [SerializeField] private Text text;
    [SerializeField] private GameObject decisionPanel1;
    [SerializeField] private GameObject decisionPanel2;
    [SerializeField] private GameObject decisionPanel3;
    [SerializeField] private GameObject decisionPanel4;
    [SerializeField] private GameObject decisionPanel5;
    [SerializeField] private GameObject decisionPanel6;
    [SerializeField] private GameObject decisionPanel7;
    [SerializeField] private GameObject insertarNombre;
    [SerializeField] private GameObject insertarEdad;
    private GameObject videoFinal;
    private VideoPlayer componenteVideoFinal;
    private Button enviarEdad;
    private Text respuestaEdad;
    private Button enviarNombre;
    public static Text respuestaNombre;
    private Text respuestaPanel1; 
    private Text respuestaPanel3;
    private List<int> decisiones = new List<int>();
    private bool didDialogueStart;
    private int lineIndex;
    private float timeChar;
    private bool onetime = false;
    private GameObject movilInteraccion;
    private bool coroutineCalled = false;
    private bool haTerminado = false;
    // Start is called before the first frame update
    void Start()
    {
        StartDialogue();
        movilInteraccion = GameObject.Find("/SceneControl/Controlador/MovilInteraccion");
        enviarNombre = insertarNombre.transform.Find("EnviarNombre").gameObject.GetComponent<Button>();
        enviarEdad = insertarEdad.transform.Find("EnviarEdad").gameObject.GetComponent<Button>();
        videoFinal = GameObject.Find("/SceneControl/Controlador/VideoFinal");
        componenteVideoFinal = videoFinal.GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(decisionPanel1.activeSelf){
           GameObject gObutton1 = decisionPanel1.transform.Find("OpcionVivir").gameObject;
           Button button1 = gObutton1.GetComponent<Button>();
           GameObject gObutton2 = decisionPanel1.transform.Find("OpcionTrabajo").gameObject;
           Button button2 = gObutton2.GetComponent<Button>();
           GameObject gObutton3 = decisionPanel1.transform.Find("OpcionEstudiar").gameObject;
           Button button3 = gObutton3.GetComponent<Button>();
           button1.onClick.AddListener(() => {
               if(onetime == false){
                    decisiones.Add(1);
                    decisionPanel1.SetActive(false);
                    respuestaPanel1 = button1.transform.Find("Text").gameObject.GetComponent<Text>();
                    lineIndex =3;
                    NextDialogueLine();
                    onetime = true;
               }
               
           });
           button2.onClick.AddListener(() => {
               if(onetime == false){
                    decisiones.Add(2);
                    decisionPanel1.SetActive(false);
                    respuestaPanel1 = button2.transform.Find("Text").gameObject.GetComponent<Text>();
                    lineIndex =3;
                    NextDialogueLine();
                    onetime = true;
               }
           });
           button3.onClick.AddListener(() => {
               if(onetime == false){
                    decisiones.Add(3);
                    decisionPanel1.SetActive(false);
                    respuestaPanel1 = button3.transform.Find("Text").gameObject.GetComponent<Text>();
                    lineIndex =3;
                    NextDialogueLine();
                    onetime = true;
               }
            });
        }else if(decisionPanel2.activeSelf){
            GameObject gObutton4 = decisionPanel2.transform.Find("OpcionNinos").gameObject;
            Button button4 = gObutton4.GetComponent<Button>();
            GameObject gObutton5 = decisionPanel2.transform.Find("OpcionAnciano").gameObject;
            Button button5 = gObutton5.GetComponent<Button>();
            button4.onClick.AddListener(() => {
                if(onetime == true){
                    decisiones.Add(4);
                    Debug.Log("Guardada opcion: "+ decisiones[1]);
                    decisionPanel2.SetActive(false);
                    lineIndex = 5;
                    NextDialogueLine();
                    onetime=false;
                }
                
            });
            button5.onClick.AddListener(() => {
                if(onetime == true){
                    decisiones.Add(5);
                    Debug.Log("Guardada opcion: "+ decisiones[1]);
                    decisionPanel2.SetActive(false);
                    lineIndex = 5;
                    NextDialogueLine();
                    onetime=false;
                }
            });
        }else if(decisionPanel3.activeSelf){
            GameObject gObutton6 = decisionPanel3.transform.Find("OpcionNinos").gameObject;
            Button button6 = gObutton6.GetComponent<Button>();
            GameObject gObutton7 = decisionPanel3.transform.Find("OpcionFamiliar").gameObject;
            Button button7 = gObutton7.GetComponent<Button>();
            button6.onClick.AddListener(() => {
                if(onetime == false){
                    respuestaPanel3 = button6.transform.Find("Text").gameObject.GetComponent<Text>();
                    decisiones.Add(6);
                    Debug.Log("Guardada opcion: "+ decisiones[1]);
                    decisionPanel3.SetActive(false);
                    lineIndex = 6;
                    NextDialogueLine();
                    onetime=true;
                }
                
            });
            button7.onClick.AddListener(() => {
                if(onetime == false){
                    respuestaPanel3= button7.transform.Find("Text").gameObject.GetComponent<Text>();
                    decisiones.Add(5);
                    Debug.Log("Guardada opcion: "+ decisiones[1]);
                    decisionPanel3.SetActive(false);
                    lineIndex = 6;
                    NextDialogueLine();
                    onetime=true;
                }
            });
        }else if(decisionPanel4.activeSelf){
           GameObject gObutton8 = decisionPanel4.transform.Find("OpcionPrincesa").gameObject;
           Button button8 = gObutton8.GetComponent<Button>();
           GameObject gObutton9 = decisionPanel4.transform.Find("OpcionCapitan").gameObject;
           Button button9 = gObutton9.GetComponent<Button>();
           GameObject gObutton10 = decisionPanel4.transform.Find("OpcionCyber").gameObject;
           Button button10 = gObutton10.GetComponent<Button>();
           button8.onClick.AddListener(() => {
               if(onetime == true){
                    decisiones.Add(8);
                    Globals.style = Style.Princess;
                    decisionPanel4.SetActive(false);
                    lineIndex =9;
                    NextDialogueLine();
                    onetime = false;
               }
               
           });
           button9.onClick.AddListener(() => {
               if(onetime == true){
                    decisiones.Add(9);
                    Globals.style = Style.Pirate;
                    decisionPanel4.SetActive(false);
                    lineIndex =9;
                    NextDialogueLine();
                    onetime = false;
               }
           });
           button10.onClick.AddListener(() => {
               if(onetime == true){
                    decisiones.Add(10);
                    Globals.style = Style.Future;
                    decisionPanel4.SetActive(false);
                    lineIndex =9;
                    NextDialogueLine();
                    onetime = false;
               }
            });
        }else if(decisionPanel5.activeSelf){
           GameObject gObutton11 = decisionPanel5.transform.Find("OpcionAntorcha").gameObject;
           Button button11 = gObutton11.GetComponent<Button>();
           GameObject gObutton12 = decisionPanel5.transform.Find("OpcionPiedra").gameObject;
           Button button12 = gObutton12.GetComponent<Button>();
           GameObject gObutton13 = decisionPanel5.transform.Find("OpcionPalo").gameObject;
           Button button13 = gObutton13.GetComponent<Button>();
           button11.onClick.AddListener(() => {
               if(onetime == false){
                    decisiones.Add(11);
                    decisionPanel5.SetActive(false);
                    lineIndex =11;
                    NextDialogueLine();
                    onetime = true;
               }
               
           });
           button12.onClick.AddListener(() => {
               if(onetime == false){
                    decisiones.Add(12);
                    decisionPanel5.SetActive(false);
                    lineIndex =11;
                    NextDialogueLine();
                    onetime = true;
               }
           });
           button13.onClick.AddListener(() => {
               if(onetime == false){
                    decisiones.Add(13);
                    decisionPanel5.SetActive(false);
                    lineIndex =11;
                    NextDialogueLine();
                    onetime = true;
               }
            });
        }else if(decisionPanel6.activeSelf){
           GameObject gObutton14 = decisionPanel6.transform.Find("OpcionJack").gameObject;
           Button button14 = gObutton14.GetComponent<Button>();
           GameObject gObutton15 = decisionPanel6.transform.Find("OpcionHeleen").gameObject;
           Button button15 = gObutton15.GetComponent<Button>();
           GameObject gObutton16 = decisionPanel6.transform.Find("OpcionElon").gameObject;
           Button button16 = gObutton16.GetComponent<Button>();
           button14.onClick.AddListener(() => {
               if(onetime == true){
                    decisiones.Add(14);
                    decisionPanel6.SetActive(false);
                    lineIndex =12;
                    NextDialogueLine();
                    onetime = false;
               }
               
           });
           button15.onClick.AddListener(() => {
               if(onetime == true){
                    decisiones.Add(15);
                    decisionPanel6.SetActive(false);
                    lineIndex =12;
                    NextDialogueLine();
                    onetime = false;
               }
           });
           button16.onClick.AddListener(() => {
               if(onetime == true){
                    decisiones.Add(16);
                    decisionPanel6.SetActive(false);
                    lineIndex =12;
                    NextDialogueLine();
                    onetime = false;
               }
            });
        }else if(decisionPanel7.activeSelf){
           GameObject gObutton17 = decisionPanel7.transform.Find("OpcionPiratas").gameObject;
           Button button17 = gObutton17.GetComponent<Button>();
           GameObject gObutton18 = decisionPanel7.transform.Find("OpcionPiratas").gameObject;
           Button button18 = gObutton18.GetComponent<Button>();
           GameObject gObutton19 = decisionPanel7.transform.Find("OpcionFuturo").gameObject;
           Button button19 = gObutton19.GetComponent<Button>();
           button17.onClick.AddListener(() => {
               if(onetime == false){
                    decisionPanel7.SetActive(false);
                    lineIndex = 13;
                    NextDialogueLine();
                    onetime=true;
               }
               
           });
           button18.onClick.AddListener(() => {
               if(onetime == false){
                    decisionPanel7.SetActive(false);
                    lineIndex =13;
                    NextDialogueLine();
                    onetime = true;
               }
           });
           button19.onClick.AddListener(() => {
               if(onetime == false){
                    decisionPanel7.SetActive(false);
                    lineIndex =13;
                    NextDialogueLine();
                    onetime = true;
               }
            });
        }else if(insertarNombre.activeSelf && lineIndex == 0 &&insertarNombre.transform.Find("Text").GetComponent<Text>().text.Length < 10){
                enviarNombre.onClick.AddListener(() => {
                
                    if(onetime == false){
                        respuestaNombre = insertarNombre.transform.Find("Text").GetComponent<Text>();
                        GridBuildingSystem.respuestaNombreSample = respuestaNombre.text;
                        Globals.nombreUsuario = respuestaNombre.text;
                        insertarNombre.SetActive(false);
                        lineIndex = 1;
                        NextDialogueLine();
                        onetime = true;
                    }
        
                    
                
            }); 
                            

            
            
        }else if(insertarEdad.activeSelf && lineIndex == 1 && int.TryParse(insertarEdad.transform.Find("Text").GetComponent<Text>().text,out var n) == true){
            enviarEdad.onClick.AddListener(() => {
                if(onetime == true){
                    respuestaEdad = insertarEdad.transform.Find("Text").GetComponent<Text>();
                    insertarEdad.SetActive(false);
                    lineIndex = 2;
                    Debug.Log(onetime);
                    NextDialogueLine();
                    onetime = false;
                }
                
            }); 
        }else if((Input.GetButtonDown("Fire1")) && (lineIndex == 3 || lineIndex == 6 || lineIndex == 7 || lineIndex == 9 || lineIndex == 12) && haTerminado == true){
                lineIndex++;
                NextDialogueLine();
                haTerminado =false;
                
        }else if((Input.GetButtonDown("Fire1")) &&(lineIndex == 0 || lineIndex == 1 || lineIndex == 2 || lineIndex == 4 || lineIndex == 5 || lineIndex == 8 || lineIndex ==10 || lineIndex == 11) && haTerminado == true){
            Debug.Log("Primera linea");
            StartCoroutine(ShowLine(true));
            haTerminado = false;
        }else if (videoFinal.activeSelf)
        {
            if ((componenteVideoFinal.frame) > 0 && (componenteVideoFinal.isPlaying == false && !coroutineCalled)){
                Debug.Log("Finaliza el video");
                StartCoroutine(changeScene());
                coroutineCalled = true;
            }
            
        }
    }
    private void StartDialogue(){
        didDialogueStart = true;
        panel.SetActive(true);
        lineIndex = 0;
        StartCoroutine(ShowLine(false));
    }
    private void NextDialogueLine(){
     
        Debug.Log("Hola caracola:"+ lineIndex);
        panel.SetActive(true);
        if(lineIndex < dialogueLines.Length){
            StartCoroutine(ShowLine(false));
            
        }else {
            Debug.Log("Hola pesicola:"+ lineIndex);
            panel.SetActive(false);
        }
    }
    private IEnumerator ShowLine(bool pulsarPanel){
        text.text = string.Empty;
        if(lineIndex == 1){
            
            dialogueLines[1] = "Genial "+ respuestaNombre.text +", que edad tienes?";
            
        }else if(lineIndex == 3){
            dialogueLines[3] = "Genial! Siempre esta bien "+ respuestaPanel1.text+". Ahora vamos con preguntas mas complejas.";
        }else if(lineIndex == 6){
            dialogueLines[6] = "De acuerdo, has elegido salvar a "+ respuestaPanel3.text + ", justo lo que esperabamos de ti "+respuestaNombre.text;
        }
        if(pulsarPanel == false){
            Debug.Log(dialogueLines[3]);
            foreach(char ch in dialogueLines[lineIndex]){
                text.text += ch;
                yield return new WaitForSeconds(timeChar);
                
            }
            haTerminado = true;
            if(lineIndex == 13){
                controladorPanel(lineIndex);
            }
        }else{
            controladorPanel(lineIndex);
        }
    }
    private async Task controladorPanel(int lineIndex){
        if(lineIndex == 13){
            await Task.Delay(5000);
            panel.SetActive(false);
            movilInteraccion.SetActive(false);  
            videoFinal.SetActive(true);
        }
        else if(lineIndex == 12){
            panel.SetActive(false);
            decisionPanel7.SetActive(true);
        }
        else if(lineIndex == 11){
            panel.SetActive(false);
            decisionPanel6.SetActive(true);
        }
        else if(lineIndex == 10){
            panel.SetActive(false);
            decisionPanel5.SetActive(true);
        }
        else if(lineIndex == 8){
            panel.SetActive(false);
            decisionPanel4.SetActive(true);
        }
        else if(lineIndex == 5){
            panel.SetActive(false);
            decisionPanel3.SetActive(true);
        }
        else if(lineIndex == 4){
            panel.SetActive(false);
            decisionPanel2.SetActive(true);
        }
        else if(lineIndex == 2){
            panel.SetActive(false);
            decisionPanel1.SetActive(true);
        }
        else if(lineIndex == 1){
            panel.SetActive(false);
            insertarEdad.SetActive(true);
        }
        else if(lineIndex == 0){
            panel.SetActive(false);
            insertarNombre.SetActive(true);
        }
    }

    IEnumerator changeScene() {
        Debug.Log("Empieza la corrutina");
        yield return new WaitForSeconds(3f);
        Debug.Log("Termina el tiempo");
        Globals.tutorialStep = 1;
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

}
