using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals
{
    public static Dictionary<string, GameResource> gameResources = new Dictionary<string,GameResource>(){
        {"Coins", new GameResource("Coins", 1000)},
        {"Minions", new GameResource("Minions", 0)}
    };

    public static int moneyCapacity = 1000;
    public static System.Random random = new System.Random();

    public static Dictionary<int, Dictionary<string, string>> buildingDataDic = new Dictionary<int, Dictionary<string, string>>(); // Clave: ID instancia edificio Valor: diccionario con valores de tipo, nivel y coste
    public static Dictionary<string, List<int>> buildingTypesDic = new Dictionary<string, List<int>>(); // Clave: tipo del edificio (House, Bank, Factory) edificio Valor: lista de ID instancia edificio
    public static Dictionary<int, Dictionary<string, int>> bankDataDic = new Dictionary<int, Dictionary<string, int>>(); // Clave: ID instancia edificio banco Valor: diccionario con valores de almacenamiento, cantidad que genera, acumulación en el edificio
}
