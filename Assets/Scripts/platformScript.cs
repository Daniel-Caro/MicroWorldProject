using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformScript : MonoBehaviour
{
    public float jumpForce = 10f;
    public bool hasCoin;

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.relativeVelocity.y <= 0f)
        {
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 velocity = rb.velocity;
                velocity.y = jumpForce;
                rb.velocity = velocity;
                if (hasCoin) {
                    Globals.obtainedCoins += 1;
                    hasCoin = false;
                } 
            }
        }
    }

    private void Update() 
    {
        if (transform.position.y + 5 < GameObject.Find("Player").transform.position.y) Destroy(gameObject);
    }
}
