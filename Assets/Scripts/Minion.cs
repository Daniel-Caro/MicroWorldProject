using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Minion : MonoBehaviour
{
    private int gameR1;
    private int gameR2;
    private int gameR3;
    private int gameR4;
    public GameObject counter;
   
    // Start is called before the first frame update
    void Start()
    {
        if(Globals.minionsQuantity.ContainsKey(1)) gameR1 = Globals.minionsQuantity[1];
        else Globals.minionsQuantity.Add(1,0);gameR1 = Globals.minionsQuantity[1];
        if(Globals.minionsQuantity.ContainsKey(2)) gameR2 = Globals.minionsQuantity[2];
        else Globals.minionsQuantity.Add(2,0);gameR2 = Globals.minionsQuantity[2];
        if(Globals.minionsQuantity.ContainsKey(3)) gameR3 = Globals.minionsQuantity[3];
        else Globals.minionsQuantity.Add(3,0);gameR3 = Globals.minionsQuantity[3];
        if(Globals.minionsQuantity.ContainsKey(4)) gameR4 = Globals.minionsQuantity[4];
        else Globals.minionsQuantity.Add(4,0); gameR4 = Globals.minionsQuantity[4];
    }

    // Update is called once per frame
    void Update()
    {
        if(Globals.minionsQuantity.ContainsKey(1)) gameR1 = Globals.minionsQuantity[1]; 
        else Globals.minionsQuantity.Add(1,0);gameR1 = Globals.minionsQuantity[1];
        if(Globals.minionsQuantity.ContainsKey(2)) gameR2 = Globals.minionsQuantity[2];
        else Globals.minionsQuantity.Add(2,0);gameR2 = Globals.minionsQuantity[2];
        if(Globals.minionsQuantity.ContainsKey(3)) gameR3 = Globals.minionsQuantity[3];
        else Globals.minionsQuantity.Add(3,0);gameR3 = Globals.minionsQuantity[3];
        if(Globals.minionsQuantity.ContainsKey(4)) gameR4 = Globals.minionsQuantity[4];
        else Globals.minionsQuantity.Add(4,0); gameR4 = Globals.minionsQuantity[4];
        int gameRt = gameR1 + gameR2 + gameR3 + gameR4;
        int capacityExtra = 0;
        foreach(KeyValuePair<int,int> kv in Globals.houseDataDic){
            capacityExtra += kv.Value;
        }
        counter.GetComponent<TextMeshProUGUI>().text = gameRt.ToString()+" / "+ (Globals.minionCapacity+capacityExtra).ToString();
        
    }
}
