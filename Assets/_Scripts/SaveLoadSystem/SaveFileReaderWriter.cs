/*  Author: Joseph Malibiran
 *  Date Created: January 28, 2021
 *  Last Updated: January 28, 2021
 *  Description: This static class contains functions that allow SaveData objects to be written as save files and reads save files.
 */

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
//using UnityEngine;

public static class SaveFileReaderWriter
{
    //Writes the given SaveData object as a save file at given path.
    public static void WriteToSaveFile(string _filepath, SaveData _newSaveFile) 
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(_filepath, FileMode.Create); //Note: the filepath includes the filename to be created/overwritten
        formatter.Serialize(stream, _newSaveFile);
        stream.Close();
    }

    //Reads save file from a given file path and returns its properties as a SaveData file.
    public static SaveData ReadFromSaveFile(string _filepath) 
    {
        if(File.Exists(_filepath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(_filepath, FileMode.Open);
            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            return data;
        }
        else 
        {
            //Debug.LogError("[Error] Save file not found in " + _filepath);
            return null;
        }
    }

    //Untested
    //Returns an array of available save files that can be loaded
    public static string[] CheckAvailableSaveFiles(string _saveFileDirectory, string _saveFileName) 
    {
        string[] saveFileNames = new string[9]; //This game will have a maximum 9 save slots hardcoded.
        BinaryFormatter formatter = new BinaryFormatter();

        for (int index = 0; index <= 8; index++) 
        {
            if (File.Exists(_saveFileDirectory + "/" + _saveFileName + index.ToString())) 
            {
                FileStream stream = new FileStream(_saveFileDirectory + "/" + _saveFileName + (index).ToString(), FileMode.Open);
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