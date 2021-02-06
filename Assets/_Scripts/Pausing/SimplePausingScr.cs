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

//Note: This script does not tamper with UI by design
public class SimplePausingScr : MonoBehaviour
{

    //Toggles game pause
    public void ToggleGamePause() 
    {
        if (Time.timeScale == 0) 
        {
            Time.timeScale = 1;
            return;
        }

        Time.timeScale = 0;
    }

    //Sets the whether the game is paused or not.
    public void SetGamePause(bool _set) 
    {
        if (_set) 
        {
            Time.timeScale = 0;
            return;
        }
        Time.timeScale = 1;
    }

    //Sets the timescale manually
    public void SetTimeScale(float _timeScale) 
    {
        Time.timeScale = _timeScale;
    }

    //Returns whether or not the game is paused.
    public bool IsGamePaused() 
    {
        if (Time.timeScale == 0) 
        {
            return true;
        }
        return false;
    }

}