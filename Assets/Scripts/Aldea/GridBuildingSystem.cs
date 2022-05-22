 using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using TMPro;
public class GridBuildingSystem : MonoBehaviour
{
    public static GridBuildingSystem current;
    public GridLayout gridLayout;
    public Tilemap MainTileMap;
    public Tilemap TempTileMap;
    public AudioSource constructionSound;
    public AudioSource noSound;
    
    private static Dictionary<TileType, List<TileBase>> tileBases;
    
    private Building temp;
    private GameObject buildingGeneral;
    private Vector3 prevPos;
    private BoundsInt prevArea;
    private bool buildingPicked = false;

    private GameObject darkPanel;
    private GameObject openBuilds;
    public static string respuestaNombreSample;

    //Sprites
    public Sprite housePirate;
    public Sprite housePrincess;
    public Sprite houseFuture;
    public Sprite bankPirate;
    public Sprite bankPrincess;
    public Sprite bankFuture;
    public Sprite factoryPirate;
    public Sprite factoryPrincess;
    public Sprite factoryFuture;
    public Sprite townHallPirate;
    public Sprite townHallPrincess;
    public Sprite townHallFuture;
    public Sprite moneyPirate;
    public Sprite moneyPrincess;
    public Sprite moneyFuture;

    //Sounds
    public AudioSource futureMusic;
    public AudioSource pirateMusic;
    public AudioSource princessMusic;

    //Backgrounds
    public GameObject futureBackground;
    public GameObject pirateBackground;
    public GameObject princessBackground;
    
    
    #region Unity Methods

    private void Awake()
    {
        current = this;
    }

