using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuberiasGameManager : MonoBehaviour
{
    public GameObject PipesHolder;
    public GameObject[] Pipes;
    public GameObject[] noWaterPipes;
    public GameObject[] WaterPipes;

    [SerializeField]
    int totalPipes = 0;

    // Start is called before the first frame update
    void Start()
    {
        float x = -6f;
        float y = 3f;

        while (y > -4f)
        {
            while (x < 7f)
            {
                if (y == 3f && x == -6f)
                {
                    x += 1.5f;
                    continue;
                }
                int rand = Random.Range(0, noWaterPipes.Length);
                GameObject newPipe = Instantiate(noWaterPipes[rand], new Vector3(x, y, -1f), Quaternion.identity);
                newPipe.transform.localScale = new Vector3(0.3f, 0.3f, 1);
                newPipe.transform.parent = PipesHolder.transform;
                x += 1.5f;
            }
            x = -6f;
            y -= 1.5f;
        }

        totalPipes = PipesHolder.transform.childCount;

        Pipes = new GameObject[totalPipes];

        for (int i = 0; i < Pipes.Length; i++)
        {
            Pipes[i] = PipesHolder.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                GameObject pipe = hit.collider.gameObject;
                if(pipe.name != "startPipe"){
                    if (Globals.selectedPipe == null)
                    {
                        Globals.selectedPipe = pipe;
                        pipe.GetComponentInChildren<SpriteRenderer>().color = new Color(1f,1f,1f,.8f);
                    }
                    else
                    {
                        if (Globals.selectedPipe.GetInstanceID() == pipe.GetInstanceID()) 
                        {
                            Globals.selectedPipe = null;
                            pipe.GetComponentInChildren<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
                        }
                        else
                        {
                            //Se obtienen los atributos de ambos pipes
                            Sprite selectedDrySprite = Globals.selectedPipe.GetComponent<PipeScript>().drySprite;
                            Sprite secondDrySprite = pipe.GetComponent<PipeScript>().drySprite;
                            Sprite selectedWaterSprite = Globals.selectedPipe.GetComponent<PipeScript>().waterSprite;
                            Sprite secondWaterSprite = pipe.GetComponent<PipeScript>().waterSprite;
                            string selectedType = Globals.selectedPipe.GetComponent<PipeScript>().type;
                            string secondType = pipe.GetComponent<PipeScript>().type;
                            float selectedRotation = Globals.selectedPipe.transform.eulerAngles.z;
                            float secondRotation = pipe.transform.eulerAngles.z;

                            //Se intercambian atributos
                            Globals.selectedPipe.GetComponent<PipeScript>().drySprite = secondDrySprite;
                            Globals.selectedPipe.GetComponent<SpriteRenderer>().sprite = secondDrySprite;
                            Globals.selectedPipe.GetComponent<PipeScript>().waterSprite = secondWaterSprite;
                            Globals.selectedPipe.GetComponent<PipeScript>().type = secondType;
                            Globals.selectedPipe.transform.eulerAngles = new Vector3(0f, 0f, secondRotation);

                            pipe.GetComponent<PipeScript>().drySprite = selectedDrySprite;
                            pipe.GetComponent<SpriteRenderer>().sprite = selectedDrySprite;
                            pipe.GetComponent<PipeScript>().waterSprite = selectedWaterSprite;
                            pipe.GetComponent<PipeScript>().type = selectedType;
                            pipe.transform.eulerAngles = new Vector3(0f, 0f, selectedRotation);

                            //Deselecciono la pipe
                            Globals.selectedPipe.GetComponentInChildren<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
                            Globals.selectedPipe = null;
                            
                            //Reiniciamos el tablero y comprobamos el agua
                            GameObject allPipes = GameObject.Find("Pipes");
                            foreach (Transform child in allPipes.transform) {
                                child.gameObject.GetComponent<PipeScript>().hasWater = false;
                                child.gameObject.GetComponent<SpriteRenderer>().sprite = child.gameObject.GetComponent<PipeScript>().drySprite;
                            }
                            PipeScript.checkWater(GameObject.Find("startPipe"));
                        }
                    }
                }
            }
        }
    }
}
