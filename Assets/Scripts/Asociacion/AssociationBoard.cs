using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public enum GameState
{
    WAIT, MOVE
}

public class AssociationBoard : MonoBehaviour
{
    public GameState currentState = GameState.MOVE;
    public int width;
    public int height;
    public int offSet = 20;
    public GameObject tilePrefab;
    public static GameObject coinPrefab;
    public GameObject[] dots;
    private AssociationBackgroundTile[,] allTiles;
    public GameObject[,] allDots;
    private AssociationFindMatches findMatches;
    public AudioSource coinSound;

    //Sprites de casillas
    //Pirata
    public Sprite dotRedPirate; 
    public Sprite dotYellowPirate; 
    public Sprite dotBluePirate; 
    public Sprite dotGreenPirate; 
    public Sprite dotRedPirateShiny; 
    public Sprite dotYellowPirateShiny; 
    public Sprite dotBluePirateShiny; 
    public Sprite dotGreenPirateShiny; 
    //Futuro
    public Sprite dotRedFuture; 
    public Sprite dotYellowFuture; 
    public Sprite dotBlueFuture; 
    public Sprite dotGreenFuture;
    public Sprite dotRedFutureShiny; 
    public Sprite dotYellowFutureShiny; 
    public Sprite dotBlueFutureShiny; 
    public Sprite dotGreenFutureShiny;  
    //Principe
    public Sprite dotRedPrincess; 
    public Sprite dotYellowPrincess; 
    public Sprite dotBluePrincess; 
    public Sprite dotGreenPrincess;
    public Sprite dotRedPrincessShiny; 
    public Sprite dotYellowPrincessShiny; 
    public Sprite dotBluePrincessShiny; 
    public Sprite dotGreenPrincessShiny;

