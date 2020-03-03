using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    private ScoreController scoreThing;

    void Awake()
    {
        scoreThing = GameObject.Find("Score").GetComponent<ScoreController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Values.DestroyedTargets++;
            Debug.Log(Values.TotalTargets);
            scoreThing.PlusScore(10);
            if (Values.DestroyedTargets == Values.TotalTargets)
            {
                scoreThing.PlusScore(-1 * scoreThing.GetStrokes());
                SceneManager.LoadScene("EndGame");
            }
            Destroy(gameObject);
        }
    }
}
