using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
public enum Style {Future,Pirate,Princess}
public class GridBuildingSystem : MonoBehaviour
{
    public static GridBuildingSystem current;

    public GridLayout gridLayout;
    public Tilemap MainTileMap;
    public Tilemap TempTileMap;
    public AudioSource constructionSound;
    public AudioSource noSound;
    
    private static Dictionary<TileType, List<TileBase>> tileBases = new Dictionary<TileType, List<TileBase>>();
    
    private Building temp;
    private GameObject buildingGeneral;
    private Vector3 prevPos;
    private BoundsInt prevArea;
    private bool buildingPicked = false;
    
    
    public Style style;
    #region Unity Methods

    private void Awake()
    {
        current = this;
    }

    void Start()
    {
        string tilePath = @"Tiles\";
        tileBases.Add(TileType.Empty, null);
        List<TileBase> whiteTiles = new List<TileBase>();
        List<TileBase> greenTiles = new List<TileBase>();
        List<TileBase> redtiles = new List<TileBase>();
        redtiles.Add(Resources.Load<TileBase>(tilePath + "red"));
        tileBases.Add(TileType.Red, redtiles);

        switch(style){

            case(Style.Future): 
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "temple-sliced_14"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "temple-sliced_13"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "temple-sliced_12"));
                greenTiles.Add( Resources.Load<TileBase>(tilePath + "greentilefuture"));
                tileBases.Add(TileType.White, whiteTiles);
                tileBases.Add(TileType.Green, greenTiles);
                break;
            case(Style.Pirate):
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "desert-sliced_17"));
                greenTiles.Add( Resources.Load<TileBase>(tilePath + "greentilepirate"));
                tileBases.Add(TileType.White, whiteTiles);
                tileBases.Add(TileType.Green, greenTiles);
                break;

            case(Style.Princess):
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "plains-sliced_06"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "plains-sliced_10"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "plains-sliced_02"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "plains-sliced_28"));
                greenTiles.Add( Resources.Load<TileBase>(tilePath + "greentileprincess"));
                tileBases.Add(TileType.White, whiteTiles);
                tileBases.Add(TileType.Green, greenTiles);
                break;
        }
        BoundsInt area = new BoundsInt(-24,-42,0,58,58,1);

        SetTilesBlock(area, TileType.White, MainTileMap);
    }

    void Update()
    {
        if (!temp)
        {
            return;
        }   

        if (Input.GetAxis("Mouse X")!=0 || Input.GetAxis("Mouse Y")!=0)//(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (EventSystem.current.IsPointerOverGameObject(0))
            {
                return;
            }
            if (!temp.Placed)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int cellPos = gridLayout.LocalToCell(touchPosition);

                if(prevPos != cellPos)
                {
                    temp.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPos + new Vector3(.5f, .5f, 0f));
                    prevPos = cellPos;
                    FollowBuilding();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (temp.CanBePlaced())
            {
                TempTileMap.ClearAllTiles();
                SpriteRenderer sr = temp.GetComponentInChildren<SpriteRenderer>();
                sr.color = new Color(1f,1f,1f,1f);
                Transform objectTransform = temp.GetComponent<Transform>();
                Transform spriteTransform = temp.transform.GetChild(0).gameObject.GetComponent<Transform>();
                spriteTransform.position = new Vector3(spriteTransform.position.x, spriteTransform.position.y, objectTransform.position.y/0.55f);
                constructionSound.Play();
                temp.Place();
                buildingPicked = false;
                BuildScript buildingData = buildingGeneral.GetComponent<BuildScript>();
                Globals.gameResources["Coins"].DedactResources(buildingData.cost);
                Dictionary<string, string> buildingDataEntry = new Dictionary<string, string>();
                buildingDataEntry.Add("Type", buildingData.type);
                buildingDataEntry.Add("Level", buildingData.level.ToString());
                Globals.buildingDataDic.Add(buildingGeneral.GetInstanceID(), buildingDataEntry);
                Globals.buildingTypesDic[buildingData.type].Add(buildingGeneral.GetInstanceID());
                if (buildingData.type == "Bank") buildingGeneral.GetComponent<BankProduction>().BeginProducing(buildingGeneral);
                Destroy(temp);
            }
        }
        
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (buildingPicked){
                ClearArea();
                Destroy(temp.gameObject);
                buildingPicked = false;
            }
        }
        
    }
    #endregion

    #region Building Placement

    public void InitializeWithBuilding(GameObject building)
    {
        int numHouses = Globals.buildingTypesDic["House"].Count();
        int numBanks = Globals.buildingTypesDic["Bank"].Count();
        int numFactories = Globals.buildingTypesDic["Factory"].Count();
        int limHouses = 0;
        int limBanks = 0;
        int limFactories = 0;
        switch(Int32.Parse(Globals.buildingDataDic[Globals.townHallId]["Level"]))
        {
            case(1):
                limHouses = 1;
                limBanks = 1;
                limFactories = 1;
                break;
            case(2):
                limHouses = 2;
                limBanks = 1;
                limFactories = 1;
                break;
            case(3):
                limHouses = 2;
                limBanks = 2;
                limFactories = 1;
                break;
            case(4):
                limHouses = 3;
                limBanks = 2;
                limFactories = 1;
                break;
            case(5):
                limHouses = 3;
                limBanks = 2;
                limFactories = 2;
                break;
            case(6):
                limHouses = 3;
                limBanks = 3;
                limFactories = 2;
                break;
            case(7):
                limHouses = 4;
                limBanks = 3;
                limFactories = 2;
                break;
            case(8):
                limHouses = 4;
                limBanks = 3;
                limFactories = 3;
                break;
            case(9):
                limHouses = 4;
                limBanks = 4;
                limFactories = 3;
                break;
            case(10):
                limHouses = 5;
                limBanks = 4;
                limFactories = 3;
                break;
        }
        if (Globals.gameResources["Coins"].currentR < building.GetComponent<BuildScript>().cost ||
        (building.GetComponent<BuildScript>().type == "House" && numHouses == limHouses) ||
        (building.GetComponent<BuildScript>().type == "Bank" && numBanks == limBanks) ||
        (building.GetComponent<BuildScript>().type == "Factory" && numFactories == limFactories)) noSound.Play();
        else{
            if (buildingPicked) Destroy(temp.gameObject);
            buildingPicked = true;
            buildingGeneral = Instantiate(building, new Vector3(0f, 0f, 0f), Quaternion.identity);
            temp = buildingGeneral.GetComponent<Building>();
            temp.gameObject.SetActive(true);
            SpriteRenderer sr = temp.GetComponentInChildren<SpriteRenderer>();
            sr.color = new Color(1f,1f,1f,.5f);
            FollowBuilding();
        }
    }

    private void ClearArea()
    {
        TileBase[] toClear = new TileBase[prevArea.size.x * prevArea.size.y * prevArea.size.z];
        FillTiles(toClear, TileType.Empty);
        TempTileMap.SetTilesBlock(prevArea, toClear);
    }

    private void FollowBuilding()
    {
        ClearArea();

        temp.area.position = gridLayout.WorldToCell(temp.gameObject.transform.position);
        BoundsInt buildingArea = temp.area;

        TileBase[] baseArray = GetTilesBlock(buildingArea, MainTileMap);

        int size = baseArray.Length;
        TileBase[] tileArray = new TileBase[size];

        for (int i = 0; i < baseArray.Length; i++)
        {
            if (tileBases[TileType.White].Contains(baseArray[i]))
            {
                tileArray[i] = tileBases[TileType.Green][0];
            }
            else
            {
                FillTiles(tileArray, TileType.Red);
                break;
            }
        }
        TempTileMap.SetTilesBlock(buildingArea, tileArray);
        prevArea = buildingArea;

    }

    public bool CanTakeArea(BoundsInt area)
    {
        TileBase[] baseArray = GetTilesBlock(area, MainTileMap);
        foreach (var b in baseArray)
        {
            if (!tileBases[TileType.White].Contains(b))
            {
                Debug.Log("Cannot place here");
                return false;
            }
        }
        return true;
    }

    public void TakeArea(BoundsInt area)
    {
        SetTilesBlock(area, TileType.Empty, TempTileMap);
        SetTilesBlock(area, TileType.Green, MainTileMap);
    }

    #endregion

    #region Tilemap Methods

    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach (var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }

        return array;
    }

    private static void SetTilesBlock(BoundsInt area, TileType type, Tilemap tilemap)
    {
        int size = area.size.x * area.size.y * area.size.z;
        TileBase[] tileArray = new TileBase[size];
        FillTiles(tileArray, type);
        tilemap.SetTilesBlock(area, tileArray);
    }

    private static void FillTiles(TileBase[] arr, TileType type)
    {
        for(int i = 0; i < arr.Length; i++)
        {
            switch(type){
                case(TileType.White):
                    
                    int index = Globals.random.Next(tileBases[type].Count);
                    arr[i] = tileBases[type].ElementAt(index);
                    break;
                case(TileType.Empty):
                    arr[i] = null;
                    break;
                default:
                    arr[i] = tileBases[type].First();
                    break;
            }
           
        }
    }
    #endregion
    
}
public enum TileType
{
    Empty,
    White,
    Green,
    Red
}