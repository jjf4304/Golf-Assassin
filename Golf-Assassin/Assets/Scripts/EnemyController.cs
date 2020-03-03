using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
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
            if (Values.DestroyedTargets == Values.TotalTargets)
            {
                SceneManager.LoadScene("EndGame");
            }
            Destroy(gameObject);
        }
    }
}
