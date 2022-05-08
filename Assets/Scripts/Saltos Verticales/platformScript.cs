using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class platformScript : MonoBehaviour
{
    public float jumpForce = 10f;
    public bool hasCoin;
    public Sprite normalPlatform;
    public AudioSource coinSound;

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
                    this.GetComponent<SpriteRenderer>().sprite = normalPlatform;
                    Globals.obtainedCoins += 1;
                    hasCoin = false;
                    coinSound.Play();
                    GameObject.Find("CoinCounter").GetComponent<TextMeshProUGUI>().text = "Monedas: " + Globals.obtainedCoins.ToString();
                } 
            }
        }
    }

    private void Update() 
    {
        if (transform.position.y + 5 < GameObject.Find("Player").transform.position.y) Destroy(gameObject);
    }
}
