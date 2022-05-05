using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        movement = Input.GetAxis("Horizontal") * movementSpeed;
    }

    private void FixedUpdate() 
    {
        Vector2 velocity = rb.velocity;
        velocity.x = movement;
        rb.velocity = velocity;
    }
}
