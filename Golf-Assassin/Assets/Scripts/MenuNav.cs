using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;
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

        if (Values.Paused)
        {
            Time.timeScale = 1f;
            Values.Paused = false;
        }

        SceneManager.LoadScene("SampleScene");
    }

    public void LoadNextLevel()
    {
        // Reset values
        Values.HoleIndex++;
        Values.DestroyedTargets = 0;
        Values.Strokes = 0;
        SceneManager.LoadScene(Values.HoleNames[Values.HoleIndex]);
    }

    public void RestartLevel()
    {
        Values.DestroyedTargets = 0;
        Values.Strokes = 0;

        if (Values.Paused)
        {
            Time.timeScale = 1f;
            Values.Paused = false;
        }

        SceneManager.LoadScene(Values.HoleNames[Values.HoleIndex]);
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
