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

    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnPosition = new Vector3();
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            int coinTrigger = Random.Range(0, 5);
            spawnPosition.y += Random.Range(minY,maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
            if (coinTrigger == 4)
            {
                newPlatform.GetComponent<platformScript>().hasCoin = true;
            }
        }
    }
}
