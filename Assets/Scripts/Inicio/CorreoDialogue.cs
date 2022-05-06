using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CorreoDialogue : MonoBehaviour
{
    [SerializeField, TextArea(4,6)] private string lineCorreo;
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text text;
    private bool didDialogueStart;
    private int lineIndex;
    private float timeChar;
    // Start is called before the first frame update
    void Start()
    {
        StartDialogue();
    }
    private void StartDialogue(){
        didDialogueStart = true;
        panel.SetActive(true);
        StartCoroutine(ShowLine());
    }

    private IEnumerator ShowLine(){
        text.text = string.Empty;
        foreach(char ch in lineCorreo){
            text.text += ch;
            yield return new WaitForSeconds(timeChar);
        }
    }
}
