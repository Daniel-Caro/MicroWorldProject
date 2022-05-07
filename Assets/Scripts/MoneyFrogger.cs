using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MoneyFrogger : MonoBehaviour
{
    public GameObject moneda;
    GameObject numMonedas;
    
    // Start is called before the first frame update
    void Start()
    {
         numMonedas = GameObject.Find("/Canvas/NumMonedas").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Globals.gameResources["Coins"].currentR += 1;
            MoneyGenerateFrogger.monedasGanadas +=1;
            numMonedas.GetComponent<TextMeshProUGUI>().text = MoneyGenerateFrogger.monedasGanadas.ToString();
            moneda.SetActive(false);
            //FindObjectOfType<GameManager>().HomeOccupied();
        }
    }
}
