using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectElementsFrogger : MonoBehaviour
{
    private GameObject car1;
    private GameObject car2;
    private GameObject car3;
    private GameObject car4;
    private GameObject car5;
    private GameObject home;
    private GameObject median;
    private GameObject road;
    private GameObject monedaModeloFuturo;
    private GameObject monedaModeloPrincesa;
    private GameObject monedaModeloPirata;
    // Start is called before the first frame update
    void Start()
    {
        
        if(Globals.style == Style.Pirate){
            car1 = GameObject.Find("/SceneController/Car1Group/Car1Pirate");
            car1.SetActive(true);
            car2 = GameObject.Find("/SceneController/Car2Group/Car2Pirate");
            car2.SetActive(true);
            car3 = GameObject.Find("/SceneController/Car3Group/Car3Pirate");
            car3.SetActive(true);
            car4 = GameObject.Find("/SceneController/Car4Group/Car4Pirate");
            car4.SetActive(true);
            car5 = GameObject.Find("/SceneController/Car5Group/Car5Pirate");
            car5.SetActive(true);
            home = GameObject.Find("/SceneController/HomeGroup/HomePirate");
            home.SetActive(true);
            median = GameObject.Find("/SceneController/MedianGroup/MedianPirateGroup");
            median.SetActive(true);
            road = GameObject.Find("/SceneController/RoadGroup/RoadPirateGroupMax");
            road.SetActive(true);
            monedaModeloPirata = GameObject.Find("/SceneController/MonedasModelo/PIRATACOIN");
            monedaModeloPirata.SetActive(true);
            
        }else if(Globals.style == Style.Princess){
            car1 = GameObject.Find("/SceneController/Car1Group/Car1Princess");
            car1.SetActive(true);
            car2 = GameObject.Find("/SceneController/Car2Group/Car2Princess");
            car2.SetActive(true);
            car3 = GameObject.Find("/SceneController/Car3Group/Car3Princess");
            car3.SetActive(true);
            car4 = GameObject.Find("/SceneController/Car4Group/Car4Princess");
            car4.SetActive(true);
            car5 = GameObject.Find("/SceneController/Car5Group/Car5Princess");
            car5.SetActive(true);
            home = GameObject.Find("/SceneController/HomeGroup/HomePrincess");
            home.SetActive(true);
            median = GameObject.Find("/SceneController/MedianGroup/MedianPrincessGroup");
            median.SetActive(true);
            road = GameObject.Find("/SceneController/RoadGroup/RoadPrincessGroupMax");
            road.SetActive(true);
            monedaModeloPrincesa = GameObject.Find("/SceneController/MonedasModelo/DIAMANTE");
            monedaModeloPrincesa.SetActive(true);
        }else if(Globals.style == Style.Future){
            car1 = GameObject.Find("/SceneController/Car1Group/Car1Future");
            car1.SetActive(true);
            car2 = GameObject.Find("/SceneController/Car2Group/Car2Future");
            car2.SetActive(true);
            car3 = GameObject.Find("/SceneController/Car3Group/Car3Future");
            car3.SetActive(true);
            car4 = GameObject.Find("/SceneController/Car4Group/Car4Future");
            car4.SetActive(true);
            car5 = GameObject.Find("/SceneController/Car5Group/Car5Future");
            car5.SetActive(true);
            home = GameObject.Find("/SceneController/HomeGroup/HomeFuture");
            home.SetActive(true);
            median = GameObject.Find("/SceneController/MedianGroup/MedianFutureGroup");
            median.SetActive(true);
            road = GameObject.Find("/SceneController/RoadGroup/RoadFutureGroupMax");
            road.SetActive(true);
            monedaModeloFuturo = GameObject.Find("/SceneController/MonedasModelo/CHIP");
            monedaModeloFuturo.SetActive(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
