using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text score;
    public Text strokes;
    public Text par;
    public Text target;

    // Start is called before the first frame update
    void Start()
    {
        Values.TotalTargets = GameObject.FindGameObjectsWithTag("Enemy")
            .Where(e => e.activeSelf)
            .ToArray()
            .Length;
        Values.HoleIndex = Values.HoleNames.IndexOf(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        score.text = GetScorePhrase();
        strokes.text = Values.Strokes.ToString();
        par.text = Values.HolePars[Values.HoleIndex].ToString();
        target.text = (Values.TotalTargets - Values.DestroyedTargets).ToString();
    }

    string GetScorePhrase()
    {
        string phrase = "";

        if (Values.Strokes == 0)
        {
            phrase = "-";
        }
        else if (Values.Strokes == (Values.HolePars[Values.HoleIndex] - 5))
        {
            phrase = "Quadruple Eagle";
        }
        else if (Values.Strokes == (Values.HolePars[Values.HoleIndex] - 4))
        {
            phrase = "Triple Eagle";
        }
        else if (Values.Strokes == (Values.HolePars[Values.HoleIndex] - 3))
        {
            phrase = "Double Eagle";
        }
        else if (Values.Strokes == (Values.HolePars[Values.HoleIndex] - 2))
        {
            phrase = "Eagle";
        }
        else if (Values.Strokes == (Values.HolePars[Values.HoleIndex] - 1))
        {
            phrase = "Birdie";
        }
        else if (Values.Strokes == Values.HolePars[Values.HoleIndex])
        {
            phrase = "Par";
        }
        else if (Values.Strokes == (Values.HolePars[Values.HoleIndex] + 1))
        {
            phrase = "Bogie";
        }
        else if (Values.Strokes == (Values.HolePars[Values.HoleIndex] + 2))
        {
            phrase = "Double Bogie";
        }
        else if (Values.Strokes == (Values.HolePars[Values.HoleIndex] + 3))
        {
            phrase = "Triple Bogie";
        }
        else if (Values.Strokes == (Values.HolePars[Values.HoleIndex] + 4))
        {
            phrase = "Quadruple Bogie";
        }
        else if (Values.Strokes >= (Values.HolePars[Values.HoleIndex] + 5))
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
