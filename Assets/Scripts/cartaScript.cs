using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cartaScript : MonoBehaviour
{
    [SerializeField] private GameObject Carta;
    [SerializeField] private SceneControlScript controller;
    public void OnMouseDown(){
        if(Carta.activeSelf){
            Carta.SetActive(false);
            controller.CardRevealed(this);
        }
    }
    private int _id;
    public int id{
        get{
            return _id;
        }
    }
    public void ChangeSprite(int id, Sprite image){
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }
    public void Unreveal(){
        Carta.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
