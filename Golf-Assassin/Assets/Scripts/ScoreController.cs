using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    private int score = 10;
    // Start is called before the first frame update
    void Start()
    {
        Values.Score = score;
        Values.TotalTargets = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = "Score: " + score.ToString();
        Values.Score = score;
    }

    public void MinusScore(int minusScore)
    {
        score = score - minusScore;
    }
}
