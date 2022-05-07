using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstaculoScript : MonoBehaviour
{
    public float velocidad;
    public static bool enMovimiento;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enMovimiento == true){
            transform.position+=Vector3.left * velocidad*Time.deltaTime;
        }
        
    }
}
 