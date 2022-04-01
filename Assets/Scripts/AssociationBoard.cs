using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssociationBoard : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject tilePrefab;
    public GameObject[] dots;
    private AssociationBackgroundTile[,] allTiles;
    public GameObject[,] allDots;

    // Start is called before the first frame update
    void Start()
    {
        allTiles = new AssociationBackgroundTile[width, height];
        allDots = new GameObject[width, height];
        SetUp();
    }

    private void SetUp()
    {
        System.Random rand = new System.Random();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 tempPositionTile = new Vector3(i, j, 1);
                GameObject newTile = Instantiate(tilePrefab, tempPositionTile, Quaternion.identity);
                newTile.transform.parent = this.transform;
                newTile.name = "(" + i.ToString() + " , " + j.ToString() +")";
                newTile.SetActive(true);
                int dotToUse = UnityEngine.Random.Range(0, dots.Length);
                Vector3 tempPositionDot = new Vector3(i, j, 0);
                GameObject dot = Instantiate(dots[dotToUse], tempPositionDot, Quaternion.identity);
                dot.SetActive(true);
                dot.transform.parent = this.transform;
                dot.name = "(" + i.ToString() + " , " + j.ToString() +")";
                allDots[i,j] = dot;
            }
        }
    }
}
