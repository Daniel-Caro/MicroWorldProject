using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    float[] rotations = { 0, 90, 180, 270 };
    public bool hasWater;
    public bool hasCoin = false;
    public string type;
    public Sprite drySprite;
    public Sprite waterSprite;
    public GameObject timeUpText;

    

    void Start() 
    {
        if (this.name != "startPipePirate" && this.name != "startPipePrincess" && this.name != "startPipeFuture")
        {
            int rand = UnityEngine.Random.Range(0, rotations.Length);
            transform.eulerAngles = new Vector3(0,0,rotations[rand]);
        }
    }

    private void OnMouseDown() 
    {
        if (timeUpText.activeSelf) return;
        Debug.Log("click");
        transform.Rotate(new Vector3(0,0,90));
        if ((this.name == "startPipePirate" || this.name == "startPipePrincess" || this.name == "startPipeFuture") && this.transform.eulerAngles.z != 0)
        {
            this.hasWater = true;
            this.GetComponent<SpriteRenderer>().sprite = this.waterSprite;
        } 
        else{
            this.hasWater = false;
            this.GetComponent<SpriteRenderer>().sprite = this.drySprite;
        } 
        GameObject allPipes = GameObject.Find("Pipes");
        foreach (Transform child in allPipes.transform) {
            child.gameObject.GetComponent<PipeScript>().hasWater = false;
            child.gameObject.GetComponent<SpriteRenderer>().sprite = child.gameObject.GetComponent<PipeScript>().drySprite;
        }
        GameObject startPipe = null;
        switch(Globals.style)
        {
             case(Style.Future):
                startPipe = GameObject.Find("startPipeFuture");
                break;
            case(Style.Pirate):
                startPipe = GameObject.Find("startPipePirate");
                break;
            case(Style.Princess):
                startPipe = GameObject.Find("startPipePrincess");
                break;
        }
        if(startPipe.GetComponent<PipeScript>().hasWater) PipeScript.checkWater(startPipe); 
    }

    public static void checkWater(GameObject pipe)
    {
        PipeScript pipeScript = pipe.GetComponent<PipeScript>();
        GameObject upPipe = null;
        GameObject bottomPipe = null;
        GameObject leftPipe = null;
        GameObject rightPipe = null;
        float x = pipe.transform.position.x;
        float y = pipe.transform.position.y;
        if (y != 3f) upPipe = Physics2D.OverlapCircle(new Vector2(x, y + 1.5f), 0.2f).gameObject;
        if (y != -3f) bottomPipe = Physics2D.OverlapCircle(new Vector2(x, y - 1.5f), 0.2f).gameObject;
        if (x != -6f) leftPipe = Physics2D.OverlapCircle(new Vector2(x - 1.5f, y), 0.2f).gameObject;
        if (x != 6f) rightPipe = Physics2D.OverlapCircle(new Vector2(x + 1.5f, y), 0.2f).gameObject;        
        float rotation = (float)Math.Floor(pipe.transform.eulerAngles.z);

        Debug.Log("Comprobando la tuberia " + pipe.name + " con tipo " + pipeScript.type + " y rotación " + rotation.ToString());
        if (upPipe != null && !pipeScript.hasWater)
        {
            float adjacentRotation = (float)Math.Floor(upPipe.transform.eulerAngles.z);
            string adjacentType = upPipe.GetComponent<PipeScript>().type;
            if (upPipe.GetComponent<PipeScript>().hasWater){
                switch(pipeScript.type) 
                {
                    case("straight"):
                        if (rotation == 0f || rotation == 180f)
                        {
                            switch(adjacentType)
                            {
                                case("straight"):
                                    if (adjacentRotation == 0f || adjacentRotation == 180f) pipeScript.hasWater = true; 
                                    break;
                                case("curve"):
                                    if (adjacentRotation == 0f || adjacentRotation == 270f) pipeScript.hasWater = true; 
                                    break;
                                case("3dir"):
                                    if (adjacentRotation != 90) pipeScript.hasWater = true; 
                                    break;
                                case("cross"):
                                    pipeScript.hasWater = true; 
                                    break;
                            }
                        }
                        break;
                    case("curve"):
                        if (rotation == 90f || rotation == 180f)
                        {
                            switch(adjacentType)
                            {
                                case("straight"):
                                    if (adjacentRotation == 0f || adjacentRotation == 180f) pipeScript.hasWater = true; 
                                    break;
                                case("curve"):
                                    if (adjacentRotation == 0f || adjacentRotation == 270f) pipeScript.hasWater = true; 
                                    break;
                                case("3dir"):
                                    if (adjacentRotation != 90) pipeScript.hasWater = true; 
                                    break;
                                case("cross"):
                                    pipeScript.hasWater = true; 
                                    break;
                            }
                        }
                        break;
                    case("3dir"):
                        if (rotation == 0f || rotation == 90f || rotation == 180f)
                        {
                            switch(adjacentType)
                            {
                                case("straight"):
                                    if (adjacentRotation == 0f || adjacentRotation == 180f) pipeScript.hasWater = true; 
                                    break;
                                case("curve"):
                                    if (adjacentRotation == 0f || adjacentRotation == 270f) pipeScript.hasWater = true; 
                                    break;
                                case("3dir"):
                                    if (adjacentRotation != 90) pipeScript.hasWater = true; 
                                    break;
                                case("cross"):
                                    pipeScript.hasWater = true; 
                                    break;
                            }
                        }
                        break;
                    case("cross"):
                        switch(adjacentType)
                        {
                            case("straight"):
                                if (adjacentRotation == 0f || adjacentRotation == 180f) pipeScript.hasWater = true; 
                                break;
                            case("curve"):
                                if (adjacentRotation == 0f || adjacentRotation == 270f) pipeScript.hasWater = true; 
                                break;
                            case("3dir"):
                                    if (adjacentRotation != 90) pipeScript.hasWater = true; 
                                break;
                            case("cross"):
                                pipeScript.hasWater = true; 
                                break;
                        }
                        break;
                }
            }
        }
        if (bottomPipe != null && !pipeScript.hasWater)
        {
            float adjacentRotation = (float)Math.Floor(bottomPipe.transform.eulerAngles.z);
            string adjacentType = bottomPipe.GetComponent<PipeScript>().type;
            if (bottomPipe.GetComponent<PipeScript>().hasWater){
                switch(pipeScript.type) 
                {
                    case("straight"):
                        if (rotation == 0f || rotation == 180f)
                        {
                            switch(adjacentType)
                            {
                                case("straight"):
                                    if (adjacentRotation == 0f || adjacentRotation == 180f) pipeScript.hasWater = true; 
                                    break;
                                case("curve"):
                                    if (adjacentRotation == 90f || adjacentRotation == 180f) pipeScript.hasWater = true; 
                                    break;
                                case("3dir"):
                                    if (adjacentRotation != 270f) pipeScript.hasWater = true; 
                                    break;
                                case("cross"):
                                    pipeScript.hasWater = true; 
                                    break;
                            }
                        }
                        break;
                    case("curve"):
                        if (rotation == 0f || rotation == 270f)
                        {
                            switch(adjacentType)
                            {
                                case("straight"):
                                    if (adjacentRotation == 0f || adjacentRotation == 180f) pipeScript.hasWater = true; 
                                    break;
                                case("curve"):
                                    if (adjacentRotation == 90f || adjacentRotation == 180f) pipeScript.hasWater = true; 
                                    break;
                                case("3dir"):
                                    if (adjacentRotation != 270f) pipeScript.hasWater = true; 
                                    break;
                                case("cross"):
                                    pipeScript.hasWater = true; 
                                    break;
                            }
                        }
                        break;
                    case("3dir"):
                        if (rotation == 0f || rotation == 180f || rotation == 270f)
                        {
                            switch(adjacentType)
                            {
                                case("straight"):
                                    if (adjacentRotation == 0f || adjacentRotation == 180f) pipeScript.hasWater = true; 
                                    break;
                                case("curve"):
                                    if (adjacentRotation == 90f || adjacentRotation == 180f) pipeScript.hasWater = true; 
                                    break;
                                case("3dir"):
                                    if (adjacentRotation != 270f) pipeScript.hasWater = true; 
                                    break;
                                case("cross"):
                                    pipeScript.hasWater = true; 
                                    break;
                            }
                        }
                        break;
                    case("cross"):
                        switch(adjacentType)
                        {
                            case("straight"):
                                if (adjacentRotation == 0f || adjacentRotation == 180f) pipeScript.hasWater = true; 
                                break;
                            case("curve"):
                                if (adjacentRotation == 90f || adjacentRotation == 180f) pipeScript.hasWater = true; 
                                break;
                            case("3dir"):
                                    if (adjacentRotation != 270f) pipeScript.hasWater = true; 
                                break;
                            case("cross"):
                                pipeScript.hasWater = true; 
                                break;
                        }
                        break;
                }
            }
        }
        if (leftPipe != null && !pipeScript.hasWater)
        {
            float adjacentRotation = (float)Math.Floor(leftPipe.transform.eulerAngles.z);
            string adjacentType = leftPipe.GetComponent<PipeScript>().type;
            if (leftPipe.GetComponent<PipeScript>().hasWater){
                switch(pipeScript.type) 
                {
                    case("straight"):
                        if (rotation == 90f || rotation == 270f)
                        {
                            switch(adjacentType)
                            {
                                case("straight"):
                                    if (adjacentRotation == 90f || adjacentRotation == 270f) pipeScript.hasWater = true; 
                                    break;
                                case("curve"):
                                    if (adjacentRotation == 0f || adjacentRotation == 90f) pipeScript.hasWater = true; 
                                    break;
                                case("3dir"):
                                    if (adjacentRotation != 180f) pipeScript.hasWater = true; 
                                    break;
                                case("cross"):
                                    pipeScript.hasWater = true; 
                                    break;
                            }
                        }
                        break;
                    case("curve"):
                        if (rotation == 180f || rotation == 270f)
                        {
                            switch(adjacentType)
                            {
                                case("straight"):
                                    if (adjacentRotation == 90f || adjacentRotation == 270f) pipeScript.hasWater = true; 
                                    break;
                                case("curve"):
                                    if (adjacentRotation == 0f || adjacentRotation == 90f) pipeScript.hasWater = true; 
                                    break;
                                case("3dir"):
                                    if (adjacentRotation != 180f) pipeScript.hasWater = true; 
                                    break;
                                case("cross"):
                                    pipeScript.hasWater = true; 
                                    break;
                            }
                        }
                        break;
                    case("3dir"):
                        if (rotation == 90f || rotation == 180f || rotation == 270f)
                        {
                            switch(adjacentType)
                            {
                                case("straight"):
                                    if (adjacentRotation == 90f || adjacentRotation == 270f) pipeScript.hasWater = true; 
                                    break;
                                case("curve"):
                                    if (adjacentRotation == 0f || adjacentRotation == 90f) pipeScript.hasWater = true; 
                                    break;
                                case("3dir"):
                                    if (adjacentRotation != 180f) pipeScript.hasWater = true; 
                                    break;
                                case("cross"):
                                    pipeScript.hasWater = true; 
                                    break;
                            }
                        }
                        break;
                    case("cross"):
                        switch(adjacentType)
                        {
                            case("straight"):
                                if (adjacentRotation == 90f || adjacentRotation == 270f) pipeScript.hasWater = true; 
                                break;
                            case("curve"):
                                if (adjacentRotation == 0f || adjacentRotation == 90f) pipeScript.hasWater = true; 
                                break;
                            case("3dir"):
                                    if (adjacentRotation != 180f) pipeScript.hasWater = true; 
                                break;
                            case("cross"):
                                pipeScript.hasWater = true; 
                                break;
                        }
                        break;
                }
            }
        }
        if (rightPipe != null && !pipeScript.hasWater)
        {
            float adjacentRotation = (float)Math.Floor(rightPipe.transform.eulerAngles.z);
            string adjacentType = rightPipe.GetComponent<PipeScript>().type;
            if (rightPipe.GetComponent<PipeScript>().hasWater){
                switch(pipeScript.type) 
                {
                    case("straight"):
                        if (rotation == 90f || rotation == 270f)
                        {
                            switch(adjacentType)
                            {
                                case("straight"):
                                    if (adjacentRotation == 90f || adjacentRotation == 270f) pipeScript.hasWater = true; 
                                    break;
                                case("curve"):
                                    if (adjacentRotation == 180f || adjacentRotation == 270f) pipeScript.hasWater = true; 
                                    break;
                                case("3dir"):
                                    if (adjacentRotation != 0f) pipeScript.hasWater = true; 
                                    break;
                                case("cross"):
                                    pipeScript.hasWater = true; 
                                    break;
                            }
                        }
                        break;
                    case("curve"):
                        if (rotation == 0f || rotation == 90f)
                        {
                            switch(adjacentType)
                            {
                                case("straight"):
                                    if (adjacentRotation == 90f || adjacentRotation == 270f) pipeScript.hasWater = true; 
                                    break;
                                case("curve"):
                                    if (adjacentRotation == 180f || adjacentRotation == 270f) pipeScript.hasWater = true; 
                                    break;
                                case("3dir"):
                                    if (adjacentRotation != 0f) pipeScript.hasWater = true; 
                                    break;
                                case("cross"):
                                    pipeScript.hasWater = true; 
                                    break;
                            }
                        }
                        break;
                    case("3dir"):
                        if (rotation == 0f || rotation == 90f || rotation == 270f)
                        {
                            switch(adjacentType)
                            {
                                case("straight"):
                                    if (adjacentRotation == 90f || adjacentRotation == 270f) pipeScript.hasWater = true; 
                                    break;
                                case("curve"):
                                    if (adjacentRotation == 180f || adjacentRotation == 270f) pipeScript.hasWater = true; 
                                    break;
                                case("3dir"):
                                    if (adjacentRotation != 0f) pipeScript.hasWater = true; 
                                    break;
                                case("cross"):
                                    pipeScript.hasWater = true; 
                                    break;
                            }
                        }
                        break;
                    case("cross"):
                        switch(adjacentType)
                        {
                            case("straight"):
                                if (adjacentRotation == 90f || adjacentRotation == 270f) pipeScript.hasWater = true; 
                                break;
                            case("curve"):
                                if (adjacentRotation == 180f || adjacentRotation == 270f) pipeScript.hasWater = true; 
                                break;
                            case("3dir"):
                                    if (adjacentRotation != 0f) pipeScript.hasWater = true; 
                                break;
                            case("cross"):
                                pipeScript.hasWater = true; 
                                break;
                        }
                        break;
                }
            }
        }

        if (pipeScript.hasWater)
        {
            //Se cambia el sprite a mojado
            SpriteRenderer spriteRenderer = pipe.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = pipeScript.waterSprite;
            //Si la casilla es la final y está girada hacia el lado correcto se gana el juego
            Debug.Log(x.ToString() + " - " + y.ToString());
            if (x == 6f && y == -3f)
            {
                bool finished = false;
                Debug.Log("Casilla final");
                switch(pipeScript.type) 
                {
                    case("straight"):
                        if (rotation == 90f || rotation == 270f)
                        {
                            Debug.Log("Juego terminado");
                            finished = true;
                        }
                        break;
                    case("curve"):
                        if (rotation == 0f || rotation == 90f)
                        {
                            Debug.Log("Juego terminado");
                            finished = true;
                        }
                        break;
                    case("3dir"):
                        if (rotation == 0f || rotation == 90f || rotation == 270f)
                        {
                            Debug.Log("Juego terminado");
                            finished = true;
                        }
                        break;
                    case("cross"):
                        Debug.Log("Juego terminado");
                        finished = true;
                        break;
                }
                if (finished)
                {
                    TuberiasGameManager.RestartGame();
                }
            }
            
            //Se comprueba el resto
            if (upPipe != null) if (!upPipe.GetComponent<PipeScript>().hasWater) PipeScript.checkWater(upPipe);
            if (bottomPipe != null) if (!bottomPipe.GetComponent<PipeScript>().hasWater) PipeScript.checkWater(bottomPipe);
            if (leftPipe != null) if (!leftPipe.GetComponent<PipeScript>().hasWater) PipeScript.checkWater(leftPipe);
            if (rightPipe != null) if (!rightPipe.GetComponent<PipeScript>().hasWater) PipeScript.checkWater(rightPipe);
        } 
        else 
        {
            //Sprite seco
            SpriteRenderer spriteRenderer = pipe.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = pipeScript.drySprite;
        }
    }
}
