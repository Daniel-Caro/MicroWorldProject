using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals
{
    public static Dictionary<string, GameResource> gameResources = new Dictionary<string,GameResource>(){
        {"Coins", new GameResource("Coins", 1000)},
        {"Minioms", new GameResource("Minioms", 0)}
    };

    public static Dictionary<int, string> buildingTypesDic = new Dictionary<int,string>();
    public static Dictionary<int, int> buildingLevelsDic = new Dictionary<int,int>();
    public static Dictionary<int, int> buildingCostsDic = new Dictionary<int,int>();
}
