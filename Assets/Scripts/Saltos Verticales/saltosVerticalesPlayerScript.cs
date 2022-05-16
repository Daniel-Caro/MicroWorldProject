using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[RequireComponent(typeof(Rigidbody2D))]
public class saltosVerticalesPlayerScript : MonoBehaviour
{
    public float movementSpeed = 10f;
    Rigidbody2D rb;
    float movement = 0f;
    public Sprite futurePlayer;
    public Sprite piratePlayer;
    public Sprite princessPlayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        switch (Globals.style)
        {
            case (Style.Future):
                sr.sprite = futurePlayer;
                break;
            case (Style.Pirate):
                sr.sprite = piratePlayer;
                break;
            case (Style.Princess):
                sr.sprite = princessPlayer;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Mouse0))
        {
            var rect = new Rect(0, 0, Screen.width/2, Screen.height);
            Debug.Log("clicaste");
            if (rect.Contains(Input.mousePosition))
            {
                Debug.Log("Derecha");
                movement = -1f * movementSpeed;
            }
            else
            {
                Debug.Log("Izquierda");
                movement = 1f * movementSpeed;
            }
        }
        else
        {
            Debug.Log(Input.GetAxis("Horizontal"));
            movement = Input.GetAxis("Horizontal") * movementSpeed;
        }
    }


    private void FixedUpdate() 
    {
        Vector2 velocity = rb.velocity;
        velocity.x = movement;
        rb.velocity = velocity;
    }
}