    void Start()
    {
        SavedData savedData = checkSavedGame();

        if(respuestaNombreSample != null){
            Debug.Log(respuestaNombreSample);
            GameObject.Find("/SampleSceneObject/UI/StatsBlock/Coins").GetComponent<TextMeshProUGUI>().text = respuestaNombreSample;
        }
        if(Globals.style == Style.Princess){
            GameObject.Find("/SampleSceneObject/UI/StatsBlock/diamante").SetActive(true);
        }else if(Globals.style == Style.Pirate){
            GameObject.Find("/SampleSceneObject/UI/StatsBlock/piratacoin").SetActive(true);
        }else if(Globals.style == Style.Future){
            GameObject.Find("/SampleSceneObject/UI/StatsBlock/chip").SetActive(true);
        }
        if (!Globals.buildingDataDic.ContainsKey(Globals.townHallId))
        {
            Debug.Log("Cargar diccionario edificios");
            Globals.buildingDataDic.Add(Globals.townHallId, new Dictionary<string, string>{
                {"Level", "1"},
                {"Type", "TownHall"}
            });
        }

        GameObject UI = GameObject.Find("UI");
        GameObject saveGame = UI.transform.Find("SaveGame").gameObject;
        GameObject yes = saveGame.transform.Find("Panel").transform.Find("Yes").gameObject;
        Button saveButton = yes.GetComponent<Button>();
        saveButton.onClick.RemoveListener(SaveManager.SaveGameData);
        saveButton.onClick.AddListener(SaveManager.SaveGameData);
        
        darkPanel = GameObject.Find("DarkPanel").gameObject;
        openBuilds = GameObject.Find("OpenBuilds").gameObject;

        if (Globals.tutorialStep >= 10)
        {
            darkPanel.SetActive(false);
        }
        else
        {
            GameObject.Find("OpenBuilds").gameObject.GetComponent<Button>().enabled = false;
        }


        tileBases = new Dictionary<TileType, List<TileBase>>();
        
        string tilePath = @"Tiles\";
        tileBases.Add(TileType.Empty, null);
        List<TileBase> whiteTiles = new List<TileBase>();
        List<TileBase> greenTiles = new List<TileBase>();
        List<TileBase> redtiles = new List<TileBase>();
        
        redtiles.Add(Resources.Load<TileBase>(tilePath + "red"));
        tileBases.Add(TileType.Red, redtiles);

        GameObject shop = GameObject.Find("Buildings").gameObject;

        switch(Globals.style){

            case(Style.Future): 
            {
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
                GameObject.Find("House").transform.Find("building1").gameObject.GetComponent<SpriteRenderer>().sprite = houseFuture;
                GameObject.Find("House2").transform.Find("building2").gameObject.GetComponent<SpriteRenderer>().sprite = bankFuture;
                GameObject.Find("House2").transform.Find("money").gameObject.GetComponent<SpriteRenderer>().sprite = moneyFuture;
                GameObject.Find("House3").transform.Find("building3").gameObject.GetComponent<SpriteRenderer>().sprite = factoryFuture;
                GameObject.Find("House4").transform.Find("Townhall").gameObject.GetComponent<SpriteRenderer>().sprite = townHallFuture;
                //Vector3 scale = GameObject.Find("House4").transform.Find("Townhall").transform.localScale;
                Vector3 pos = GameObject.Find("House4").transform.Find("Townhall").transform.position;
                GameObject.Find("House4").transform.Find("Townhall").transform.localScale = new Vector3(1.124288f, 1.247888f, 1.124288f);
                GameObject.Find("House4").transform.Find("Townhall").transform.localPosition = new Vector3(0.17f, 5.61f, pos.z);
                shop.transform.Find("Build1").transform.Find("BuildImage").gameObject.GetComponent<Image>().sprite = houseFuture;
                shop.transform.Find("Build2").transform.Find("BuildImage").gameObject.GetComponent<Image>().sprite = bankFuture;
                shop.transform.Find("Build3").transform.Find("BuildImage").gameObject.GetComponent<Image>().sprite = factoryFuture;
                futureMusic.gameObject.SetActive(true);
                futureBackground.SetActive(true);
                GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = new Color(137/255f,80/255f,54/255f);
                break;
            }
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
                GameObject.Find("House").transform.Find("building1").gameObject.GetComponent<SpriteRenderer>().sprite = housePirate;
                GameObject.Find("House2").transform.Find("building2").gameObject.GetComponent<SpriteRenderer>().sprite = bankPirate;
                GameObject.Find("House2").transform.Find("money").gameObject.GetComponent<SpriteRenderer>().sprite = moneyPirate;
                GameObject.Find("House3").transform.Find("building3").gameObject.GetComponent<SpriteRenderer>().sprite = factoryPirate;
                GameObject.Find("House4").transform.Find("Townhall").gameObject.GetComponent<SpriteRenderer>().sprite = townHallPirate;
                shop.transform.Find("Build1").transform.Find("BuildImage").gameObject.GetComponent<Image>().sprite = housePirate;
                shop.transform.Find("Build2").transform.Find("BuildImage").gameObject.GetComponent<Image>().sprite = bankPirate;
                shop.transform.Find("Build3").transform.Find("BuildImage").gameObject.GetComponent<Image>().sprite = factoryPirate;
                pirateMusic.gameObject.SetActive(true);
                pirateBackground.SetActive(true);
                GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = new Color(82/255f,185/255f,242/255f);
                break;
            case(Style.Princess):
            {
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
                GameObject.Find("House").transform.Find("building1").gameObject.GetComponent<SpriteRenderer>().sprite = housePrincess;
                GameObject.Find("House2").transform.Find("building2").gameObject.GetComponent<SpriteRenderer>().sprite = bankPrincess;
                GameObject.Find("House2").transform.Find("money").gameObject.GetComponent<SpriteRenderer>().sprite = moneyPrincess;
                GameObject.Find("House3").transform.Find("building3").gameObject.GetComponent<SpriteRenderer>().sprite = factoryPrincess;
                GameObject.Find("House4").transform.Find("Townhall").gameObject.GetComponent<SpriteRenderer>().sprite = townHallPrincess;
                Vector3 pos = GameObject.Find("House4").transform.Find("Townhall").transform.position;
                GameObject.Find("House4").transform.Find("Townhall").transform.position = new Vector3(pos.x, 1.1f, pos.z);
                shop.transform.Find("Build1").transform.Find("BuildImage").gameObject.GetComponent<Image>().sprite = housePrincess;
                shop.transform.Find("Build2").transform.Find("BuildImage").gameObject.GetComponent<Image>().sprite = bankPrincess;
                shop.transform.Find("Build3").transform.Find("BuildImage").gameObject.GetComponent<Image>().sprite = factoryPrincess;
                princessMusic.gameObject.SetActive(true);
                princessBackground.SetActive(true);
                GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = new Color(28/255f,143/255f,74/255f);
                break;
            }
        }
        reiniciarCollider(GameObject.Find("House").transform.Find("building1").gameObject);
        reiniciarCollider(GameObject.Find("House2").transform.Find("building2").gameObject);
        reiniciarCollider(GameObject.Find("House3").transform.Find("building3").gameObject);
        reiniciarCollider(GameObject.Find("House4").transform.Find("Townhall").gameObject);
        BoundsInt area = new BoundsInt(-15,-15,0,29,29,1);
        BoundsInt green_area = new BoundsInt(-4,-4,0,7,7,1);
        SetTilesBlock(area, TileType.White, MainTileMap);
        SetTilesBlock(green_area, TileType.Green, MainTileMap);
        shop.SetActive(false);

        if (savedData != null) placeBuildings(savedData);

        GameObject.Find("House").SetActive(false);
        GameObject.Find("House2").transform.Find("money").gameObject.SetActive(false);
        GameObject.Find("House2").SetActive(false);
        GameObject.Find("House3").SetActive(false);
        shop.SetActive(false);
        if (Globals.tutorialStep >= 14) StartCoroutine(saveDataCoroutine());
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
                if (Globals.tutorialStep == 3)
                {
                    Globals.tutorialStep++;
                    darkPanel.SetActive(true);
                    GameObject.Find("TutorialText").gameObject.GetComponent<TextMeshProUGUI>().text = "Sigue los mismos pasos para construir un banco y una fábrica";
                    GameObject.Find("Build1").gameObject.GetComponent<Button>().enabled = false;
                    GameObject.Find("Build2").gameObject.GetComponent<Button>().enabled = true;
                }
                else if (Globals.tutorialStep == 4)
                {
                    Globals.tutorialStep++;
                    GameObject.Find("Build2").gameObject.GetComponent<Button>().enabled = false;
                    GameObject.Find("Build3").gameObject.GetComponent<Button>().enabled = true;
                }
                else if (Globals.tutorialStep == 5)
                {
                    Globals.tutorialStep++;
                    darkPanel.SetActive(true);
                    GameObject.Find("Build1").gameObject.GetComponent<Button>().enabled = true;
                    GameObject.Find("Build2").gameObject.GetComponent<Button>().enabled = true;
                    GameObject.Find("TutorialText").gameObject.GetComponent<TextMeshProUGUI>().text = "Estos son los tres edificios principales con los que contarás durante tu aventura";
                }
                TempTileMap.ClearAllTiles();
                SpriteRenderer sr = temp.GetComponentInChildren<SpriteRenderer>();
                sr.color = new Color(1f,1f,1f,1f);
                Transform objectTransform = temp.GetComponent<Transform>();
                Transform spriteTransform = temp.transform.GetChild(0).gameObject.GetComponent<Transform>();
                spriteTransform.position = new Vector3(spriteTransform.position.x, spriteTransform.position.y, objectTransform.position.y/0.55f);
                constructionSound.Play();
                Globals.buildingPositions.Add(Globals.nextId, temp.transform.position);
                temp.Place();
                buildingPicked = false;
                BuildScript buildingData = buildingGeneral.GetComponent<BuildScript>();
                buildingData.id = Globals.nextId;
                Globals.nextId += 1;
                Globals.gameResources["Coins"].DedactResources(buildingData.cost);
                if (Globals.tutorialStep == 4) Globals.gameResources["Coins"].currentR = 1750;
                Dictionary<string, string> buildingDataEntry = new Dictionary<string, string>();
                buildingDataEntry.Add("Type", buildingData.type);
                buildingDataEntry.Add("Level", buildingData.level.ToString());
                Globals.buildingDataDic.Add(buildingData.id, buildingDataEntry);
                Globals.buildingTypesDic[buildingData.type].Add(buildingData.id);
                if (buildingData.type == "Bank") buildingGeneral.GetComponent<BankProduction>().BeginProducing(buildingGeneral);
                else if (buildingData.type == "Factory") {
                    buildingGeneral.GetComponent<MinionProduction>().RegisterFactory(buildingGeneral);
                }else if (buildingData.type == "House"){
                    buildingGeneral.GetComponent<MinionProduction>().RegisterHouse(buildingGeneral);
                }
                Destroy(temp);
                //Se cierra la tienda
                GameObject.Find("Buildings").SetActive(false);
                GameObject.Find("CloseBuilds").SetActive(false);
                openBuilds.SetActive(true);
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
            //buildingGeneral.transform.parent = GameObject.Find("SampleSceneObject").transform;
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

    private void reiniciarCollider(GameObject building)
    {
        Destroy(building.GetComponent<PolygonCollider2D>());
        building.AddComponent<PolygonCollider2D>();
    }
    IEnumerator saveDataCoroutine()
    {
        yield return new WaitForSeconds(15f);
        SaveManager.SaveGameData();
        Debug.Log("Se han guardado los datos");
        StartCoroutine(saveDataCoroutine());
    }
    #endregion

    private SavedData checkSavedGame()
    {
        SavedData savedData = SaveManager.LoadGameData();
        if (savedData != null) //Existen datos de guardado
        {
            Globals.style = (Style) savedData.style;
            Globals.tutorialStep = savedData.tutorialStep;
            Globals.nextId = savedData.nextId;
            Globals.gameResources["Coins"].currentR = savedData.resourcesQuantities[0];
            Globals.gameResources["Minions"].currentR = savedData.resourcesQuantities[1];
            Globals.minigameAccess["Pipes"] = savedData.minigameAccess[0];
            Globals.minigameAccess["Frogger"] = savedData.minigameAccess[1];
            Globals.minigameAccess["Association"] = savedData.minigameAccess[2];
            Globals.minigameAccess["Memory"] = savedData.minigameAccess[3];
            Globals.minigameAccess["Jumps"] = savedData.minigameAccess[4];
            Globals.minigameAccess["Flappy"] = savedData.minigameAccess[5];
            Globals.moneyCapacity = savedData.moneyCapacity;
            Globals.minionCapacity = savedData.minionCapacity;
            Globals.nombreUsuario = savedData.playerName;
            GameObject.Find("StatsBlock").transform.Find("Coins").gameObject.GetComponent<TextMeshProUGUI>().text = Globals.nombreUsuario;
            int i = 0;
            while (i < savedData.keysBuildingDataDic.Count())
            {
                if (Globals.buildingDataDic.ContainsKey(savedData.keysBuildingDataDic[i]))
                {
                    i++;
                    continue;
                }
                Globals.buildingDataDic.Add(savedData.keysBuildingDataDic[i], new Dictionary<string, string>(){
                    {"Type", savedData.typesBuildingDataDic[i]},
                    {"Level", savedData.levelsBuildingDataDic[i]}
                });
                i++;
            }
            i = 0;
            while (i < savedData.keysBuildingPositions.Count())
            {
                if (Globals.buildingPositions.ContainsKey(savedData.keysBuildingPositions[i])) 
                {
                    i++;
                    continue;
                }
                Globals.buildingPositions.Add(savedData.keysBuildingPositions[i], new Vector3(savedData.buildingPositionsX[i], savedData.buildingPositionsY[i], savedData.buildingPositionsZ[i]));
                i++;
            }
            Globals.buildingTypesDic["House"].AddRange(savedData.houseIds);
            Globals.buildingTypesDic["Bank"].AddRange(savedData.bankIds);
            Globals.buildingTypesDic["Factory"].AddRange(savedData.factoryIds);
            i = 0;
            while (i < savedData.keysBankDataDic.Count())
            {
                if (Globals.bankDataDic.ContainsKey(savedData.keysBankDataDic[i]))
                {
                    i++;
                    continue;
                }
                Globals.bankDataDic.Add(savedData.keysBankDataDic[i], new Dictionary<string, int>(){
                    {"Storage", savedData.storageBankDataDic[i]},
                    {"Quantity", savedData.quantityBankDataDic[i]},
                    {"Accumulated", savedData.accumulatedBankDataDic[i]}
                });
                i++;
            }
            i = 0;
            while (i < savedData.keysFactoryDataDic.Count())
            {
                if (Globals.factoryDataDic.ContainsKey(savedData.keysFactoryDataDic[i]))
                {
                    i++;
                    continue;
                }
                Globals.factoryDataDic.Add(savedData.keysFactoryDataDic[i], new Dictionary<int, int>(){
                    {1, savedData.tier1FactoryDataDic[i]},
                    {2, savedData.tier2FactoryDataDic[i]},
                    {3, savedData.tier3FactoryDataDic[i]},
                    {4, savedData.tier4FactoryDataDic[i]}
                });
                i++;
            }
            i = 0;
            while (i < savedData.keysColaFactoria.Count())
            {
                if (Globals.colaFactoria.ContainsKey(savedData.keysColaFactoria[i]))
                {
                    i++;
                    continue;
                }
                Globals.colaFactoria.Add(savedData.keysColaFactoria[i], savedData.valuesColaFactoria[i].ToList());
                i++;
            }
            i = 0;
            while (i < savedData.keysFactoryProducingDic.Count())
            {
                if (Globals.factoryProducingDic.ContainsKey(savedData.keysFactoryProducingDic[i]))
                {
                    i++;
                    continue;
                }
                Globals.factoryProducingDic.Add(savedData.keysFactoryProducingDic[i], savedData.valuesFactoryProducingDic[i]);
                i++;
            }
            i = 0;
            while (i < savedData.keysFactoryMinionBeingProducedDic.Count())
            {
                if (Globals.factoryMinionBeingProducedDic.ContainsKey(savedData.keysFactoryMinionBeingProducedDic[i]))
                {
                    i++;
                    continue;
                }
                Globals.factoryMinionBeingProducedDic.Add(savedData.keysFactoryMinionBeingProducedDic[i], savedData.valuesFactoryMinionBeingProducedDic[i]);
                i++;
            }
            Globals.minionsQuantity[1] = savedData.minionsQuantity[0];
            Globals.minionsQuantity[2] = savedData.minionsQuantity[1];
            Globals.minionsQuantity[3] = savedData.minionsQuantity[2];
            Globals.minionsQuantity[4] = savedData.minionsQuantity[3];
            i = 0;
            while (i < savedData.keysHouseDataDic.Count())
            {
                if (Globals.houseDataDic.ContainsKey(savedData.keysHouseDataDic[i]))
                {
                    i++;
                    continue;
                }
                Globals.houseDataDic.Add(savedData.keysHouseDataDic[i], savedData.valuesHouseDataDic[i]);
                i++;
            }
            return savedData;
        }
        else return null;
    }

    private void placeBuildings(SavedData savedData)
    {
        //Una vez cargados los datos tenemos que volver a poner los edificios
        foreach(KeyValuePair<int, Vector3> entry in Globals.buildingPositions)
        {
            GameObject building = null;
            switch (Globals.buildingDataDic[entry.Key]["Type"])
            {
                case "House":
                    building = GameObject.Find("House");
                    break;
                case "Bank":
                    building = GameObject.Find("House2");
                    break;
                case "Factory":
                    building = GameObject.Find("House3");
                    break;
            }
            GameObject realBuilding = Instantiate(building, new Vector3(entry.Value.x, entry.Value.y, entry.Value.z), Quaternion.identity);
            var temp = realBuilding.GetComponent<Building>();
            temp.Place();
            BuildScript buildingData = realBuilding.GetComponent<BuildScript>();
            buildingData.id = entry.Key;
            buildingData.level = Int32.Parse(Globals.buildingDataDic[entry.Key]["Level"]);
            var diffInSeconds = (DateTime.Now - savedData.savedTime).TotalSeconds;
            if (Globals.buildingDataDic[entry.Key]["Type"] == "Bank")
            {
                BankProduction bankInfo = realBuilding.GetComponent<BankProduction>();
                bankInfo.BeginProducing(realBuilding);
                int coinsProduced = (int) (diffInSeconds * bankInfo.quantity) / bankInfo.time;
                if (coinsProduced > 0)
                {
                    if (Globals.bankDataDic[buildingData.id]["Accumulated"] + coinsProduced > Globals.bankDataDic[buildingData.id]["Storage"]) Globals.bankDataDic[buildingData.id]["Accumulated"] = Globals.bankDataDic[buildingData.id]["Storage"];
                    else Globals.bankDataDic[buildingData.id]["Accumulated"] = Globals.bankDataDic[buildingData.id]["Accumulated"] + coinsProduced;
                }
            }
            else if (Globals.buildingDataDic[entry.Key]["Type"] == "Factory")
            {
                MinionProduction factoryInfo = realBuilding.GetComponent<MinionProduction>();
                if (savedData.minionSecondsLeft < diffInSeconds && Globals.factoryMinionBeingProducedDic[buildingData.id] != 0) //El minion que estaba en cola se ha hecho
                {
                    Globals.factoryDataDic[buildingData.id][Globals.factoryMinionBeingProducedDic[buildingData.id]]++;
                    diffInSeconds = diffInSeconds - savedData.minionSecondsLeft;
                    while (diffInSeconds >= 15f && Globals.colaFactoria[buildingData.id].Count > 0)
                    {
                        int nextMinion = Globals.colaFactoria[buildingData.id][0];
                        Globals.factoryDataDic[buildingData.id][nextMinion]++;
                        Globals.colaFactoria[buildingData.id].RemoveAt(0);
                        diffInSeconds= diffInSeconds - 15f;
                    }
                    if (Globals.colaFactoria[buildingData.id].Count > 0) //Aun quedan minions por producir
                    {
                        double timeLeft = 15f - diffInSeconds;
                        StartCoroutine(factoryInfo.produceMinionParcialTime(buildingData.id, Globals.colaFactoria[buildingData.id][0], timeLeft));
                    }
                    else
                    {
                        Globals.factoryProducingDic[buildingData.id] = false;
                    }
                }
                else if (Globals.factoryMinionBeingProducedDic[buildingData.id] != 0)
                {
                    double timeLeft = 15f - diffInSeconds;
                    StartCoroutine(factoryInfo.produceMinionParcialTime(buildingData.id, Globals.factoryMinionBeingProducedDic[buildingData.id], timeLeft));
                }
            }
            Destroy(temp);
        }
    }
    
}
public enum TileType
{
    Empty,
    White,
    Green,
    Red
}