using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saltosVerticalesGenerator : MonoBehaviour
{
    public GameObject platformPrefab;

    public int numberOfPlatforms = 1000;
    public float levelWidth = 3f;
    public float minY = 0.2f;
    public float maxY = 1.5f;

    //Sprites platforms
    public Sprite futurePlatform;
    public Sprite futurePlatformShiny;
    public Sprite piratePlatform;
    public Sprite piratePlatformShiny;
    public Sprite princessPlatform;
    public Sprite princessPlatformShiny;

    //Sprites backgrounds
    public GameObject futureBack1;
    public GameObject futureBack2;
    public GameObject pirateBack1;
    public GameObject pirateBack2;
    public GameObject princessBack1;
    public GameObject princessBack2;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnPosition = new Vector3();
        switch (Globals.style)
        {
            case (Style.Future):
                GameObject.Find("PirateBackground1").SetActive(false);
                GameObject.Find("PrincessBackground1").SetActive(false);
                platformPrefab.GetComponent<SpriteRenderer>().sprite = futurePlatform;
                break;
            case (Style.Pirate):
                GameObject.Find("FutureBackground1").SetActive(false);
                GameObject.Find("PrincessBackground1").SetActive(false);
                platformPrefab.GetComponent<SpriteRenderer>().sprite = piratePlatform;
                break;
            case (Style.Princess):
                GameObject.Find("PirateBackground1").SetActive(false);
                GameObject.Find("FutureBackground1").SetActive(false);
                platformPrefab.GetComponent<SpriteRenderer>().sprite = princessPlatform;
                break;
        }
        for (int j = 1; j < 210; j++){
            Vector3 backgroundSpawn = new Vector3(0.09f, j*10f, 10f);
            bool bckBool = false;
            switch (Globals.style)
            {
                case (Style.Future):
                    if (bckBool) Instantiate(futureBack1, backgroundSpawn, Quaternion.identity).SetActive(true);
                    else Instantiate(futureBack2, backgroundSpawn, Quaternion.identity).SetActive(true);
                    break;
                case (Style.Pirate):
                    if (bckBool) Instantiate(pirateBack1, backgroundSpawn, Quaternion.identity).SetActive(true);
                    else Instantiate(pirateBack2, backgroundSpawn, Quaternion.identity).SetActive(true);
                    break;
                case (Style.Princess):
                    if (bckBool) Instantiate(princessBack1, backgroundSpawn, Quaternion.identity).SetActive(true);
                    else Instantiate(princessBack2, backgroundSpawn, Quaternion.identity).SetActive(true);
                    break;
            }
        }
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            int coinTrigger = Random.Range(0, 5);
            spawnPosition.y += Random.Range(minY,maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
            SpriteRenderer sr = newPlatform.GetComponent<SpriteRenderer>();
            switch (Globals.style)
            {
                case (Style.Future):
                    if (coinTrigger == 4) sr.sprite = futurePlatformShiny;
                    else sr.sprite = futurePlatform;
                    break;
                case (Style.Pirate):
                    if (coinTrigger == 4) sr.sprite = piratePlatformShiny;
                    else sr.sprite = piratePlatform;
                    break;
                case (Style.Princess):
                    if (coinTrigger == 4) sr.sprite = princessPlatformShiny;
                    else sr.sprite = princessPlatform;
                    break;
            }
            if (coinTrigger == 4)
            {
                newPlatform.GetComponent<platformScript>().hasCoin = true;
            }
        }
    }

    void Update() {
        
    }
}
