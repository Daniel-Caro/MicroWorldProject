using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
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

        switch(Globals.style){

            case(Style.Future): 
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "future_alter1"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "future_alter2"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "future_basic"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "future_basic"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "future_basic"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "future_basic"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "future_basic"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "future_basic2"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "future_basic2"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "future_basic2"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "future_basic3"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "future_basic3"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "future_basic3"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "future_basic3"));
                greenTiles.Add( Resources.Load<TileBase>(tilePath + "future_green"));
                tileBases.Add(TileType.White, whiteTiles);
                tileBases.Add(TileType.Green, greenTiles);
                break;
            case(Style.Pirate):
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "arena_concha"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "arena_estrella"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "arena_textura"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "arena_textura"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "arena_textura"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "arena_textura"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "arena_textura"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "arena_textura"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "arena_textura"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "arena_textura"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "arena_textura"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "arena_textura"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "arena_textura"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "arena_textura"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "arena_textura"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "arena_textura"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "arena_textura"));
                greenTiles.Add( Resources.Load<TileBase>(tilePath + "arena_green"));
                tileBases.Add(TileType.White, whiteTiles);
                tileBases.Add(TileType.Green, greenTiles);
                break;

            case(Style.Princess):
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "hierba_matojo"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "hierba_matojo"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "hierba_matojo"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "hierba_matojo"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "hierba_matojo"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "hierba_matojo"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "hierba_matojo"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "hierba_normal"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "hierba_normal"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "hierba_normal"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "hierba_normal"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "hierba_normal"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "hierba_normal"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "hierba_normal"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "hierba_normal"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "hierba_normal"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "hierba_normal"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "hierba_normal"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "hierba_normal"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "hierba_normal"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "hierba_normal"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "hierba_normal"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "hierba_normal"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "hierba_normal"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "hierba_normal"));
                whiteTiles.Add( Resources.Load<TileBase>(tilePath + "hierba_tocon"));
                greenTiles.Add( Resources.Load<TileBase>(tilePath + "hierba_green"));
                tileBases.Add(TileType.White, whiteTiles);
                tileBases.Add(TileType.Green, greenTiles);
                break;
        }
        BoundsInt area = new BoundsInt(-15,-15,0,29,29,1);
        BoundsInt green_area = new BoundsInt(-4,-4,0,7,7,1);
        SetTilesBlock(area, TileType.White, MainTileMap);
        SetTilesBlock(green_area, TileType.Green, MainTileMap);
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
                else if (buildingData.type == "Factory") {
                    buildingGeneral.GetComponent<MinionProduction>().RegisterFactory(buildingGeneral);
                }else if (buildingData.type == "House"){
                    buildingGeneral.GetComponent<MinionProduction>().RegisterHouse(buildingGeneral);
                }
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
        Dictionary<string, int> limByLevel = Globals.numBuildingsByLevel[Int32.Parse(Globals.buildingDataDic[Globals.townHallId]["Level"])];
        int limHouses = limByLevel["House"];
        int limBanks = limByLevel["Bank"];;
        int limFactories = limByLevel["Factory"];;

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

                /*case(TileType.Green):
                    int ind = Globals.random.Next(tileBases[type].Count);
                    arr[i] = tileBases[type].ElementAt(ind);
                    break;*/

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