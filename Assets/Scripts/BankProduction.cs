using System;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class BankProduction : MonoBehaviour
{
    public int time;
    public int quantity;
    public int storage;
    private bool producing;
    private CancellationTokenSource cts;
    private CancellationToken token;

    public void BeginProducing(GameObject building){
        Dictionary<string, int> bankInitialProperties = new Dictionary<string, int>();
        bankInitialProperties.Add("Storage", storage);
        bankInitialProperties.Add("Quantity", quantity);
        bankInitialProperties.Add("Accumulated", 0);
        Globals.moneyCapacity += storage;
        Globals.bankDataDic.Add(building.GetInstanceID(), bankInitialProperties);
        cts = new CancellationTokenSource();
        token = cts.token;
        Produce(building);
    }

    public async void Produce(GameObject building, CancellationToken token)
    {
        while (Globals.bankDataDic[building.GetInstanceID()]["Storage"] > Globals.bankDataDic[building.GetInstanceID()]["Accumulated"]) //Condicional de parada si se llena el almacenamiento
        {
            producing = true;
            await Task.Delay(TimeSpan.FromSeconds(time));
            Globals.bankDataDic[building.GetInstanceID()]["Accumulated"] += Globals.bankDataDic[building.GetInstanceID()]["Quantity"];
            if(token.IsCancellationRequested) break;
        }
        producing = false;
    }
    
    public bool HarvestResource(GameObject building){
        if (Globals.bankDataDic[building.GetInstanceID()]["Accumulated"] > 0){
            int left = 0;
            if (Globals.gameResources["Coins"].currentR + Globals.bankDataDic[building.GetInstanceID()]["Accumulated"] > Globals.moneyCapacity) left = Globals.gameResources["Coins"].currentR + Globals.bankDataDic[building.GetInstanceID()]["Accumulated"] - Globals.moneyCapacity;
            Globals.gameResources["Coins"].AddResource(Globals.bankDataDic[building.GetInstanceID()]["Accumulated"]);
            Globals.bankDataDic[building.GetInstanceID()]["Accumulated"] = left;
            if(!producing) Produce(building);
            return true;
        }
        else return false;
    }

    public void chooseAttribute(GameObject building){
        GameObject choosePanel = panel.transform.Find("ChoosingPanel").gameObject;
        choosePanel.SetActive(true);
        Dictionary<string, int> bankData = Globals.bankDataDic[building.GetInstanceID()];
        int storage = bankData["Storage"];
        int quantity = bankData["Quantity"];
        GameObject storageButton = choosePanel.transform.Find("StorageButton").gameObject;
        storageButton.transform.Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = "Almacenamiento: " + storage.ToString() + " > " + (storage+50).ToString();
        GameObject quantityButton = choosePanel.transform.Find("QuantityButton").gameObject;
        quantityButton.transform.Find("Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = "Producción: " + quantity.ToString() + " > " + (quantity+50).ToString();
        storageButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        storageButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
            Globals.bankDataDic[building.GetInstanceID()]["Storage"] = storage + 50;
            Globals.moneyCapacity += 50;
            token.Cancel();
            Produce(building);
            choosePanel.SetActive(false);
        });
        quantityButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        quantityButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
            Globals.bankDataDic[building.GetInstanceID()]["Quantity"] = quantity + 50;
            token.Cancel();
            Produce(building);
            choosePanel.SetActive(false);
        });
    }
}
