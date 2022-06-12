using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TuberiasGameManager : MonoBehaviour
{
    public static GameObject PipesHolder;
    public static GameObject coinPrefab;
    public static GameObject[] Pipes;
    public static GameObject[] noWaterPipes;
    public GameObject timeUpText;
    public Sprite coinSpriteFuture;
    public Sprite coinSpritePirate;
    public Sprite coinSpritePrincess;

    //Sounds
    public AudioSource futureMusic;
    public AudioSource pirateMusic;
    public AudioSource princessMusic; 

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Tuberias"));

        //Inicio de variables estáticas
        PipesHolder = GameObject.Find("Pipes");
        GameObject drySpriteHolder = null;
        coinPrefab = GameObject.Find("coinPrefab");
        coinPrefab.SetActive(false);
        switch (Globals.style)
        {
            case(Style.Future):
                GameObject.Find("startPipePirate").SetActive(false);
                GameObject.Find("emptyPipesPirate").SetActive(false);
                GameObject.Find("PirateBackground").SetActive(false);
                GameObject.Find("startPipePrincess").SetActive(false);
                GameObject.Find("emptyPipesPrincess").SetActive(false);
                GameObject.Find("PrincessBackground").SetActive(false);
                drySpriteHolder = GameObject.Find("emptyPipesFuture");
                coinPrefab.GetComponent<SpriteRenderer>().sprite = coinSpriteFuture;
                futureMusic.gameObject.SetActive(true);
                break;
            case(Style.Pirate):
                GameObject.Find("startPipePrincess").SetActive(false);
                GameObject.Find("emptyPipesPrincess").SetActive(false);
                GameObject.Find("PrincessBackground").SetActive(false);
                GameObject.Find("startPipeFuture").SetActive(false);
                GameObject.Find("emptyPipesFuture").SetActive(false);
                GameObject.Find("FutureBackground").SetActive(false);
                drySpriteHolder = GameObject.Find("emptyPipesPirate");
                coinPrefab.GetComponent<SpriteRenderer>().sprite = coinSpritePirate;
                pirateMusic.gameObject.SetActive(true);
                break;
            case(Style.Princess):
                GameObject.Find("startPipePirate").SetActive(false);
                GameObject.Find("emptyPipesPirate").SetActive(false);
                GameObject.Find("PirateBackground").SetActive(false);
                GameObject.Find("startPipeFuture").SetActive(false);
                GameObject.Find("emptyPipesFuture").SetActive(false);
                GameObject.Find("FutureBackground").SetActive(false);
                drySpriteHolder = GameObject.Find("emptyPipesPrincess");
                coinPrefab.GetComponent<SpriteRenderer>().sprite = coinSpritePrincess;
                princessMusic.gameObject.SetActive(true);
                break;
        }
        noWaterPipes = new GameObject[4];
        int j = 0;
        foreach (Transform child in drySpriteHolder.transform) {
            noWaterPipes[j] = child.gameObject;
            j++;
        }
        drySpriteHolder.SetActive(false);
        
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
                int coinTrigger = Random.Range(0, 6);
                GameObject newPipe = Instantiate(noWaterPipes[rand], new Vector3(x, y, -1f), Quaternion.identity);
                newPipe.transform.localScale = new Vector3(0.3f, 0.3f, 1);
                newPipe.transform.parent = PipesHolder.transform;
                if (coinTrigger == 5)
                {
                    GameObject coin = Instantiate(coinPrefab, new Vector3(x, y, -2f), Quaternion.identity);
                    coin.SetActive(true);
                    newPipe.GetComponent<PipeScript>().hasCoin = true;
                } 
                x += 1.5f;
            }
            x = -6f;
            y -= 1.5f;
        }

        Pipes = new GameObject[PipesHolder.transform.childCount];

        for (int i = 0; i < Pipes.Length; i++)
        {
            Pipes[i] = PipesHolder.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButtonDown(1))
        {
            if (timeUpText.activeSelf) return;
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                GameObject pipe = hit.collider.gameObject;
                if(pipe.name != "startPipePirate" && pipe.name != "startPipePrincess" && pipe.name != "startPipeFuture"){
                    if (Globals.selectedPipe == null)
                    {
                        Globals.selectedPipe = pipe;
                        pipe.GetComponentInChildren<SpriteRenderer>().color = new Color(1f,1f,1f,.6f);
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
                            GameObject startPipe = null;
                            switch(Globals.style)
                            {
                                case(Style.Future):
                                    startPipe = GameObject.Find("startPipeFuture");
                                    break;
                                case(Style.Pirate):
                                    startPipe = GameObject.Find("startPipePirate");
                                    break;
                                case(Style.Princess):
                                    startPipe = GameObject.Find("startPipePrincess");
                                    break;
                            }
                            PipeScript.checkWater(startPipe);
                        }
                    }
                }
            }
        }
    }

    public static void RestartGame(){
        GameObject pipes = GameObject.Find("Pipes");
        foreach (Transform child in pipes.transform) {
            if (child.gameObject.GetComponent<PipeScript>().hasCoin && child.gameObject.GetComponent<PipeScript>().hasWater) Globals.obtainedCoins++;
            Destroy(child.gameObject);
        }
        GameObject.Find("CoinCounter").GetComponent<TextMeshProUGUI>().text = "Monedas: " + Globals.obtainedCoins.ToString();
        GameObject startPipe = null;
        switch(Globals.style)
        {
            case(Style.Future):
                startPipe = GameObject.Find("startPipeFuture");
                break;
            case(Style.Pirate):
                startPipe = GameObject.Find("startPipePirate");
                break;
            case(Style.Princess):
                startPipe = GameObject.Find("startPipePrincess");
                break;
        }
        startPipe.transform.eulerAngles = new Vector3(0f, 0f, 0F);
        startPipe.GetComponent<PipeScript>().hasWater = false;
        startPipe.GetComponent<SpriteRenderer>().sprite = startPipe.GetComponent<PipeScript>().drySprite;

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
                int coinTrigger = Random.Range(0, 6);
                GameObject newPipe = Instantiate(noWaterPipes[rand], new Vector3(x, y, -1f), Quaternion.identity);
                newPipe.transform.localScale = new Vector3(0.3f, 0.3f, 1);
                newPipe.transform.parent = PipesHolder.transform;
                if (coinTrigger == 5)
                {
                    GameObject coin = Instantiate(coinPrefab, new Vector3(x, y, -2f), Quaternion.identity);
                    coin.SetActive(true);
                    newPipe.GetComponent<PipeScript>().hasCoin = true;
                } 
                x += 1.5f;
            }
            x = -6f;
            y -= 1.5f;
        }

        Pipes = new GameObject[PipesHolder.transform.childCount];

        for (int i = 0; i < Pipes.Length; i++)
        {
            Pipes[i] = PipesHolder.transform.GetChild(i).gameObject;
        }
    }
}
