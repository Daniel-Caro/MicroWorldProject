using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MoneyGenerateFrogger : MonoBehaviour
{
    private int numMonedas = 5;
    private int index = 0;
    public static int monedasGanadas = 0;
    // Start is called before the first frame update
    void Start()
    {
        while(index < numMonedas){
            int monedaGenerada = Random.Range(1,30);
            if(monedaGenerada == 1){
                GameObject moneda= GameObject.Find("/SceneController/MoneyGroup/money");
                moneda.SetActive(true);
            }else if(monedaGenerada == 2){
                GameObject moneda = GameObject.Find("/SceneController/MoneyGroup/money2");
                moneda.SetActive(true);
            }else if(monedaGenerada == 3){
                GameObject moneda = GameObject.Find("/SceneController/MoneyGroup/money3");
                moneda.SetActive(true);
            }
            else if(monedaGenerada == 4){
                GameObject moneda = GameObject.Find("/SceneController/MoneyGroup/money4");
                moneda.SetActive(true);
            }
            else if(monedaGenerada == 5){
                GameObject moneda = GameObject.Find("/SceneController/MoneyGroup/money5");
                moneda.SetActive(true);
            }
            else if(monedaGenerada == 6){
                GameObject moneda = GameObject.Find("/SceneController/MoneyGroup/money6");
                moneda.SetActive(true);
            }
            else if(monedaGenerada == 7){
                GameObject moneda = GameObject.Find("/SceneController/MoneyGroup/money7");
                moneda.SetActive(true);
            }
            else if(monedaGenerada == 8){
                GameObject moneda = GameObject.Find("/SceneController/MoneyGroup/money8");
                moneda.SetActive(true);
            }
            else if(monedaGenerada == 9){
                GameObject moneda = GameObject.Find("/SceneController/MoneyGroup/money9");
                moneda.SetActive(true);
            }
            else if(monedaGenerada == 10){
                GameObject moneda = GameObject.Find("/SceneController/MoneyGroup/money10");
                moneda.SetActive(true);
            }
            else if(monedaGenerada == 11){
                GameObject moneda = GameObject.Find("/SceneController/MoneyGroup/money11");
                moneda.SetActive(true);
            }
            else if(monedaGenerada == 12){
                GameObject moneda = GameObject.Find("/SceneController/MoneyGroup/money12");
                moneda.SetActive(true);
            }
            else if(monedaGenerada == 13){
                GameObject moneda = GameObject.Find("/SceneController/MoneyGroup/money13");
                moneda.SetActive(true);
            }
            else if(monedaGenerada == 14){
                GameObject moneda = GameObject.Find("/SceneController/MoneyGroup/money14");
                moneda.SetActive(true);
            }
            else if(monedaGenerada == 15){
                GameObject moneda = GameObject.Find("/SceneController/MoneyGroup/money15");
                moneda.SetActive(true);
            }
            else if(monedaGenerada == 16){
                GameObject moneda = GameObject.Find("/SceneController/MoneyGroup/money16");
                moneda.SetActive(true);
            }
            else if(monedaGenerada == 17){
                GameObject moneda = GameObject.Find("/SceneController/MoneyGroup/money17");
                moneda.SetActive(true);
            }else if(monedaGenerada == 18){
                GameObject moneda = GameObject.Find("/SceneController/MoneyGroup/money18");
                moneda.SetActive(true);
            }
            else if(monedaGenerada == 19){
                GameObject moneda = GameObject.Find("/SceneController/MoneyGroup/money19");
                moneda.SetActive(true);
            }
            else if(monedaGenerada == 20){
                GameObject moneda = GameObject.Find("/SceneController/MoneyGroup/money20");
                moneda.SetActive(true);
            }
            else if(monedaGenerada == 21){
                GameObject moneda = GameObject.Find("/SceneController/MoneyGroup/money21");
                moneda.SetActive(true);
            }
            else if(monedaGenerada == 22){
                GameObject moneda = GameObject.Find("/SceneController/MoneyGroup/money22");
                moneda.SetActive(true);
            }
            else if(monedaGenerada == 23){
                GameObject moneda = GameObject.Find("/SceneController/MoneyGroup/money23");
                moneda.SetActive(true);
            }
            else if(monedaGenerada == 24){
                GameObject moneda = GameObject.Find("/SceneController/MoneyGroup/money24");
                moneda.SetActive(true);
            }
            else if(monedaGenerada == 25){
                GameObject moneda = GameObject.Find("/SceneController/MoneyGroup/money25");
                moneda.SetActive(true);
            }
            else if(monedaGenerada == 26){
                GameObject moneda = GameObject.Find("/SceneController/MoneyGroup/money26");
                moneda.SetActive(true);
            }
            else if(monedaGenerada == 27){
                GameObject moneda = GameObject.Find("/SceneController/MoneyGroup/money27");
                moneda.SetActive(true);
            }else if(monedaGenerada == 28){
                GameObject moneda = GameObject.Find("/SceneController/MoneyGroup/money28");
                moneda.SetActive(true);
            }
            else if(monedaGenerada == 29){
                GameObject moneda = GameObject.Find("/SceneController/MoneyGroup/money29");
                moneda.SetActive(true);
            }
            else if(monedaGenerada == 30){
                GameObject moneda = GameObject.Find("/SceneController/MoneyGroup/money30");
                moneda.SetActive(true);
            }
            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
