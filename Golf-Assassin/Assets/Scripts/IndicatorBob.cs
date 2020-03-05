using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorBob : MonoBehaviour
{
    private float max;
    private float min;
    private float t;

    // Start is called before the first frame update
    void Start()
    {
        max = gameObject.transform.position.y;
        min = max - 5;
        t = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.Rotate(Vector3.up);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, Mathf.Lerp(max, min, t), gameObject.transform.position.z);
        t += 0.01f;
        if (t > 1.0f)
        {
            float temp = max;
            max = min;
            min = temp;
            t = 0.0f;
        }
    }
}
