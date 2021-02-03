/*  Author: Joseph Malibiran
 *  Date Created: January 28, 2021
 *  Last Updated: January 28, 2021
 *  Description: This class holds all the loaded data of a save file. Game data must be converted to this format before writing to save files. 
 *  And Save files must be converted to this data before being used by the game. Note: This class cannot use Unity's Vector3.
 */

using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SaveData 
{
    public string savefileHeader; //The save file header seen in-game view. This is different from the save file name.
    public string gameVersion;
    public float playerLocationX;
    public float playerLocationY;
    public float playerLocationZ;
    public float playerOrientationX;
    public float playerOrientationY;
    public float playerOrientationZ;

    public int livesAmount;
    public int ammoAmount;
    public int seedsCollected;
    public int aliensKilled;
    public int currentLevel; //0 means not in a level
    public int levelsUnlocked;

    public SaveData() 
    {
        //These are default values that SHOULD be replaced upon instantiation
        savefileHeader = "default header";
        gameVersion = "undefined";
        playerLocationX = 0;
        playerLocationY = 0;
        playerLocationZ = 0;
        playerOrientationX = 0;
        playerOrientationY = 0;
        playerOrientationZ = 0;

        livesAmount = 3;
        ammoAmount = 100;
        seedsCollected = 0;
        aliensKilled = 0;
        currentLevel = 0; //0 means not in a level
        levelsUnlocked = 1;
    }

    public SaveData(string _insertFileName, string _insertGameVersion, float _insertPositionX, float _insertPositionY, float _insertPositionZ, float _insertRotationX, float _insertRotationY, float _insertRotationZ,
                    int _insertLivesAmount, int _insertAmmoAmount, int _insertSeedsCollected, int _insertAliensKilled, int _insertCurrentLevel, int _insertLevelsUnlocked)
    {
        savefileHeader = _insertFileName;
        gameVersion = _insertGameVersion;
        playerLocationX = _insertPositionX;
        playerLocationY = _insertPositionY;
        playerLocationZ = _insertPositionZ;
        playerOrientationX = _insertRotationX;
        playerOrientationY = _insertRotationY;
        playerOrientationZ = _insertRotationZ;

        livesAmount = _insertLivesAmount;
        ammoAmount = _insertAmmoAmount;
        seedsCollected = _insertSeedsCollected;
        aliensKilled = _insertAliensKilled;
        currentLevel = _insertCurrentLevel; //0 means not in a level
        levelsUnlocked = _insertLevelsUnlocked;
    }
}
