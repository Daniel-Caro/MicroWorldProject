using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedData
{
    //Variables de guardado
    public int style;
    public int tutorialStep;
    public int nextId;
    public int[] resourcesQuantities = new int[2];
    public bool[] minigameAccess;
    public int moneyCapacity;
    public int minionCapacity;
    public int[] keysBuildingDataDic;
    public string[] typesBuildingDataDic;
    public string[] levelsBuildingDataDic;
    public int[] keysBuildingPositions;
    public float[] buildingPositionsX;
    public float[] buildingPositionsY;
    public float[] buildingPositionsZ;
    public int[] houseIds;
    public int[] bankIds;
    public int[] factoryIds;
    public int[] keysBankDataDic;
    public int[] storageBankDataDic;
    public int[] quantityBankDataDic;
    public int[] accumulatedBankDataDic;
    public int[] keysFactoryDataDic;
    public int[] tier1FactoryDataDic;
    public int[] tier2FactoryDataDic;
    public int[] tier3FactoryDataDic;
    public int[] tier4FactoryDataDic;
    public int[] keysColaFactoria;
    public int[][] valuesColaFactoria;
    public int[] keysFactoryProducingDic;
    public bool[] valuesFactoryProducingDic;
    public int [] keysFactoryMinionBeingProducedDic;
    public int [] valuesFactoryMinionBeingProducedDic;
    public int[] minionsQuantity;
    public int[] keysHouseDataDic;
    public int[] valuesHouseDataDic;
    public DateTime savedTime;
    public float minionSecondsLeft;

    //Constructor de clase de guardado
    public SavedData()
    {
        savedTime = DateTime.Now;
        style = (int) Globals.style;
        tutorialStep = Globals.tutorialStep;
        nextId = Globals.nextId;
        resourcesQuantities[0] = Globals.gameResources["Coins"].currentR;
        resourcesQuantities[1] = Globals.gameResources["Minions"].currentR;
        minigameAccess = new bool[Globals.minigameAccess.Count];
        Globals.minigameAccess.Values.CopyTo(minigameAccess, 0);
        moneyCapacity = Globals.moneyCapacity;
        minionCapacity = Globals.minionCapacity;
        //BuildingDataDic
        keysBuildingDataDic = new int[Globals.buildingDataDic.Count];
        Globals.buildingDataDic.Keys.CopyTo(keysBuildingDataDic, 0);
        typesBuildingDataDic = new string[Globals.buildingDataDic.Count];
        levelsBuildingDataDic = new string[Globals.buildingDataDic.Count];
        int i = 0;
        foreach(KeyValuePair<int, Dictionary<string,string>> entry in Globals.buildingDataDic)
        {
            typesBuildingDataDic[i] = entry.Value["Type"];
            levelsBuildingDataDic[i] = entry.Value["Level"];
            i++;
        }
        houseIds = Globals.buildingTypesDic["House"].ToArray();
        bankIds = Globals.buildingTypesDic["Bank"].ToArray();
        factoryIds = Globals.buildingTypesDic["Factory"].ToArray();
        //BuildingPositions
        keysBuildingPositions = new int[Globals.buildingPositions.Count];
        buildingPositionsX = new float[Globals.buildingPositions.Count];
        buildingPositionsY = new float[Globals.buildingPositions.Count];
        buildingPositionsZ = new float[Globals.buildingPositions.Count];
        Globals.buildingPositions.Keys.CopyTo(keysBuildingPositions, 0);
        i = 0;
        foreach(KeyValuePair<int, Vector3> entry in Globals.buildingPositions)
        {
            buildingPositionsX[i] = entry.Value.x;
            buildingPositionsY[i] = entry.Value.y;
            buildingPositionsZ[i] = entry.Value.z;
            i++;
        }
        //BankDataDic
        keysBankDataDic = new int[Globals.bankDataDic.Count];
        Globals.bankDataDic.Keys.CopyTo(keysBankDataDic, 0);
        storageBankDataDic = new int[Globals.bankDataDic.Count];
        quantityBankDataDic = new int[Globals.bankDataDic.Count];
        accumulatedBankDataDic = new int[Globals.bankDataDic.Count];
        i = 0;
        foreach(KeyValuePair<int, Dictionary<string,int>> entry in Globals.bankDataDic)
        {
            storageBankDataDic[i] = entry.Value["Storage"];
            quantityBankDataDic[i] = entry.Value["Quantity"];
            accumulatedBankDataDic[i] = entry.Value["Accumulated"];
            i++;
        }
        //FactoryDataDic
        keysFactoryDataDic = new int[Globals.factoryDataDic.Count];
        Globals.factoryDataDic.Keys.CopyTo(keysFactoryDataDic, 0);
        tier1FactoryDataDic = new int[Globals.factoryDataDic.Count];
        tier2FactoryDataDic = new int[Globals.factoryDataDic.Count];
        tier3FactoryDataDic = new int[Globals.factoryDataDic.Count];
        tier4FactoryDataDic = new int[Globals.factoryDataDic.Count];
        i = 0;
        foreach(KeyValuePair<int, Dictionary<int,int>> entry in Globals.factoryDataDic)
        {
            tier1FactoryDataDic[i] = entry.Value[1];
            tier2FactoryDataDic[i] = entry.Value[2];
            tier3FactoryDataDic[i] = entry.Value[3];
            tier4FactoryDataDic[i] = entry.Value[4];
            i++;
        }
        //Cola factoria
        keysColaFactoria = new int[Globals.colaFactoria.Count];
        Globals.colaFactoria.Keys.CopyTo(keysColaFactoria, 0);
        valuesColaFactoria = new int[Globals.colaFactoria.Count][];
        i = 0;
        foreach(KeyValuePair<int, List<int>> entry in Globals.colaFactoria)
        {
            valuesColaFactoria[i] = entry.Value.ToArray();
            i++;
        }
        //FactoriaProducing
        keysFactoryProducingDic = new int[Globals.factoryProducingDic.Count];
        Globals.factoryProducingDic.Keys.CopyTo(keysFactoryProducingDic, 0);
        valuesFactoryProducingDic = new bool[Globals.factoryProducingDic.Count];
        i = 0;
        foreach(KeyValuePair<int, bool> entry in Globals.factoryProducingDic)
        {
            valuesFactoryProducingDic[i] = entry.Value;
            i++;
        }
        //MinionBeingProduced
        keysFactoryMinionBeingProducedDic = new int[Globals.factoryMinionBeingProducedDic.Count];
        valuesFactoryMinionBeingProducedDic = new int[Globals.factoryMinionBeingProducedDic.Count];
        Globals.factoryMinionBeingProducedDic.Keys.CopyTo(keysFactoryMinionBeingProducedDic, 0);
        Globals.factoryMinionBeingProducedDic.Values.CopyTo(valuesFactoryMinionBeingProducedDic, 0);
        //MinionsQuiantities
        minionsQuantity = new int[Globals.minionsQuantity.Count];
        Globals.minionsQuantity.Values.CopyTo(minionsQuantity, 0);
        //HouseDataDic
        keysHouseDataDic = new int[Globals.houseDataDic.Count];
        valuesHouseDataDic = new int[Globals.houseDataDic.Count];
        Globals.houseDataDic.Values.CopyTo(valuesHouseDataDic, 0);
        Globals.houseDataDic.Keys.CopyTo(keysHouseDataDic, 0);
        if (Globals.stopwatch.IsRunning)
        {
            minionSecondsLeft =  (float)(500f - Globals.stopwatch.Elapsed.TotalSeconds);
        }
    }
}
