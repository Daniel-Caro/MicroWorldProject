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
                if ((y == 3f && x == -6f) || (y == -3f && x == 6f))
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
    void Update()
    {
        
    }
}
