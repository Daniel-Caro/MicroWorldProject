using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssociationTimer : MonoBehaviour
{
    Image timerBar;
    public float maxTime = 5f;
    float timeLeft;
    public GameObject timesUpText;

    // Start is called before the first frame update
    void Start()
    {
        timesUpText.SetActive(false);
        timerBar = GetComponent<Image>();
        timeLeft = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
        } else{
            timesUpText.SetActive(true);
            //Se tiene que añadir las monedas obtenidas a la cantidad de monedas del jugador (10 monedas por cada una? 100?)
            Debug.Log("Monedas obtenidas: " + Globals.obtainedCoins);
            Globals.obtainedCoins = 0;
            Time.timeScale = 0;
        }
    }
}
