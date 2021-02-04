/*  Author: Joseph Malibiran
 *  Date Created: January 31, 2021
 *  Last Updated: January 31, 2021
 *  Description: This script is a temporary placeholder for user input triggering events. As per project instructions, we should actually have key mapping capability from the options menu.
 *  This was created as a placeholder mechanic to temporarily meet the criteria of "Project Part 4 – First Playable Release" to have key activation for: Inventory System, Pausing, Quick Saving, Quick Loading.
 *  
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SaveFileManager))]
[RequireComponent(typeof(SimplePausingScr))]
public class TempInputEventScr : MonoBehaviour
{
    [SerializeField] private Transform playerCharRef;

    private SaveFileManager saveManager;
    private SimplePausingScr pauseScr;

    private void Awake() 
    {
        saveManager = this.gameObject.GetComponent<SaveFileManager>();
        pauseScr = this.gameObject.GetComponent<SimplePausingScr>();
    }

    private void Update() 
    {
        CheckInput();
    }

    //Listens for user key presses and triggers appropriate events
    private void CheckInput() 
    {
        if (Input.GetKeyUp(KeyCode.I)) 
        {
            //Toggle Inventory here
        }

        if (Input.GetKeyUp(KeyCode.P)) 
        {
            //Toggle Pausing here
            pauseScr.ToggleGamePause();
        }

        if (Input.GetKeyUp(KeyCode.L)) 
        {
            SaveData loadedData;

            //Activate Quick Load here
            if(saveManager)
            {
                loadedData = saveManager.LoadGame(1);
            }

            //TODO load level using save file info here
        }

        if (Input.GetKeyUp(KeyCode.O)) 
        {
            if(playerCharRef && saveManager)
            {
                //Activate Quick Save here
                SaveData newSave = new SaveData();
                newSave.playerLocationX = playerCharRef.position.x;
                newSave.playerLocationY = playerCharRef.position.y;
                newSave.playerLocationZ = playerCharRef.position.z;
                newSave.playerOrientationX = playerCharRef.localEulerAngles.x;
                newSave.playerOrientationY = playerCharRef.localEulerAngles.y;
                newSave.playerOrientationZ = playerCharRef.localEulerAngles.z;
                saveManager.SaveGame(1, newSave);
            } 
        }
    }
}
