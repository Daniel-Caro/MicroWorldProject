using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals
{
    public static Dictionary<string, GameResource> gameResources = new Dictionary<string,GameResource>(){
        {"Coins", new GameResource("Coins", 50000000)}
    };

    public static int moneyCapacity = 50000000;
    public static int minionCapacity = 2;
    public static System.Random random = new System.Random();
    public static Dictionary<int, string> buildingTypesDic = new Dictionary<int,string>(); // Clave: ID instancia edificio Valor: tipo del edificio (TownHall, Bank, Factory)
    public static Dictionary<int, int> buildingLevelsDic = new Dictionary<int,int>(); // Clave: ID instancia edificio Valor: nivel del edificio
    public static Dictionary<int, int> buildingCostsDic = new Dictionary<int,int>(); // Clave: ID instancia edificio Valor: coste subida nivel
    public static Dictionary<int, Dictionary<string, int>> bankDataDic = new Dictionary<int, Dictionary<string, int>>(); // Clave: ID instancia edificio banco Valor: diccionario con valores de almacenamiento, cantidad que genera, acumulación en el edificio
    
    public static Dictionary<int, Dictionary<int, int>> factoryDataDic = new Dictionary<int, Dictionary<int, int>>(); //
    public static Dictionary<int, List<int>> colaFactoria = new Dictionary<int, List<int>>(); // Clave ID Factoria: Lista con cola de Minions por factoria
    public static Dictionary<int,int> minionsQuantity = new Dictionary<int,int>(); // Clave Tier minion: Valor cantidad minions
    public static Dictionary<int, int> houseDataDic = new Dictionary<int, int>();
    //public static Dictionary<int,Dictionary<int,int>> minionDataDic = new Dictionary<int, Dictionary<string,int>>(); // Clave ID: Valor Cantidad que produce por minion
    //public static Dictionary<int, List<GameResource>> colaMinion = new Dictionary<int, List<GameResource>>(); // Clave ID: Valor cola de minions
   // public static Dictionary<int, int> costMinion = new Dictionary<int, int>(); // Clave Tier Minion: Valor Coste minion
    //public static Dictionary<int, int> cantidadMinions = new Dictionary<int, int>(); //Clave Tier Minion: Cantidad Minion
    
    
}
