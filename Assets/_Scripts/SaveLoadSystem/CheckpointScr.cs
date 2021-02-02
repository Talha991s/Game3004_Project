/*  Author: Joseph Malibiran
 *  Date Created: February 2, 2021
 *  Last Updated: February 2, 2021
 *  Description: This script is used for a prefab that functions as a quicksave activation location- a game checkpoint. 
 *  If an object with the tag "Player" enters the radius, a quick save will trigger and save to save file slot 0.
 *  There is a cooldown before the player character can activate the checkpoint prefab again.
 *  If the reference to SaveFileManager object is not present, it will save directly via static SaveFileReaderWriter object.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScr : MonoBehaviour
{
    [SerializeField] private SaveFileManager saveManagerRef;
    private float quickSaveCooldown = 2.0f;
    private bool canActivateCheckpoint = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && canActivateCheckpoint) 
        {
            Debug.Log("[Notice] Quicksave activated.");
            //Note: We elect to use coroutine to set up a delay before the player can activate quicksave again. 
            //This is a safety measure to prevent the system from accidentally triggering more than once in quick succession.
            StartCoroutine(QuickSaveRoutine(quickSaveCooldown));    
        }
    }

    private IEnumerator QuickSaveRoutine(float delay)
    {
        canActivateCheckpoint = false;

        if (saveManagerRef) 
        {
            saveManagerRef.SaveGame(0);
        }
        else 
        {
            SaveFileReaderWriter.WriteToSaveFile(Application.persistentDataPath + "/Hamstronaut0.hamsave", new SaveData());
        }

        yield return new WaitForSeconds(delay);
        canActivateCheckpoint = true;
    }
}
