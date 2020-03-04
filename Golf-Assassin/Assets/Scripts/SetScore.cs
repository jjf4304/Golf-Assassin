using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetScore : MonoBehaviour
{
    public Text strokeBox;
    public Text parBox;
    public Text scoreBox;

    // Start is called before the first frame update
    void Start()
    {
        parBox.text = "Par: " + Values.Par;
        strokeBox.text = "Total Strokes: " + Values.Strokes;
        scoreBox.text = "Score: " + Values.Score;
    }
}
