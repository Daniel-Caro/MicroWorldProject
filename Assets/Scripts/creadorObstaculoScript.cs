using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creadorObstaculoScript : MonoBehaviour
{
    public float tiempoMax;
    private float tiempoInicial = 0;
    
    public GameObject obstaculoPirata;
    public GameObject obstaculoPrincesa;
    public GameObject obstaculoFuturo;
    private GameObject obstaculo;
    public float altura;
    private int probMoneda = 10;
    // Start is called before the first frame update
    void Start()
    {
        if(Globals.style == Style.Princess){
            obstaculo = obstaculoPrincesa;
        }else if (Globals.style == Style.Pirate){
            obstaculo = obstaculoPirata;
        }else if(Globals.style == Style.Future){
            obstaculo = obstaculoFuturo;
        }
        GameObject obstaculoNuevo = Instantiate(obstaculo);
        obstaculoNuevo.transform.position=transform.position+new Vector3(0,0,0);
        Destroy(obstaculoNuevo,10);
    }

    // Update is called once per frame
    void Update()
    {
        if(tiempoInicial > tiempoMax){
                GameObject obstaculoNuevo = Instantiate(obstaculo);
                GameObject ganarMoneda = obstaculoNuevo.transform.Find("sumarDinero").gameObject;
                int numProb = Random.Range(0,100);
                Debug.Log(numProb);
                Debug.Log(probMoneda);
                if(numProb<probMoneda){
                    Debug.Log("Deberia ponerse true");
                    ganarMoneda.SetActive(true);
                }else{
                    Debug.Log("Deberia ponerse false");
                    ganarMoneda.SetActive(false);
                }
                obstaculoNuevo.transform.position=transform.position+new Vector3(0,Random.Range(-altura,altura),0);
                Destroy(obstaculoNuevo,10);
                tiempoInicial = 0;
                tiempoMax = Random.Range(1,1.5f);
        }else{
            tiempoInicial  += Time.deltaTime;
        }
    }
}
