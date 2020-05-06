using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    public Vector3 vectorToRotateAround;
    public float speed;


    // Update is called once per frame
    void Update()
    {
        if(!Values.Paused)
        transform.RotateAround(transform.position, vectorToRotateAround, speed);
    }
}
