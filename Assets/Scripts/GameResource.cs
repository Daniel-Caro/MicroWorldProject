using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResource
{
    public string nameR;
    public int currentR;

    public GameResource(string nameres, int initial){
        nameR = nameres;
        currentR = initial;
    }

    public void AddResource(int quantity){
        currentR += quantity;
        if(currentR > Globals.moneyCapacity) currentR = Globals.moneyCapacity;
    }

    public void DedactResources(int quantity){
        currentR -= quantity;
        if(currentR < 0) currentR = 0;
    }

    public string Name {
        get => nameR;
    }
    public int Quantity {
        get => currentR;
    }
}
