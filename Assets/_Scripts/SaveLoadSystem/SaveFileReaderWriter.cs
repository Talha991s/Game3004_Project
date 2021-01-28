/*  Author: Joseph Malibiran
 *  Date Created: January 28, 2021
 *  Last Updated: January 28, 2021
 *  Description:
 *  
 */

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveFileReadWrite 
{
    public static void WriteToSaveFile(string path, SaveData newSaveFile) 
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, newSaveFile);
        stream.Close();
    }

    public static SaveData ReadFromSaveFile(string path) 
    {
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            return data;
        }
        else 
        {
            Debug.LogError("[Error] Save file not found in " + path);
            return null;
        }
    }
}