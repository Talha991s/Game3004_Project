/*  Author: Joseph Malibiran
 *  Date Created: January 30, 2021
 *  Last Updated: January 30, 2021
 *  Description: A simple way of pausing the game opposed to more involved methods. 'Time.timeScale = 0' can be used as a way to pause the game; This is basically a wrapper for Time.timeScale. UI buttons and Update() should still work.
 *  "Except for realtimeSinceStartup and fixedDeltaTime, timeScale affects all the time and delta time measuring variables of the Time class... FixedUpdate functions will not be called when timeScale is set to zero."
 *  This means all movement should be multiplied by Time.deltaTime for the pause effect to work. If you don't want to use Time.deltaTime in a movement (eg. rotation) use SimplePausingScr.IsGamePaused() to check whether 
 *  or not the game is paused. Then, disable controls or movement if the function returns true.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


}
