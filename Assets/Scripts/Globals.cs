using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public enum Style {Future,Pirate,Princess}

public class Globals
{

    public static Style style = Style.Future;


    public static int tutorialStep = 15;

    public static Dictionary<string, GameResource> gameResources = new Dictionary<string,GameResource>() {
        {"Coins", new GameResource("Coins", 50000)},
        {"Minions", new GameResource("Minions", 0)}
    };

    public static Dictionary<string, bool> minigameAccess = new Dictionary<string,bool>() {
        {"Pipes", false},
        {"Frogger", false},
        {"Association", false},
        {"Memory", false},
        {"Jumps", false},
        {"Flappy", false},
    };

    public static bool doubleCoinsBoost = false;
    public static bool restartGameBoost = false;

    public static int moneyCapacity = 100000;
    public static int minionCapacity = 2;
    public static int townHallId = 0;
    public static int nextId = 1;
    public static System.Random random = new System.Random();
    public static Stopwatch stopwatch = new Stopwatch();


    public static Dictionary<int, Dictionary<string, string>> buildingDataDic = new Dictionary<int, Dictionary<string, string>>();  // Clave: ID instancia edificio Valor: diccionario con valores de tipo, nivel y coste
    public static Dictionary<int, Vector3> buildingPositions = new Dictionary<int, Vector3>();
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

    public static Dictionary<int, Dictionary<string, int>> numBuildingsByLevel = new Dictionary<int, Dictionary<string, int>>{
        {1, new Dictionary<string, int>{{"House", 1}, {"Bank", 1}, {"Factory", 1}}},
        {2, new Dictionary<string, int>{{"House", 2}, {"Bank", 1}, {"Factory", 1}}},
        {3, new Dictionary<string, int>{{"House", 2}, {"Bank", 2}, {"Factory", 1}}},
        {4, new Dictionary<string, int>{{"House", 3}, {"Bank", 2}, {"Factory", 1}}},
        {5, new Dictionary<string, int>{{"House", 3}, {"Bank", 2}, {"Factory", 2}}},
        {6, new Dictionary<string, int>{{"House", 3}, {"Bank", 3}, {"Factory", 2}}},
        {7, new Dictionary<string, int>{{"House", 4}, {"Bank", 3}, {"Factory", 2}}},
        {8, new Dictionary<string, int>{{"House", 4}, {"Bank", 3}, {"Factory", 3}}},
        {9, new Dictionary<string, int>{{"House", 4}, {"Bank", 4}, {"Factory", 3}}},
        {10, new Dictionary<string, int>{{"House", 5}, {"Bank", 4}, {"Factory", 3}}}
    };

    public static Dictionary<int, Dictionary<string, int>> bankDataDic = new Dictionary<int, Dictionary<string, int>>(); // Clave: ID instancia edificio banco Valor: diccionario con valores de almacenamiento, cantidad que genera, acumulación en el edificio
    public static Dictionary<int, Dictionary<int, int>> factoryDataDic = new Dictionary<int, Dictionary<int, int>>(); //Minions que contiene la factoria
  
    public static Dictionary<int, List<int>> colaFactoria = new Dictionary<int, List<int>>(); // Clave ID Factoria: Lista con cola de Minions por factoria
    public static Dictionary<int,int> minionsQuantity = new Dictionary<int,int>(); // Clave Tier minion: Valor cantidad minions
    public static Dictionary<int, int> houseDataDic = new Dictionary<int, int>(); //
    public static Dictionary<int,bool> factoryProducingDic = new Dictionary<int,bool>(); //Clave ID Factoria: Valor Si esta produciendo en ese momento
    public static Dictionary<int,int> factoryMinionBeingProducedDic = new Dictionary<int,int>();
    
    //VARIABLES COMUNES MINIJUEGOS
    public static int obtainedCoins;


    //MINIJUEGO TUBERIAS
    public static GameObject selectedPipe = null;



}
