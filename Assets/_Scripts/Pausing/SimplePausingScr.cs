/*  Author: Joseph Malibiran
 *  Date Created: January 30, 2021
 *  Last Updated: January 30, 2021
 *  Description: A simple way of pausing the game opposed to more involved methods. 'Time.timeScale = 0' can be used as a way to pause the game. UI buttons and Update() should still work.
 *  "Except for realtimeSinceStartup and fixedDeltaTime, timeScale affects all the time and delta time measuring variables of the Time class... FixedUpdate functions will not be called when timeScale is set to zero."
 *  This means all movement should be multiplied by Time.deltaTime for the pause effect to work.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePausingScr : MonoBehaviour
{
    private bool isPaused = false;

    //Toggles game pause
    public void ToggleGamePause() 
    {
        if (isPaused) 
        {
            Time.timeScale = 1;
            isPaused = false;
            return;
        }

        Time.timeScale = 0;
        isPaused = true;
    }

    //Sets the whether the game is paused or not.
    public void SetGamePause(bool _set) 
    {
        if (_set) 
        {
            Time.timeScale = 0;
            isPaused = true;
            return;
        }

        Time.timeScale = 1;
        isPaused = false;
    }

    //Returns whether or not the game is paused.
    public bool IsGamePaused() 
    {
        return isPaused;
    }

}
