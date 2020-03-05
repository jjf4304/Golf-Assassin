using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateUI : MonoBehaviour
{
    private Transform ball;

    void Start()
    {
        ball = gameObject.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ball.Rotate(Vector3.up);
    }
}
