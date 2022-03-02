using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals
{
    public static Dictionary<string, GameResource> gameResources = new Dictionary<string,GameResource>(){
        {"Coins", new GameResource("Coins", 0)},
        {"Minioms", new GameResource("Minioms", 0)}
    };
}
