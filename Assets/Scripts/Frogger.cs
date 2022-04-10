using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frogger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite deadSpritePirate;
    public Sprite idleSpritePirate;
    public Sprite deadSpritePrincess;
    public Sprite idleSpritePrincess;
    public Sprite deadSpriteFuture;
    public Sprite idleSpriteFuture;
    public string type;
    // Start is called before the first frame update
    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }
    void Start()
    {
        if(type == "Princess"){
            spriteRenderer.sprite = idleSpritePrincess;
        }
        else if(type == "Pirate"){
            spriteRenderer.sprite = idleSpritePirate;
        }
        else if(type == "Future"){
            spriteRenderer.sprite = idleSpriteFuture;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) ){
            Vector3 direction = new Vector3(0,0.19f,0);
            transform.rotation = Quaternion.Euler(0f,0f,0f);
            Move(direction);
        }
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) ){
            Vector3 direction = new Vector3(0,-0.19f,0);
            transform.rotation = Quaternion.Euler(0f,0f,180f);
            Move(direction);
        }
        else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) ){
            Vector3 direction = new Vector3(-0.19f,0,0);
            transform.rotation = Quaternion.Euler(0f,0f,90f);
            Move(direction);
        }
        else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) ){
            Vector3 direction = new Vector3(0.19f,0,0);
            transform.rotation = Quaternion.Euler(0f,0f,-90f);
            Move(direction);
        }
    }
    private void Move(Vector3 direction){
        Vector3 destination = transform.position + direction;
        Collider2D barrier = Physics2D.OverlapBox(destination, Vector2.zero, 0f,LayerMask.GetMask("Barrier"));
        Collider2D obstacle = Physics2D.OverlapBox(destination, Vector2.zero, 0f,LayerMask.GetMask("Obstacle"));
        if(barrier != null){
            return;
        }else{
            transform.position = destination;
        }
        if(obstacle != null){
            transform.position = destination;
            Death();
        }else{
            transform.position = destination;
        }
    }
    private void Death(){
        if(type == "Princess"){
            spriteRenderer.sprite = deadSpritePrincess;
        }
        else if(type == "Pirate"){
            spriteRenderer.sprite = deadSpritePirate;
        }
        else if(type == "Future"){
            spriteRenderer.sprite = deadSpriteFuture;
        }
        transform.rotation = Quaternion.identity;
        enabled = false;
    }
    private void Respawn(){

        enabled = true;

    }
    private void OnTriggerEnter2D(Collider2D other){
        if(enabled && other.gameObject.layer == LayerMask.NameToLayer("Obstacle")){
            Death();
        }
    }
}
