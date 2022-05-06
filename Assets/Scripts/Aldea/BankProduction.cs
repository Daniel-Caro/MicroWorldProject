using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BankProduction : MonoBehaviour
{
    public int time;
    public int quantity;
    public int storage;
    public GameObject imageCoin;
    private bool producing;
    private CancellationTokenSource cts;

    public void BeginProducing(GameObject building){
        Dictionary<string, int> bankInitialProperties = new Dictionary<string, int>();
        bankInitialProperties.Add("Storage", storage);
        bankInitialProperties.Add("Quantity", quantity);
        bankInitialProperties.Add("Accumulated", 0);
        Globals.moneyCapacity += storage;
        Globals.bankDataDic.Add(building.GetInstanceID(), bankInitialProperties);
        cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;
        Produce(building, token);
    }

    public async void Produce(GameObject building, CancellationToken token)
    {
        while (Globals.bankDataDic[building.GetInstanceID()]["Storage"] > Globals.bankDataDic[building.GetInstanceID()]["Accumulated"]) //Condicional de parada si se llena el almacenamiento
        {
            producing = true;
            await Task.Delay(TimeSpan.FromSeconds(time));
            int accumulated = Globals.bankDataDic[building.GetInstanceID()]["Accumulated"];
            int storage = Globals.bankDataDic[building.GetInstanceID()]["Storage"];
            int quantity = Globals.bankDataDic[building.GetInstanceID()]["Quantity"];
            if (accumulated + quantity > storage) Globals.bankDataDic[building.GetInstanceID()]["Accumulated"] = storage; //añadir if de almacenamiento
            else Globals.bankDataDic[building.GetInstanceID()]["Accumulated"] += quantity;

            imageCoin.SetActive(true);
            Debug.Log("activasion de monedas");

            if(token.IsCancellationRequested){
                Debug.Log("Producción cancelada");
                break;
            }
        }
        producing = false;
    }
    
    public bool HarvestResource(GameObject building){
        if (Globals.bankDataDic[building.GetInstanceID()]["Accumulated"] > 0){
            
            int left = 0;
            if (Globals.gameResources["Coins"].currentR + Globals.bankDataDic[building.GetInstanceID()]["Accumulated"] > Globals.moneyCapacity) left = Globals.gameResources["Coins"].currentR + Globals.bankDataDic[building.GetInstanceID()]["Accumulated"] - Globals.moneyCapacity;
            Globals.gameResources["Coins"].AddResource(Globals.bankDataDic[building.GetInstanceID()]["Accumulated"]);
            Globals.bankDataDic[building.GetInstanceID()]["Accumulated"] = left;
            cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            if(!producing) Produce(building, token);
            return true;
        }
        else return false;
    }

    public void chooseAttribute(GameObject building, GameObject panel, BuildScript buildScript){
        GameObject choosePanel = panel.transform.Find("ChoosingPanel").gameObject;
        choosePanel.SetActive(true);
        Dictionary<string, int> bankData = Globals.bankDataDic[building.GetInstanceID()];
        int storage = bankData["Storage"];
        int quantity = bankData["Quantity"];
        GameObject storageButton = choosePanel.transform.Find("StorageButton").gameObject;
        storageButton.transform.Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = storage.ToString() + " > " + (storage+500).ToString();
        GameObject quantityButton = choosePanel.transform.Find("QuantityButton").gameObject;
        quantityButton.transform.Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = quantity.ToString() + " > " + (quantity+3).ToString();
        storageButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        storageButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
            Globals.bankDataDic[building.GetInstanceID()]["Storage"] = storage + 500;
            Globals.moneyCapacity += 500;
            cts.Cancel();
            cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            Produce(building,token);
            choosePanel.SetActive(false);
            buildScript.LevelUp();
        });
        quantityButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        quantityButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
            Globals.bankDataDic[building.GetInstanceID()]["Quantity"] = quantity + 3;
            cts.Cancel();
            cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            Produce(building, token);
            choosePanel.SetActive(false);
            buildScript.LevelUp();
        });
    }
}
