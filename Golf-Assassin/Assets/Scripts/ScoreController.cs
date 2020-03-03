using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    private int score = 0;
    private int strokes = 0;
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

    public void PlusScore(int plusScore)
    {
        score = score + plusScore;
    }

    public void PlusStroke(int plusStroke)
    {
        strokes = strokes + plusStroke;
    } 
    public int GetStrokes()
    {
        return strokes;
    }
}
