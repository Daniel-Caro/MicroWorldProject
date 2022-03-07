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

    public void BeginProducing(GameObject building){
        Dictionary<string, int> bankInitialProperties = new Dictionary<string, int>();
        bankInitialProperties.Add("Storage", storage);
        bankInitialProperties.Add("Quantity", quantity);
        bankInitialProperties.Add("Accumulated", 0);
        Globals.moneyCapacity += storage;
        Globals.bankDataDic.Add(building.GetInstanceID(), bankInitialProperties);
        Produce(building);
    }

    public async void Produce(GameObject building)
    {
        while (Globals.bankDataDic[building.GetInstanceID()]["Storage"] > Globals.bankDataDic[building.GetInstanceID()]["Accumulated"]) //Condicional de parada si se llena el almacenamiento
        {
            producing = true;
            await Task.Delay(TimeSpan.FromSeconds(time));
            Globals.bankDataDic[building.GetInstanceID()]["Accumulated"] += Globals.bankDataDic[building.GetInstanceID()]["Quantity"];
        }
        producing = false;
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(time);
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
}
