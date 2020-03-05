using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject toBeActivated;
    public GameObject toBeDeactivated;

    public void togglePause()
    {
        toBeActivated.SetActive(true);
        toBeDeactivated.SetActive(false);

        if (Time.timeScale == 0f)
        {
            Values.Paused = false;
            Time.timeScale = 1f;
        }
        else
        {
            Values.Paused = true;
            Time.timeScale = 0f;
        }
    }
}
