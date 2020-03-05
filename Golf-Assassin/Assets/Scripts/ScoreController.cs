using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text score;
    public Text strokes;
    public Text par;

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
        par.text = Values.Par[Values.HoleIndex].ToString();
    }

    string GetScorePhrase()
    {
        string phrase = "";

        if (Values.Strokes == 0)
        {
            phrase = "-";
        }
        else if (Values.Strokes == (Values.Par[Values.HoleIndex] - 5))
        {
            phrase = "Quadruple Eagle";
        }
        else if (Values.Strokes == (Values.Par[Values.HoleIndex] - 4))
        {
            phrase = "Triple Eagle";
        }
        else if (Values.Strokes == (Values.Par[Values.HoleIndex] - 3))
        {
            phrase = "Double Eagle";
        }
        else if (Values.Strokes == (Values.Par[Values.HoleIndex] - 2))
        {
            phrase = "Eagle";
        }
        else if (Values.Strokes == (Values.Par[Values.HoleIndex] - 1))
        {
            phrase = "Birdie";
        }
        else if (Values.Strokes == Values.Par[Values.HoleIndex])
        {
            phrase = "Par";
        }
        else if (Values.Strokes == (Values.Par[Values.HoleIndex] + 1))
        {
            phrase = "Bogie";
        }
        else if (Values.Strokes == (Values.Par[Values.HoleIndex] + 2))
        {
            phrase = "Double Bogie";
        }
        else if (Values.Strokes == (Values.Par[Values.HoleIndex] + 3))
        {
            phrase = "Triple Bogie";
        }
        else if (Values.Strokes == (Values.Par[Values.HoleIndex] + 4))
        {
            phrase = "Quadruple Bogie";
        }
        else if (Values.Strokes >= (Values.Par[Values.HoleIndex] + 5))
        {
            phrase = "You suck";
        }
        else if (Values.Strokes < Values.TotalTargets)
        {
            phrase = "Impossible";
        }
        Values.Score = phrase;
        return phrase;
    }
}
