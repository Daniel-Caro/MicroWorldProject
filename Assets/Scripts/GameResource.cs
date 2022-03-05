using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResource
{
    private string nameR;
    private int currentR;

    public GameResource(string nameres, int initial){
        nameR = nameres;
        currentR = initial;
    }

    public void AddResource(int quantity){
        currentR += quantity;
        if(currentR < 0) currentR = 0;
    }

    public void DedactResource(int quantity){
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