    //Sounds
    public AudioSource futureMusic;
    public AudioSource pirateMusic;
    public AudioSource princessMusic;   

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("minijuego-asociacion"));

        coinPrefab = GameObject.Find("coinPrefab");
        coinPrefab.SetActive(false);
        findMatches = FindObjectOfType<AssociationFindMatches>();
        allTiles = new AssociationBackgroundTile[width, height];
        allDots = new GameObject[width, height];
        switch (Globals.style)
        {
            case Style.Pirate:
                GameObject.Find("backgroundFuture").SetActive(false);
                GameObject.Find("backgroundPrincess").SetActive(false);
                dots[0].GetComponent<SpriteRenderer>().sprite = dotRedPirate;
                dots[0].GetComponent<AssociationDot>().shinySprite = dotRedPirateShiny;
                dots[1].GetComponent<SpriteRenderer>().sprite = dotYellowPirate;
                dots[1].GetComponent<AssociationDot>().shinySprite = dotYellowPirateShiny;
                dots[2].GetComponent<SpriteRenderer>().sprite = dotBluePirate;
                dots[2].GetComponent<AssociationDot>().shinySprite = dotBluePirateShiny;
                dots[3].GetComponent<SpriteRenderer>().sprite = dotGreenPirate;
                dots[3].GetComponent<AssociationDot>().shinySprite = dotGreenPirateShiny;
                pirateMusic.gameObject.SetActive(true);
                break;
            case Style.Future:
                GameObject.Find("backgroundPrincess").SetActive(false);
                GameObject.Find("backgroundPirate").SetActive(false);
                dots[0].GetComponent<SpriteRenderer>().sprite = dotRedFuture;
                dots[0].GetComponent<AssociationDot>().shinySprite = dotRedFutureShiny;
                dots[1].GetComponent<SpriteRenderer>().sprite = dotYellowFuture;
                dots[1].GetComponent<AssociationDot>().shinySprite = dotYellowFutureShiny;
                dots[2].GetComponent<SpriteRenderer>().sprite = dotBlueFuture;
                dots[2].GetComponent<AssociationDot>().shinySprite = dotBlueFutureShiny;
                dots[3].GetComponent<SpriteRenderer>().sprite = dotGreenFuture;
                dots[3].GetComponent<AssociationDot>().shinySprite = dotGreenFutureShiny;
                futureMusic.gameObject.SetActive(true);
                break;
            case Style.Princess:
                GameObject.Find("backgroundFuture").SetActive(false);
                GameObject.Find("backgroundPirate").SetActive(false);
                dots[0].GetComponent<SpriteRenderer>().sprite = dotRedPrincess;
                dots[0].GetComponent<AssociationDot>().shinySprite = dotRedPrincessShiny;
                dots[1].GetComponent<SpriteRenderer>().sprite = dotYellowPrincess;
                dots[1].GetComponent<AssociationDot>().shinySprite = dotYellowPrincessShiny;
                dots[2].GetComponent<SpriteRenderer>().sprite = dotBluePrincess;
                dots[2].GetComponent<AssociationDot>().shinySprite = dotBluePrincessShiny;
                dots[3].GetComponent<SpriteRenderer>().sprite = dotGreenPrincess;
                dots[3].GetComponent<AssociationDot>().shinySprite = dotGreenPrincessShiny;
                princessMusic.gameObject.SetActive(true);
                break;
            default:
                break;
        }
        SetUp();
    }

    private void SetUp()
    {
        System.Random rand = new System.Random();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 tempPositionTile = new Vector3(i, j + offSet, 1);
                GameObject newTile = Instantiate(tilePrefab, tempPositionTile, Quaternion.identity);
                newTile.transform.parent = this.transform;
                newTile.name = "(" + i.ToString() + " , " + j.ToString() +")";
                newTile.SetActive(true);
                int dotToUse = UnityEngine.Random.Range(0, dots.Length);
                Vector3 tempPositionDot = new Vector3(i, j + offSet, 0);

                int maxIterations = 0;
                while (MatchesAt(i, j , dots[dotToUse]) && maxIterations < 100)
                {
                    dotToUse = UnityEngine.Random.Range(0, dots.Length);
                    maxIterations++;
                }
                maxIterations = 0;

                GameObject dot = Instantiate(dots[dotToUse], tempPositionDot, Quaternion.identity);
                dot.SetActive(true);
                dot.GetComponent<AssociationDot>().row = j;
                dot.GetComponent<AssociationDot>().column = i;
                if (UnityEngine.Random.Range(0, 7) == 6)
                {
                    dot.GetComponent<AssociationDot>().hasCoin = true; 
                    dot.GetComponent<SpriteRenderer>().sprite = dot.GetComponent<AssociationDot>().shinySprite;
                } 

                dot.transform.parent = this.transform;
                dot.name = "(" + i.ToString() + " , " + j.ToString() +")";
                allDots[i,j] = dot;
            }
        }
    }

    private bool MatchesAt(int column, int row, GameObject piece)
    {
        if (column > 1 && row > 1)
        {
            if (allDots[column - 1, row].tag == piece.tag && allDots[column - 2, row].tag == piece.tag)
            {
                return true;
            }
            if (allDots[column, row - 1].tag == piece.tag && allDots[column, row - 2].tag == piece.tag)
            {
                return true;
            }
        }
        else if (column <= 1 || row <= 1)
        {
            if (row > 1)
            {
                if (allDots[column, row - 1].tag == piece.tag && allDots[column, row - 2].tag == piece.tag)
                {
                    return true;
                }
            }
            if (column > 1)
            {
                if (allDots[column - 1, row].tag == piece.tag && allDots[column - 2, row].tag == piece.tag)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void DestroyMatchesAt(int column, int row)
    {
        if (allDots[column, row].GetComponent<AssociationDot>().isMatched)
        {
            findMatches.currentMatches.Remove(allDots[column, row]);
            if (allDots[column, row].GetComponent<AssociationDot>().hasCoin)
            {
                coinSound.Play();
                Globals.obtainedCoins++;
            }
            Destroy(allDots[column, row]);
            allDots[column, row] = null;
        }
    }

    public void DestroyMatches()
    {
        currentState = GameState.WAIT;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allDots[i, j] != null)
                {
                    DestroyMatchesAt(i, j);
                }
            }
        }
        StartCoroutine(DecreaseRowCo());
    }

    private IEnumerator DecreaseRowCo()
    {
        currentState = GameState.WAIT;
        int nullCount = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allDots[i, j] == null)
                {
                    nullCount++;
                }
                else if (nullCount > 0)
                {
                    allDots[i, j].GetComponent<AssociationDot>().row -= nullCount;
                    allDots[i, j] = null;
                }
            }
            nullCount = 0;
        }
        yield return new WaitForSeconds(.4f);
        StartCoroutine(FillBoardCo());
    }

    private void RefillBoard()
    {
         for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allDots[i, j] == null)
                {
                    Vector3 tempPosition = new Vector3(i, j + offSet, 0);
                    int dotToUse = UnityEngine.Random.Range(0, dots.Length);
                    GameObject piece = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity);
                    piece.SetActive(true);
                    allDots[i, j] = piece;
                    piece.GetComponent<AssociationDot>().row = j;
                    piece.GetComponent<AssociationDot>().column = i;
                }
            }
        }
    }

    private bool MatchesOnBoard()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allDots[i, j] != null)
                {
                    if(allDots[i, j].GetComponent<AssociationDot>().isMatched)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private IEnumerator FillBoardCo()
    {
        RefillBoard();
        yield return new WaitForSeconds(.5f);

        while(MatchesOnBoard())
        {
            currentState = GameState.WAIT;
            DestroyMatches();
            yield return new WaitForSeconds(2f);
        }

        yield return new WaitForSeconds(.5f);
        currentState = GameState.MOVE;
    }
}
