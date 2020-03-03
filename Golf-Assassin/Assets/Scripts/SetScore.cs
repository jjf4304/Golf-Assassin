using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetScore : MonoBehaviour
{
    public Text scoreBox;

    // Start is called before the first frame update
    void Start()
    {
        scoreBox.text = "Par: " + Values.Score;
    }
}
