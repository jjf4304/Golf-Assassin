using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNav : MonoBehaviour
{
    public void LoadGame()
    {
        // Reset values
        Values.HoleIndex = 0;
        Values.DestroyedTargets = 0;
        Values.Strokes = 0;
        Time.timeScale = 1f;
        Values.Paused = false;

        SceneManager.LoadScene("SampleScene");
    }

    public void LoadHelpMenu()
    {
        SceneManager.LoadScene("HelpMenu");
    }

    public void ReturnToMainMenu()
    {
        // Reset values
        Time.timeScale = 1f;
        Values.Paused = false;

        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
