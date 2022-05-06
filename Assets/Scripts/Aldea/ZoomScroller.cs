using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomScroller : MonoBehaviour
{
    [SerializeField]
    private float ScrollSpeed = 10;
    
    private Camera ZoomCamera;  

    private void Start()
    {
        ZoomCamera = Camera.main;
    }

    
    void Update()
    {
        if(ZoomCamera.orthographicSize <= 16f && ZoomCamera.orthographicSize >= 7f){
            ZoomCamera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
            if(ZoomCamera.orthographicSize > 16f){
                ZoomCamera.orthographicSize = 16f;
            } else if(ZoomCamera.orthographicSize < 7f){
                ZoomCamera.orthographicSize = 7f;
            }
        }
        
        
    }
}
