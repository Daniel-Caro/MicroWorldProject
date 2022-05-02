using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueStart : MonoBehaviour
{
    [SerializeField, TextArea(4,6)] private string[] dialogueLines;
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject decisionPanel1;
    [SerializeField] private GameObject decisionPanel2;
    [SerializeField] private GameObject decisionPanel3;
    private List<int> decisiones = new List<int>();
    private bool didDialogueStart;
    private int lineIndex;
    private float timeChar;
    
    // Start is called before the first frame update
    void Start()
    {
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(decisionPanel1.activeSelf){
           GameObject gObutton1 = decisionPanel1.transform.Find("Opcion1").gameObject;
           Button button1 = gObutton1.GetComponent<Button>();
           GameObject gObutton2 = decisionPanel1.transform.Find("Opcion2").gameObject;
           Button button2 = gObutton2.GetComponent<Button>();
           button1.onClick.AddListener(() => {
               decisiones.Add(1);
               Debug.Log("Guardada opcion: "+ decisiones[0]);
               decisionPanel1.SetActive(false);
           });
           button2.onClick.AddListener(() => {
               decisiones.Add(2);
               Debug.Log("Guardada opcion: "+ decisiones[0]);
               decisionPanel1.SetActive(false);
           });
        }else if(decisionPanel2.activeSelf){
            GameObject gObutton3 = decisionPanel2.transform.Find("Opcion3").gameObject;
            Button button3 = gObutton3.GetComponent<Button>();
            GameObject gObutton4 = decisionPanel2.transform.Find("Opcion4").gameObject;
            Button button4 = gObutton4.GetComponent<Button>();
            button3.onClick.AddListener(() => {
                decisiones.Add(3);
                Debug.Log("Guardada opcion: "+ decisiones[1]);
                decisionPanel2.SetActive(false);
            });
            button4.onClick.AddListener(() => {
                decisiones.Add(4);
                Debug.Log("Guardada opcion: "+ decisiones[1]);
                decisionPanel2.SetActive(false);
            });
        }else if(Input.GetButtonDown("Fire1")){
            if(text.text == dialogueLines[lineIndex]){
                NextDialogueLine();
            }
        }
    }
    private void StartDialogue(){
        didDialogueStart = true;
        panel.SetActive(true);
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }
    private void NextDialogueLine(){
        lineIndex++;
        
        if(lineIndex < dialogueLines.Length){
            StartCoroutine(ShowLine());
            
        }else {
            panel.SetActive(false);
        }
    }
    private IEnumerator ShowLine(){
        text.text = string.Empty;
        foreach(char ch in dialogueLines[lineIndex]){
            text.text += ch;
            yield return new WaitForSeconds(timeChar);
        }controladorPanel(lineIndex);
    }
    private void controladorPanel(int lineIndex){
        if(lineIndex == 3){
            decisionPanel2.SetActive(true);
        }else if(lineIndex == 2){
            decisionPanel1.SetActive(true);
        }else if(lineIndex == 1){
            
        }else if(lineIndex == 0){
            
        }
    }
    
}
