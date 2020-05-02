using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public GameObject indicator;
    public AudioSource hitSound;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Values.DestroyedTargets++;
            hitSound.Play();

            if (Values.DestroyedTargets == Values.TotalTargets)
            {
                Values.CourseScore += Values.HoleScore;
                SceneManager.LoadScene("EndGame");
            }

            Destroy(gameObject);
            Destroy(indicator);
        }
    }
}
