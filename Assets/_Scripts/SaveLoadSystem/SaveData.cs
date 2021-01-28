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
        savefileHeader = "default save name";
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

    public SaveData(string insertFileName, float insertPositionX, float insertPositionY, float insertPositionZ, float insertRotationX, float insertRotationY, float insertRotationZ,
                    int insertLivesAmount, int insertAmmoAmount, int insertSeedsCollected, int insertAliensKilled, int insertCurrentLevel, int insertLevelsUnlocked)
    {
        savefileHeader = insertFileName;
        playerLocationX = insertPositionX;
        playerLocationY = insertPositionY;
        playerLocationZ = insertPositionZ;
        playerOrientationX = insertRotationX;
        playerOrientationY = insertRotationY;
        playerOrientationZ = insertRotationZ;

        livesAmount = insertLivesAmount;
        ammoAmount = insertAmmoAmount;
        seedsCollected = insertSeedsCollected;
        aliensKilled = insertAliensKilled;
        currentLevel = insertCurrentLevel; //0 means not in a level
        levelsUnlocked = insertLevelsUnlocked;
    }
}
