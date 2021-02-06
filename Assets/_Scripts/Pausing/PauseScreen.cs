using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    bool paused = false;

    public void ToggleGamePause() 
    {
        if (paused) 
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
            paused = false;
        }
        else
        {
            Time.timeScale = 0;
            gameObject.SetActive(true);
            paused = true;
        }
        
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
