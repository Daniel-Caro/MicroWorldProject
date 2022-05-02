using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slatosVerticalesCamera : MonoBehaviour
{
    public Transform target;

    private void LateUpdate()
    {
        if (target.position.y > transform.position.y)
        {
            Vector3 newPos =  new Vector3(transform.position.x, target.position.y, transform.position.z);
            transform.position = newPos;
        }
    }
}
