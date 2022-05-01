using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueStart : MonoBehaviour
{
    [SerializeField, TextArea(4,6)] private string[] dialogueLines;
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject decisionPanel1;
    [SerializeField] private GameObject decisionPanel2;
    [SerializeField] private GameObject decisionPanel3;
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
        if(Input.GetButtonDown("Fire1")){
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
            muestrapanel(lineIndex);
        }else {
            panel.SetActive(false);
        }
    }
    private IEnumerator ShowLine(){
        text.text = string.Empty;
        foreach(char ch in dialogueLines[lineIndex]){
            text.text += ch;
            yield return new WaitForSeconds(timeChar);
        }
    }
    private void muestrapanel( int lineIndex){
        if(lineIndex == 4){
            decisionPanel3.SetActive(false);
        }else if(lineIndex == 3){
            decisionPanel3.SetActive(true);
            decisionPanel2.SetActive(false);
        }else if(lineIndex == 2){
            decisionPanel2.SetActive(true);
            decisionPanel1.SetActive(false);
        }else if(lineIndex == 1){
            decisionPanel1.SetActive(true);
        }
    }
}
