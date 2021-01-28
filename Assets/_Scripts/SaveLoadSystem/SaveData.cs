/*  Author: Joseph Malibiran
 *  Date Created: January 28, 2021
 *  Last Updated: January 28, 2021
 *  Description:
 *  
 */

using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SaveData 
{
    public string savefileName;
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
        savefileName = "default save name";
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
}
