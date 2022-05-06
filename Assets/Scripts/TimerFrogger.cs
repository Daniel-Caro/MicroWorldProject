using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TimerFrogger : MonoBehaviour 
{
    //
    Image timeBar;
    public float maxTime;
    public static float timeLeft;
    public GameObject timeUpText;
    public GameObject frogger;
    // Start is called before the first frame update
    void Start()
    {
        timeUpText.SetActive(false);
        timeBar = GetComponent<Image>();
        timeLeft = maxTime;

    }

    // Update is called once per frame
    void Update()
    {
        if(timeLeft > 0){
            timeLeft-=Time.deltaTime;
            timeBar.fillAmount = timeLeft/maxTime;
        }else{

            frogger.SetActive(false);
            Time.timeScale = 0;
        }

    }
}