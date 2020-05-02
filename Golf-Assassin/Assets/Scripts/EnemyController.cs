using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public GameObject indicator;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Values.DestroyedTargets++;

            if (Values.DestroyedTargets == Values.TotalTargets)
            {
                SceneManager.LoadScene("EndGame");
            }

            Destroy(gameObject);
            Destroy(indicator);
        }
    }
}
