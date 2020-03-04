using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text score;
    public Text strokes;

    // Start is called before the first frame update
    void Start()
    {
        Values.TotalTargets = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    // Update is called once per frame
    void Update()
    {
        score.text = GetScorePhrase();
        strokes.text = Values.Strokes.ToString();
    }

    string GetScorePhrase()
    {
        string phrase = "-";

        if (Values.Strokes == (Values.Par - 2))
        {
            phrase = "Eagle";
        }
        else if (Values.Strokes == (Values.Par - 1))
        {
            phrase = "Birdie";
        }
        else if (Values.Strokes == Values.Par)
        {
            phrase = "Par";
        }
        else if (Values.Strokes == (Values.Par + 1))
        {
            phrase = "Bogie";
        }
        else if (Values.Strokes == (Values.Par + 2))
        {
            phrase = "Double Bogie";
        }
        else if (Values.Strokes >= (Values.Par + 3))
        {
            phrase = "You suck";
        }
        else if (Values.Strokes < (Values.Par - 2))
        {
            phrase = "Impossible";
        }
        Values.Score = phrase;
        return phrase;
    }
}
