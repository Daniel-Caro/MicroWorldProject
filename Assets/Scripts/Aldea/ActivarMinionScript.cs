using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarMinionScript : MonoBehaviour
{
    public GameObject factoria;
    public GameObject imageMinion;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(KeyValuePair<int, Dictionary<int,int>> kv in Globals.factoryDataDic){
            foreach(KeyValuePair<int,int> kv2 in kv.Value){
                if(kv2.Value >0){
                    int idFabrica = getKeyByValue(kv.Value, Globals.factoryDataDic);
                    if(factoria.GetComponent<BuildScript>().id == idFabrica){
                        imageMinion.SetActive(true);
                    }
                    //FindObject(fabrica,"pngwing.com (2)").gameObject.SetActive(true);
                }
            }
        }
    }
    private int getKeyByValue(Dictionary<int,int> value, Dictionary<int ,Dictionary<int,int>> dictionary){

        foreach (int keyVar in dictionary.Keys){
            if (dictionary[keyVar] == value)
            {
                return keyVar;
            }
        }
        return 0;

    }
}
