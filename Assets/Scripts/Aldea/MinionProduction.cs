using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinionProduction : MonoBehaviour
{

    public int time;
    public int cantidad1;
    public int cantidad2;
    public int cantidad3;
    public int cantidad4;
    public GameObject imageMinion1;
    
    /*
    public void BeginProducing(GameObject building){
        Dictionary<int, int>> factoryInitialProperties = new Dictionary<int,int>>();
        factoryInitialProperties.Add(1, 0);
        factoryInitialProperties.Add(2, 0);
        factoryInitialProperties.Add(3, 0);
        factoryInitialProperties.Add(4, 0);
        Globals.factoryDataDic.Add(building.GetComponent<BuildScript>().id, factoryInitialProperties);
        Produce(building);
    }*/
    public void RegisterFactory(GameObject building){
        Dictionary<int, int> factoryInitialProperties = new Dictionary<int,int>();
        List<int> cola = new List<int>();
        factoryInitialProperties.Add(1,0);
        factoryInitialProperties.Add(2,0);
        factoryInitialProperties.Add(3,0);
        factoryInitialProperties.Add(4,0);
        Globals.factoryDataDic.Add(building.GetComponent<BuildScript>().id, factoryInitialProperties);
        Globals.colaFactoria.Add(building.GetComponent<BuildScript>().id, cola);
        Globals.factoryProducingDic.Add(building.GetComponent<BuildScript>().id, false);
        Globals.factoryMinionBeingProducedDic.Add(building.GetComponent<BuildScript>().id,0);
        Debug.Log("El id de la nueva factoria  es"+ building.GetComponent<BuildScript>().id);
        
    }
    public void RegisterHouse(GameObject building){
        Globals.houseDataDic.Add(building.GetComponent<BuildScript>().id, 1);
        Debug.Log("El id de la nueva casa es"+ building.GetComponent<BuildScript>().id);
    }
    public async Task Produce(GameObject building, string text)
    {   
        time = 10;
        int capacityExtra = 0;
        foreach(KeyValuePair<int,int> kv in Globals.houseDataDic){
            capacityExtra += kv.Value;
        }
        int sumacolasmasacumulado = 0;
        foreach(KeyValuePair<int, List<int>> kv in Globals.colaFactoria){
            sumacolasmasacumulado += kv.Value.Count;
        }
        foreach(KeyValuePair<int, Dictionary<int,int>> kv2 in Globals.factoryDataDic){
            foreach(KeyValuePair<int,int> kv3 in kv2.Value){
                sumacolasmasacumulado += kv3.Value;
            }
        }
        if(storageComplete() == false){
           
                if (Globals.factoryProducingDic[building.GetComponent<BuildScript>().id] == true ){
                    Debug.Log("Se añade a la lista:" + text);
                    if (text.Equals("Tier1"))Globals.colaFactoria[building.GetComponent<BuildScript>().id].Add(1);
                    else if (text.Equals("Tier2"))Globals.colaFactoria[building.GetComponent<BuildScript>().id].Add(2);
                    else if (text.Equals("Tier3"))Globals.colaFactoria[building.GetComponent<BuildScript>().id].Add(3);
                    else if (text.Equals("Tier4"))Globals.colaFactoria[building.GetComponent<BuildScript>().id].Add(4);
                }else{
                    /*if(Globals.colaFactoria[building.GetComponent<BuildScript>().id].Count> 0){
                        int miniontoproduce =Globals.colaFactoria[building.GetComponent<BuildScript>().id][0];
                        if (miniontoproduce == 1){
                            Debug.Log("Se produce minion desde cola tier 1");
                            Globals.factoryProducingDic[building.GetComponent<BuildScript>().id] = true;
                            await Task.Delay(TimeSpan.FromSeconds(time)); //20 minutos 
                            Globals.factoryDataDic[building.GetComponent<BuildScript>().id][1] += 1;
                            Debug.Log(Globals.factoryDataDic[building.GetComponent<BuildScript>().id][1]);
                            Globals.colaFactoria[building.GetComponent<BuildScript>().id].RemoveAt(0);
                            Globals.factoryProducingDic[building.GetComponent<BuildScript>().id]  = false;
                        }
                        else if (miniontoproduce == 2 && Int32.Parse(Globals.buildingDataDic[building.GetComponent<BuildScript>().id]["Level"]) >= 4){
                            Debug.Log("Se produce minion desde cola tier 2");
                            Globals.factoryProducingDic[building.GetComponent<BuildScript>().id] = true;
                            await Task.Delay(TimeSpan.FromSeconds(time*60)); //20 minutos 
                            Globals.factoryDataDic[building.GetComponent<BuildScript>().id][2] += 1;
                            Debug.Log(Globals.factoryDataDic[building.GetComponent<BuildScript>().id][2]);
                            Globals.colaFactoria[building.GetComponent<BuildScript>().id].RemoveAt(0);
                            Globals.factoryProducingDic[building.GetComponent<BuildScript>().id]  = false;
                        }
                        else if (miniontoproduce== 3 && Int32.Parse(Globals.buildingDataDic[building.GetComponent<BuildScript>().id]["Level"]) >= 7){
                            Globals.factoryProducingDic[building.GetComponent<BuildScript>().id] = true;
                            Debug.Log("Se produce minion desde cola tier 3");
                            await Task.Delay(TimeSpan.FromSeconds(time*60)); //20 minutos 
                            Globals.factoryDataDic[building.GetComponent<BuildScript>().id][3] += 1;
                            Debug.Log(Globals.factoryDataDic[building.GetComponent<BuildScript>().id][2]);
                            Globals.colaFactoria[building.GetComponent<BuildScript>().id].RemoveAt(0);
                            Globals.factoryProducingDic[building.GetComponent<BuildScript>().id]  = false;
                        }
                        else if (miniontoproduce == 4 && Int32.Parse(Globals.buildingDataDic[building.GetComponent<BuildScript>().id]["Level"]) >= 10){
                            Debug.Log("Se produce minion desde cola tier 4");
                            Globals.factoryProducingDic[building.GetComponent<BuildScript>().id] = true;
                            await Task.Delay(TimeSpan.FromSeconds(time*60)); //20 minutos 
                            Globals.factoryDataDic[building.GetComponent<BuildScript>().id][4] += 1;
                            Debug.Log(Globals.factoryDataDic[building.GetComponent<BuildScript>().id][4]);
                            Globals.colaFactoria[building.GetComponent<BuildScript>().id].RemoveAt(0);
                            Globals.factoryProducingDic[building.GetComponent<BuildScript>().id]  = false;
                        }
                        else{
                            Debug.Log("No tienes el nivel suficiente para fabricar a este minion");
                        }

                    }*/
                        if (text.Equals("Tier1")){
                            Debug.Log("Se produce minion tier 1");
                            Globals.factoryProducingDic[building.GetComponent<BuildScript>().id] = true;
                            Globals.stopwatch.Start();
                            Globals.factoryMinionBeingProducedDic[building.GetComponent<BuildScript>().id] = 1;
                            await Task.Delay(TimeSpan.FromSeconds(time)); //20 minutos 
                            Globals.stopwatch.Reset();
                            Debug.Log("Se ha producido minion tier 1");
                            Globals.factoryDataDic[building.GetComponent<BuildScript>().id][1] += 1;
                            Debug.Log(Globals.factoryDataDic[building.GetComponent<BuildScript>().id][1]);
                            Globals.factoryProducingDic[building.GetComponent<BuildScript>().id]  = false;
                        }
                        else if (text.Equals("Tier2") && Int32.Parse(Globals.buildingDataDic[building.GetComponent<BuildScript>().id]["Level"])>= 4){
                            Debug.Log("Se produce minion tier 2");
                            Globals.factoryProducingDic[building.GetComponent<BuildScript>().id] = true;
                            Globals.stopwatch.Start();
                            Globals.factoryMinionBeingProducedDic[building.GetComponent<BuildScript>().id] = 2;
                            await Task.Delay(TimeSpan.FromSeconds(time)); //20 minutos
                            Globals.stopwatch.Reset();
                            Globals.factoryDataDic[building.GetComponent<BuildScript>().id][2] += 1;
                            Debug.Log(Globals.factoryDataDic[building.GetComponent<BuildScript>().id][2]);
                            Globals.factoryProducingDic[building.GetComponent<BuildScript>().id]  = false;
                        }
                        else if (text.Equals("Tier3") && Int32.Parse(Globals.buildingDataDic[building.GetComponent<BuildScript>().id]["Level"]) >= 7){
                            Globals.factoryProducingDic[building.GetComponent<BuildScript>().id] = true;
                            Debug.Log("Se produce minion tier 3");
                            Globals.stopwatch.Start();
                            Globals.factoryMinionBeingProducedDic[building.GetComponent<BuildScript>().id] = 3;
                            await Task.Delay(TimeSpan.FromSeconds(time)); //20 minutos 
                            Globals.stopwatch.Reset();
                            Globals.factoryDataDic[building.GetComponent<BuildScript>().id][3] += 1;
                            Debug.Log(Globals.factoryDataDic[building.GetComponent<BuildScript>().id][3]);
                            Globals.factoryProducingDic[building.GetComponent<BuildScript>().id]  = false;
                        }
                        else if (text.Equals("Tier4") && Int32.Parse(Globals.buildingDataDic[building.GetComponent<BuildScript>().id]["Level"]) >= 10){
                            Debug.Log("Se produce minion tier 4");
                            Globals.factoryProducingDic[building.GetComponent<BuildScript>().id] = true;
                            Globals.stopwatch.Start();
                            Globals.factoryMinionBeingProducedDic[building.GetComponent<BuildScript>().id] = 4;
                            await Task.Delay(TimeSpan.FromSeconds(time)); //20 minutos 
                            Globals.stopwatch.Reset();
                            Globals.factoryDataDic[building.GetComponent<BuildScript>().id][4] += 1;
                            Debug.Log(Globals.factoryDataDic[building.GetComponent<BuildScript>().id][4]);
                            Globals.factoryProducingDic[building.GetComponent<BuildScript>().id]  = false;
                        }
                        else{
                            StartCoroutine(Globals.popInfoMessage("No tienes el nivel suficiente para fabricar a este minion"));
                            Debug.Log("No tienes el nivel suficiente para fabricar a este minion");
                        }
                          
                
                
                }
        }else{
            if(text.Equals("Tier1")){
                Globals.gameResources["Coins"].currentR += 10;
            }else if(text.Equals("Tier2")){
                Globals.gameResources["Coins"].currentR += 30;
            }else if(text.Equals("Tier3")){
                Globals.gameResources["Coins"].currentR += 30;
            }else if(text.Equals("Tier4")){
                Globals.gameResources["Coins"].currentR += 50;
            }
            StartCoroutine(Globals.popInfoMessage("No tienes espacio para producir más"));
            Debug.Log("No tienes espacio para producir más");
        }
    }
    
    /*
    public void chooseMinion(GameObject building, GameObject panel, BuildScript buildScript){
        GameObject choosePanelMinion =panel.transform.Find("ChoosingMinion").gameObject;
        GameObject tier1button = choosePanelMinion.transform.Find("miniontier1").gameObject;
        tier1button.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        tier1button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
            
            Produce(building, "Tier1");
        });
        GameObject tier2button = choosePanelMinion.transform.Find("miniontier2").gameObject;
        tier2button.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        tier2button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
            Produce(building, "Tier2");
        });
        GameObject tier3button = choosePanelMinion.transform.Find("miniontier3").gameObject;
        tier3button.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        tier3button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
            Produce(building, "Tier3");
        });
        GameObject tier4button = choosePanelMinion.transform.Find("miniontier4").gameObject;
        tier4button.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        tier4button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
            Produce(building, "Tier4");
        });
    }*/
    public bool HarvestResource(GameObject building){
        Dictionary<int,int> buildingFactory = Globals.factoryDataDic[building.GetComponent<BuildScript>().id];
        for(int i = 1; i < buildingFactory.Count+1; i++){
            
            if(buildingFactory[i]>0) {
                return true;
            }          
        }
        return false;
        /*
        if (Globals.factoryDataDic[building.GetComponent<BuildScript>().id]["Accumulated"] > 0){ 
            
            int left = 0;
            if (Globals.gameResources["Coins"].currentR + Globals.bankDataDic[building.GetComponent<BuildScript>().id]["Accumulated"] > Globals.moneyCapacity) left = Globals.gameResources["Coins"].currentR + Globals.bankDataDic[building.GetComponent<BuildScript>().id]["Accumulated"] - Globals.moneyCapacity;
            Globals.gameResources["Coins"].AddResource(Globals.bankDataDic[building.GetComponent<BuildScript>().id]["Accumulated"]);
            Globals.bankDataDic[building.GetComponent<BuildScript>().id]["Accumulated"] = left;
           // if(!producing) Produce(building, token);
            return true;
        }*/
        
    }

    public bool storageComplete(){
        int quantityStorage = 0;
        int minionsQ = 0;
        int minionsFactory = 0;
        int minionsCola = 0;
        int minionsProd = 0;
        int totalMinions = 0;
        foreach(KeyValuePair<int, int> kv in Globals.houseDataDic){
            quantityStorage+=kv.Value;
        }
        foreach(KeyValuePair<int, int> kv in Globals.minionsQuantity){
            minionsQ += kv.Value;
        }
        foreach(KeyValuePair<int, Dictionary<int,int>> kv in Globals.factoryDataDic){
            foreach(KeyValuePair<int,int> kv2 in kv.Value){
                minionsFactory += kv2.Value;
            }
        }
        foreach(KeyValuePair<int, List<int>> kv in Globals.colaFactoria){
            minionsCola += kv.Value.Count;
        }
        foreach(KeyValuePair<int, bool> kv in Globals.factoryProducingDic){
            if(kv.Value == true){
                minionsProd += 1;
            }
        }
        Debug.Log("Minions recogidos: "+ minionsQ);
        Debug.Log("Minions en fabrica: "+ minionsFactory);
        Debug.Log("Minions en cola: "+ minionsCola);
        Debug.Log("Minions en produccion: "+ minionsProd);
        totalMinions = minionsQ + minionsFactory + minionsCola + minionsProd;
        Debug.Log("Total minions:" +totalMinions);
        int almacenamientototal = quantityStorage + Globals.minionCapacity;
        Debug.Log("Almacenamiento total" + almacenamientototal);
        if(totalMinions  <quantityStorage+ Globals.minionCapacity){
            return false;
        }
        else {
            return true;
        }
        
    }

    /*
    public bool houseComplete(int id){
        
        int nivelcasa = Globals.buildingLevelsDic[id];
        int numminions = 0;
        bool completo = false;
        Dictionary <int,int> diccionariocasa = Globals.houseDataDic[id];
        for(int i = 1; i < diccionariocasa.Count+1; i++){
            numminions += diccionariocasa[i];   
        }
        if(numminions<=nivelcasa) completo = false;
        else completo = true;
        return completo;
    }
    */
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(int factoryId in Globals.factoryDataDic.Keys){
            if(Globals.colaFactoria[factoryId].Count> 0 && Globals.factoryProducingDic[factoryId] == false){
                int miniontoproduce =Globals.colaFactoria[factoryId][0];
                Globals.factoryMinionBeingProducedDic[factoryId] = miniontoproduce;
                if (miniontoproduce == 1){
                    Debug.Log("Se produce minion desde cola tier 1");
                    Globals.factoryProducingDic[factoryId] = true;
                    Globals.colaFactoria[factoryId].RemoveAt(0);
                    StartCoroutine(waitToDo(factoryId,1));//20 minutos 
                    
                }
                else if (miniontoproduce == 2 && Int32.Parse(Globals.buildingDataDic[factoryId]["Level"]) >= 4){
                    Debug.Log("Se produce minion desde cola tier 2");
                    Globals.factoryProducingDic[factoryId] = true;
                    Globals.colaFactoria[factoryId].RemoveAt(0);
                    StartCoroutine(waitToDo(factoryId,2)); //20 minutos 
                }
                else if (miniontoproduce== 3 && Int32.Parse(Globals.buildingDataDic[factoryId]["Level"]) >= 7){
                    Globals.factoryProducingDic[factoryId] = true;
                    Debug.Log("Se produce minion desde cola tier 3");
                    Globals.colaFactoria[factoryId].RemoveAt(0);
                    StartCoroutine(waitToDo(factoryId,3)); //20 minutos 
                    
                }
                else if (miniontoproduce == 4 && Int32.Parse(Globals.buildingDataDic[factoryId]["Level"]) >= 10){
                    Debug.Log("Se produce minion desde cola tier 4");
                    Globals.factoryProducingDic[factoryId] = true;
                    Globals.colaFactoria[factoryId].RemoveAt(0);
                    StartCoroutine(waitToDo(factoryId,4)); //20 minutos 
                    
                }
            }
            else if (Globals.factoryProducingDic[factoryId] == false)
            {
                Globals.factoryMinionBeingProducedDic[factoryId] = 0;
            }   
        }


    
    }
    IEnumerator waitToDo(int factoria,int tier) {
        Globals.stopwatch.Start();
        yield return new WaitForSeconds(10f);
        Globals.stopwatch.Reset();
        Globals.factoryDataDic[factoria][tier] += 1;
        Debug.Log(Globals.factoryDataDic[factoria][1]);
        Globals.factoryProducingDic[factoria]  = false;
    }

    public IEnumerator produceMinionParcialTime(int id, int tier,double time) {
        Globals.factoryProducingDic[id]  = true;
        Globals.stopwatch.Start();
        yield return new WaitForSeconds((float)time);
        Globals.stopwatch.Reset();
        Globals.factoryDataDic[id][tier] += 1;
        Debug.Log(Globals.factoryDataDic[id][1]);
        Globals.colaFactoria[id].RemoveAt(0);
        Globals.factoryProducingDic[id]  = false;
    }
    
}