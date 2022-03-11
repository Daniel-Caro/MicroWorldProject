using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals
{
    public static Dictionary<string, GameResource> gameResources = new Dictionary<string,GameResource>(){
        {"Coins", new GameResource("Coins", 100000000)},
        {"Minions", new GameResource("Minions", 0)}
    };

    public static int moneyCapacity = 1000;
    public static int townHallId = -1840;
    public static System.Random random = new System.Random();

    public static Dictionary<int, Dictionary<string, string>> buildingDataDic = new Dictionary<int, Dictionary<string, string>>{
        {townHallId, new Dictionary<string, string>{
            {"Level", "1"},
            {"Type", "TownHall"}
        }}
    }; // Clave: ID instancia edificio Valor: diccionario con valores de tipo, nivel y coste
    public static Dictionary<string, List<int>> buildingTypesDic = new Dictionary<string, List<int>>(){
        {"House", new List<int>()},
        {"Bank", new List<int>()},
        {"Factory", new List<int>()}
    }; // Clave: tipo del edificio (House, Bank, Factory) edificio Valor: lista de ID instancia edificio
    public static Dictionary<string, List<int>> buildingCostsDic = new Dictionary<string, List<int>>(){
        {"TownHall", new List<int> {3000, 3500, 4000, 4000, 4500, 5000, 5500, 6000, 6500, 7000}},
        {"House", new List<int> {1000, 1500, 2000, 2500, 3000, 3000, 3500, 4000, 5000, 5000}},
        {"Bank", new List<int> {2000, 2500, 3000, 4000, 4500, 5000, 5000, 5500, 6000, 6000}},
        {"Factory", new List<int> {3000, 3000, 3500, 3500, 4000, 5000, 5500, 6000, 6500, 6500}}
    }; // Clave: tipo del edificio (House, Bank, Factory) edificio Valor: lista de costes por nivel
    public static Dictionary<int, Dictionary<string, int>> bankDataDic = new Dictionary<int, Dictionary<string, int>>(); // Clave: ID instancia edificio banco Valor: diccionario con valores de almacenamiento, cantidad que genera, acumulación en el edificio
}
