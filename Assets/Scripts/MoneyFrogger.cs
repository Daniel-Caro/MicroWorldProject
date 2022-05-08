using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
public class MoneyFrogger : MonoBehaviour
{
    public GameObject moneda;
    GameObject numMonedas;
    GameObject sonidoMoneda;
    // Start is called before the first frame update
    void Start()
    {
        numMonedas = GameObject.Find("/Canvas/NumMonedas").gameObject;
        sonidoMoneda = GameObject.Find("/SceneController/audiosFondo/audioMoneda");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sonidoMoneda.SetActive(true);
            Globals.gameResources["Coins"].currentR += 1;
            MoneyGenerateFrogger.monedasGanadas +=1;
            numMonedas.GetComponent<TextMeshProUGUI>().text = MoneyGenerateFrogger.monedasGanadas.ToString();
            moneda.SetActive(false);
            desactivarSonido();
            //FindObjectOfType<GameManager>().HomeOccupied();
        }
    }
    private async Task desactivarSonido(){
        await Task.Delay(100);
        sonidoMoneda.SetActive(false);
    }
}
