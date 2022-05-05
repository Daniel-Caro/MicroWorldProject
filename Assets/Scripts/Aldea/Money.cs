using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{
    private GameResource gameR;
    public GameObject counter;

    // Start is called before the first frame update
    void Start()
    {
        gameR = Globals.gameResources["Coins"];
    }

    // Update is called once per frame
    void Update()
    {
        gameR = Globals.gameResources["Coins"];
        counter.GetComponent<TextMeshProUGUI>().text = gameR.currentR.ToString() + " / " + Globals.moneyCapacity.ToString();
    }
}
