using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssociationBoard : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject tilePrefab;
    private AssociationBackgroundTile[,] allTiles;

    // Start is called before the first frame update
    void Start()
    {
        allTiles = new AssociationBackgroundTile[width, height];
        SetUp();
    }

    private void SetUp()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 tempPosition = new Vector2(i, j);
                GameObject newTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity);
                newTile.SetActive(true);
            }
        }
    }
}
