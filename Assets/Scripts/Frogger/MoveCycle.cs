using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCycle : MonoBehaviour
{
    public Vector2 direction = new Vector3(0.19f,0,0);
    public float speed = 1f;
    public int size = 1;
    private Vector3 leftEdge;
    private Vector3 rightEdge;
    // Start is called before the first frame update
    void Start()
    {
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
    }

    // Update is called once per frame
    void Update()
    {
        if(Frogger.haMuerto == false){
            if(direction.x > 0 &&  (transform.position.x - size) > rightEdge.x){
            Vector3 position = transform.position;
            position.x = leftEdge.x - size;
            transform.position = position;
            }
            else if(direction.x < 0 &&  (transform.position.x + size) < leftEdge.x){
                Vector3 position = transform.position;
                position.x = rightEdge.x + size;
                transform.position = position;
            }
            else{
                transform.Translate(direction * speed *Time.deltaTime);
            }
        }
        
    }
    
}
