//Created by Shimron, edited by Joseph

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    //Note: The part commented out below is not needed; timeScale is handled by SimplePausingScr.cs and Screen gameObject activation is handled by UI buttons.

    //bool paused = false;

    //public void ToggleGamePause() 
    //{
    //    if (paused) 
    //    {
    //        Time.timeScale = 1;
    //        gameObject.SetActive(false);
    //        paused = false;
    //    }
    //    else
    //    {
    //        Time.timeScale = 0;
    //        gameObject.SetActive(true);
    //        paused = true;
    //    }
        
    //}

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //Temporary; Used by TemporaryTransitionButton in HUD prefab
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
