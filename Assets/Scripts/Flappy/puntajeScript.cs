using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class puntajeScript : MonoBehaviour
{
    public static int monedasGanadas = 0;
    // Start is called before the first frame update
    void Start()
    {
        monedasGanadas = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = monedasGanadas.ToString();
        
    }
    
    public static void meterMonedas(){
        Globals.gameResources["Coins"].currentR += monedasGanadas;
        Debug.Log(Globals.gameResources["Coins"].currentR);
    }
}
