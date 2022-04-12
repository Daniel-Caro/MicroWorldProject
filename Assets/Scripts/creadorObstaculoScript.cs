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
    public string type;
    // Start is called before the first frame update
    void Start()
    {
        if(type == "Princess"){
            obstaculo = obstaculoPrincesa;
        }else if (type == "Pirate"){
            obstaculo = obstaculoPirata;
        }else if(type == "Future"){
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
                obstaculoNuevo.transform.position=transform.position+new Vector3(0,Random.Range(-altura,altura),0);
                Destroy(obstaculoNuevo,10);
                tiempoInicial = 0;
                tiempoMax = Random.Range(1,1.5f);
        }else{
            tiempoInicial  += Time.deltaTime;
        }
    }
}
