using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals
{
    public static Dictionary<string, GameResource> gameResources = new Dictionary<string,GameResource>(){
        {"Coins", new GameResource("Coins", 1000)},
        {"Minioms", new GameResource("Minioms", 0)}
    };

    public static int moneyCapacity = 1000;

    public static Dictionary<int, string> buildingTypesDic = new Dictionary<int,string>(); // Clave: ID instancia edificio Valor: tipo del edificio (TownHall, Bank, Factory)
    public static Dictionary<int, int> buildingLevelsDic = new Dictionary<int,int>(); // Clave: ID instancia edificio Valor: nivel del edificio
    public static Dictionary<int, int> buildingCostsDic = new Dictionary<int,int>(); // Clave: ID instancia edificio Valor: coste subida nivel
    public static Dictionary<int, Dictionary<string, int>> bankDataDic = new Dictionary<int, Dictionary<string, int>>(); // Clave: ID instancia edificio banco Valor: diccionario con valores de almacenamiento, cantidad que genera, acumulación en el edificio
}
