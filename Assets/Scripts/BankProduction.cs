using System;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class BankProduction : MonoBehaviour
{
    public int time;
    public int quantity;

    public async void BeginProducing(GameObject building){
        Globals.bankDataDic.Add(building.GetInstanceID(), new Tuple<int,int>(quantity,0));
        while (true) //Condicional de parada si se llena el almacenamiento
        {
            await Task.Delay(TimeSpan.FromSeconds(time));
            Tuple<int,int> oldEntry = Globals.bankDataDic[building.GetInstanceID()];
            Globals.bankDataDic[building.GetInstanceID()] = new Tuple<int,int>(oldEntry.Item1,oldEntry.Item2 + oldEntry.Item1);
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(time);
    }
    
    public bool HarvestResource(GameObject building){
        if (Globals.bankDataDic[building.GetInstanceID()].Item2 > 0){
            Globals.gameResources["Coins"].currentR += Globals.bankDataDic[building.GetInstanceID()].Item2;
            Globals.bankDataDic[building.GetInstanceID()] = new Tuple<int,int>(Globals.bankDataDic[building.GetInstanceID()].Item1, 0);
            return true;
        }
        else return false;
    }
}
