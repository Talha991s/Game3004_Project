/*  Author: Joseph Malibiran
 *  Date Created: January 28, 2021
 *  Last Updated: January 28, 2021
 *  Description: This static class contains functions that allow SaveData objects to be written as save files and reads save files.
 */

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveFileReaderWriter
{
    //Writes the given SaveData object as a save file at given path.
    public static void WriteToSaveFile(string filepath, SaveData newSaveFile) 
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filepath, FileMode.Create); //Note: the filepath includes the filename to be created/overwritten
        formatter.Serialize(stream, newSaveFile);
        stream.Close();
    }

    //Reads save file from a given file path and returns its properties as a SaveData file.
    public static SaveData ReadFromSaveFile(string filepath) 
    {
        if(File.Exists(filepath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filepath, FileMode.Open);
            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            return data;
        }
        else 
        {
            Debug.LogError("[Error] Save file not found in " + filepath);
            return null;
        }
    }

    //Untested
    //Returns an array of available save files that can be loaded
    public static string[] CheckAvailableSaveFiles(string saveFileDirectory, string saveFileName) 
    {
        string[] saveFileNames = new string[8]; //This game will have a maximum 8 save slots hardcoded.
        BinaryFormatter formatter = new BinaryFormatter();

        for (int index = 0; index < 8; index++) 
        {
            if (File.Exists(saveFileDirectory + "/" + saveFileName + index.ToString())) 
            {
                FileStream stream = new FileStream(saveFileDirectory + "/" + saveFileName + (index + 1).ToString(), FileMode.Open);
                SaveData data = formatter.Deserialize(stream) as SaveData;
                saveFileNames[index] = data.savefileHeader;
                stream.Close();
            }
            else 
            {
                saveFileNames[index] = "Empty Save Slot";
            }
        }

        return saveFileNames;
    }
}