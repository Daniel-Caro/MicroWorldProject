using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class personajeScript : MonoBehaviour
{
    public float velocity = 3;
    private Rigidbody2D rb;
    public ControladorEscena controladorEscena;
    private float g;
    private bool firstJump = true;
    private GameObject textoEmpezar;
    // Start is called before the first frame update
    void Start()
    {
        textoEmpezar = GameObject.Find("/SceneController/CanvasPuntaje/pulsaEmpezar");
        rb = GetComponent<Rigidbody2D>();
        g = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(0,0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            textoEmpezar.SetActive(false);
            if(firstJump == true){
                rb.gravityScale = g;
                firstJump = false;
            }
            rb.velocity = Vector2.up * velocity;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision){
        controladorEscena.youLose();
    }
    
}
