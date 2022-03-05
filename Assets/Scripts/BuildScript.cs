using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildScript : MonoBehaviour//, IClick
{

    public GameObject panel;
    private GameObject building;
    public int level;
    public int cost;

    private void Start() {
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            Debug.Log(hit.collider.gameObject.tag);
            if (hit.collider != null)
            {
                if(hit.collider.gameObject.tag == "building"){
                    building = hit.collider.gameObject.transform.parent.gameObject;
                    showPanel();
                }
            } 
            
        } 
    }

    public void showPanel(){
        GameObject text = panel.transform.Find("InfoText").gameObject;
        text.GetComponent<UnityEngine.UI.Text>().text = "Ayuntamiento nivel: " + level.ToString();
        GameObject button = panel.transform.Find("LevelUpButton").gameObject;
        GameObject buttonText = button.transform.Find("LevelUpText").gameObject;
        
        button.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
            LevelUp();
        });
        buttonText.GetComponent<UnityEngine.UI.Text>().text = "Subir de nivel\n" + cost.ToString();
        panel.SetActive(true);
    }

    public void LevelUp(){
        if(true) { //Condicion de coste (recursos >= coste)
            level++;
            GameObject text = panel.transform.Find("InfoText").gameObject;
            text.GetComponent<UnityEngine.UI.Text>().text = "Ayuntamiento nivel: " + level.ToString();
        }
    }
}
