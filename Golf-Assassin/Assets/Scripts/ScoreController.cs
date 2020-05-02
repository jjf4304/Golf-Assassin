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
        Values.HoleScore = Values.Strokes - Values.HolePars[Values.HoleIndex];
        score.text = Values.HoleScore.ToString();
        strokes.text = Values.Strokes.ToString();
        par.text = Values.HolePars[Values.HoleIndex].ToString();
        target.text = (Values.TotalTargets - Values.DestroyedTargets).ToString();
    }
}
