using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class ObjVoladoresScript : MonoBehaviour
{
    public GameObject nube1;
    public GameObject nube2;
    public GameObject nave;
    public GameObject pavana;

    private bool haPasado = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(haPasado == false){
            waitObjecto();
        }else{
            activacionObjetos(Globals.style);
        }
    }
    private async Task activacionObjetos(Style style){
        if(style == Style.Princess){
            nube1.SetActive(true);
            nube2.SetActive(true);
            await Task.Delay(20000);
            nube1.SetActive(false);
            nube2.SetActive(false);
            haPasado = false;
        }else if(style == Style.Pirate){
            pavana.SetActive(true);
            
            await Task.Delay(35000);
            pavana.SetActive(false);
            haPasado = false;
        }else if(style == Style.Future){
            nave.SetActive(true);
            await Task.Delay(20000);
            nave.SetActive(false);
            haPasado = false;      
            }
        
    }
    private async Task waitObjecto(){
       // Debug.Log("Pasa por aquilololo");
        await Task.Delay(10000);
        haPasado = true;
    }
}
