/*  Author: Joseph Malibiran
 *  Date Created: January 28, 2021
 *  Last Updated: January 28, 2021
 *  Description:
 *  
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Windows;

public class SaveManager : MonoBehaviour 
{
    [SerializeField] private string savefileName = "marco";
    [SerializeField] private Vector3 playerLocation = Vector3.zero;
    [SerializeField] private Vector3 playerOrientation = Vector3.zero;

    [SerializeField] private int livesAmount = 3;
    [SerializeField] private int ammoAmount = 100;
    [SerializeField] private int seedsCollected = 0;
    [SerializeField] private int aliensKilled = 0;
    [SerializeField] private int currentLevel = 0; //0 means not in a level
    [SerializeField] private int levelsUnlocked = 1;

    [SerializeField] private bool saveButton = false; //TODO Remove. This is only used during development to test savefile saving.
    [SerializeField] private bool loadButton = false; //TODO Remove. This is only used during development to test savefile loading.

    private SaveData loadedSaveData; //Initial save data being used

    private void Update() 
    {
        if (saveButton) 
        {
            saveButton = false;
            SaveGame();
        }

        if (loadButton) 
        {
            loadButton = false;
            LoadGame(0); //temp test
        }
    }

    public void SaveGame() 
    {
        loadedSaveData = new SaveData();
        loadedSaveData.savefileName = this.savefileName;
        loadedSaveData.playerLocationX = this.playerLocation.x;
        loadedSaveData.playerLocationY = this.playerLocation.y;
        loadedSaveData.playerLocationZ = this.playerLocation.z;
        loadedSaveData.playerOrientationX = this.playerOrientation.x;
        loadedSaveData.playerOrientationY = this.playerOrientation.y;
        loadedSaveData.playerOrientationZ = this.playerOrientation.z;

        loadedSaveData.livesAmount = this.livesAmount;
        loadedSaveData.ammoAmount = this.ammoAmount;
        loadedSaveData.seedsCollected = this.seedsCollected;
        loadedSaveData.aliensKilled = this.aliensKilled;
        loadedSaveData.currentLevel = this.currentLevel; //0 means not in a level
        loadedSaveData.levelsUnlocked = this.levelsUnlocked;

        SaveFileReadWrite.WriteToSaveFile(Application.persistentDataPath + "/" + savefileName + ".hamsave", loadedSaveData);
    }

    public void LoadGame(string path) 
    {
        loadedSaveData = SaveFileReadWrite.ReadFromSaveFile(path);

        this.savefileName = loadedSaveData.savefileName;
        this.playerLocation = new Vector3(loadedSaveData.playerLocationX, loadedSaveData.playerLocationY, loadedSaveData.playerLocationZ);
        this.playerOrientation = new Vector3(loadedSaveData.playerOrientationX, loadedSaveData.playerOrientationY, loadedSaveData.playerOrientationZ);
        this.livesAmount = loadedSaveData.livesAmount;
        this.ammoAmount = loadedSaveData.ammoAmount;
        this.seedsCollected = loadedSaveData.seedsCollected;
        this.aliensKilled = loadedSaveData.aliensKilled;
        this.currentLevel = loadedSaveData.currentLevel; //0 means not in a level
        this.levelsUnlocked = loadedSaveData.levelsUnlocked;
    }

    public void LoadGame(int saveFileIndex) 
    {
        //TODO: saveFileIndex currently unused

        loadedSaveData = SaveFileReadWrite.ReadFromSaveFile(Application.persistentDataPath + "/" + savefileName + ".hamsave"); //TODO temp

        this.savefileName = loadedSaveData.savefileName;
        this.playerLocation = new Vector3(loadedSaveData.playerLocationX, loadedSaveData.playerLocationY, loadedSaveData.playerLocationZ);
        this.playerOrientation = new Vector3(loadedSaveData.playerOrientationX, loadedSaveData.playerOrientationY, loadedSaveData.playerOrientationZ);
        this.livesAmount = loadedSaveData.livesAmount;
        this.ammoAmount = loadedSaveData.ammoAmount;
        this.seedsCollected = loadedSaveData.seedsCollected;
        this.aliensKilled = loadedSaveData.aliensKilled;
        this.currentLevel = loadedSaveData.currentLevel; //0 means not in a level
        this.levelsUnlocked =loadedSaveData.levelsUnlocked;
    }

}
