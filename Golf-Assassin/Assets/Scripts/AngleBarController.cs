using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngleBarController : MonoBehaviour
{
    private Slider angleBar;
    private float currentAngle = 1;

    void Awake()
    {
        angleBar = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        currentAngle = angleBar.value;
    }

    public float GetAngle()
    {
        return currentAngle;
    }
}
