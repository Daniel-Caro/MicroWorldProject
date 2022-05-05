using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D cursor;
    public Texture2D cursorClicked;

    void Start()
    {
        
    }

    private void Awake() {
        ChangeCursor(cursor);
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void ChangeCursor(Texture2D cursorType){

        Cursor.SetCursor(cursorType, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangeCursor(cursorClicked);
        }
        if (Input.GetMouseButtonUp(0)){
            ChangeCursor(cursor);
        }
    }

}
