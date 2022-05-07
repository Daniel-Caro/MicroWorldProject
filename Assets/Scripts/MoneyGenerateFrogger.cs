using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MoneyGenerateFrogger : MonoBehaviour
{
    private int numMonedas = 15;
    private int index = 0;
    public static int monedasGanadas = 0;
    // Start is called before the first frame update
    void Start()
    {
        while(index < numMonedas){
            int monedaGenerada = Random.Range(1,32);
            if(monedaGenerada == 1){
                if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN1");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE1");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP1");
                    monedaFuturo.SetActive(true);
                }
            }else if(monedaGenerada == 2){
                if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN2");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE2");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP2");
                    monedaFuturo.SetActive(true);
                }
            }else if(monedaGenerada == 3){
                if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN3");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE3");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP3");
                    monedaFuturo.SetActive(true);
                }
            }
            else if(monedaGenerada == 4){
                if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN4");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE4");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP4");
                    monedaFuturo.SetActive(true);
                }
                
            }
            else if(monedaGenerada == 5){
                if(Globals.style == Style.Pirate){
                        GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN5");
                        monedaPirata.SetActive(true);
                    }else if(Globals.style == Style.Princess){
                        GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE5");
                        monedaPrincesa.SetActive(true);
                    }else if(Globals.style == Style.Future){
                        GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP5");
                        monedaFuturo.SetActive(true);
                    }
            }
            else if(monedaGenerada == 6){
                if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN6");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE6");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP6");
                    monedaFuturo.SetActive(true);
                }
            }
            else if(monedaGenerada == 7){
                if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN7");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE7");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP7");
                    monedaFuturo.SetActive(true);
                }
            }
            else if(monedaGenerada == 8){
                if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN8");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE8");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP9");
                    monedaFuturo.SetActive(true);
                }
            }
            else if(monedaGenerada == 9){
                if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN9");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE9");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP33");
                    monedaFuturo.SetActive(true);
                }
            }
            else if(monedaGenerada == 10){
                if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN10");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE10");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP10");
                    monedaFuturo.SetActive(true);
                }
            }
            else if(monedaGenerada == 11){
                if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN11");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE11");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP11");
                    monedaFuturo.SetActive(true);
                }
            }
            else if(monedaGenerada == 12){
                if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN12");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE12");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP12");
                    monedaFuturo.SetActive(true);
                }
            }
            else if(monedaGenerada == 13){
                if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN13");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE13");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP13");
                    monedaFuturo.SetActive(true);
                }
            }
            else if(monedaGenerada == 14){
                if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN14");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE14");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP14");
                    monedaFuturo.SetActive(true);
                }
            }
            else if(monedaGenerada == 15){
                if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN15");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE15");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP15");
                    monedaFuturo.SetActive(true);
                }
            }
            else if(monedaGenerada == 16){
                if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN16");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE16");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP16");
                    monedaFuturo.SetActive(true);
                }
            }
            else if(monedaGenerada == 17){
                if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN17");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE17");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP17");
                    monedaFuturo.SetActive(true);
                }
            }else if(monedaGenerada == 18){
                if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN18");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE18");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP18");
                    monedaFuturo.SetActive(true);
                }
            }
            else if(monedaGenerada == 19){
               if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN19");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE19");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP19");
                    monedaFuturo.SetActive(true);
                }
            }
            else if(monedaGenerada == 20){
                if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN20");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE20");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP20");
                    monedaFuturo.SetActive(true);
                }
            }
            else if(monedaGenerada == 21){
                if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN21");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE21");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP21");
                    monedaFuturo.SetActive(true);
                }
            }
            else if(monedaGenerada == 22){
                if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN22");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE22");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP22");
                    monedaFuturo.SetActive(true);
                }
            }   
            else if(monedaGenerada == 23){
                if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN23");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE23");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP23");
                    monedaFuturo.SetActive(true);
                }
            }
            else if(monedaGenerada == 24){
                if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN24");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE24");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP24");
                    monedaFuturo.SetActive(true);
                }
            }
            else if(monedaGenerada == 25){
                if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN25");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE25");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP25");
                    monedaFuturo.SetActive(true);
                }
            }
            else if(monedaGenerada == 26){
               if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN26");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE26");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP26");
                    monedaFuturo.SetActive(true);
                }
            }
            else if(monedaGenerada == 27){
               if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN27");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE27");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP27");
                    monedaFuturo.SetActive(true);
                }
            }else if(monedaGenerada == 28){
                if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN28");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE28");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP28");
                    monedaFuturo.SetActive(true);
                }
            }
            else if(monedaGenerada == 29){
                if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN29");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE29");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP29");
                    monedaFuturo.SetActive(true);
                }
            }
            else if(monedaGenerada == 30){
                if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN30");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE30");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP30");
                    monedaFuturo.SetActive(true);
                }
            } else if(monedaGenerada == 31){
                if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN31");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE31");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP31");
                    monedaFuturo.SetActive(true);
                }
            } else if(monedaGenerada == 32){
                if(Globals.style == Style.Pirate){
                    GameObject monedaPirata= GameObject.Find("/SceneController/MoneyGroup/MoneyPirataGroup/PIRATACOIN32");
                    monedaPirata.SetActive(true);
                }else if(Globals.style == Style.Princess){
                    GameObject monedaPrincesa= GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup (1)/DIAMANTE32");
                    monedaPrincesa.SetActive(true);
                }else if(Globals.style == Style.Future){
                    GameObject monedaFuturo = GameObject.Find("/SceneController/MoneyGroup/MoneyFuturoGroup/CHIP32");
                    monedaFuturo.SetActive(true);
                }
            }
            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
