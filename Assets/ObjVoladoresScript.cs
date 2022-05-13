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
    private Animator animatorNave;
    private Animator animatorNube1;
    private Animator animatorNube2;
    private Animator animatorPavana;

    private bool haPasado = true;
    // Start is called before the first frame update
    void Start()
    {
        animatorNave = nave.GetComponent<Animator>();
        animatorNube1 = nube1.GetComponent<Animator>();
        animatorNube2 = nube2.GetComponent<Animator>();
        animatorPavana = pavana.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(haPasado == false){
            StartCoroutine(waitToDo());
        }else{
            activacionObjetos(Globals.style);
        }
    }
    private void activacionObjetos(Style style){
        if(style == Style.Princess){
           
            nube1.SetActive(true);
            nube2.SetActive(true);
            
            if (animatorNube1.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animatorNube1.IsInTransition(0))
            {
                nube1.SetActive(false);
                haPasado = false;
            }
            if (animatorNube2.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animatorNube2.IsInTransition(0))
            {
                nube2.SetActive(false);
                haPasado = false;
            }
            
            haPasado = false;
        }else if(style == Style.Pirate){
            
            pavana.SetActive(true);
            if (animatorPavana.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animatorPavana.IsInTransition(0))
            {
                pavana.SetActive(false);
                haPasado = false;
            }
            
        }else if(style == Style.Future){
              
            nave.SetActive(true);

            if (animatorNave.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animatorNave.IsInTransition(0))
            {
                nave.SetActive(false);
                haPasado = false;
            }
            }
        
    }
    IEnumerator waitToDo(){
        yield return new WaitForSeconds(10f);
        haPasado = true;
    }

}
