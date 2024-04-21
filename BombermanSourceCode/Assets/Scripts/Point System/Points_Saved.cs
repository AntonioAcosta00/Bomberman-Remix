using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
public class Points_Saved : MonoBehaviour
{
    public string directoryName = "Highscore";
    public Highscore The_Highscore;

    // Saving the highscore into a file
    public void SaveToFile()
    {
        // To save in a directory, it must be created first
        if (!Directory.Exists(directoryName))
            Directory.CreateDirectory(directoryName);

        // The formatter will convert our unity data type into a binary file
        BinaryFormatter formatter = new BinaryFormatter();

        // Choose the save location
        FileStream saveFile = File.Create(directoryName + "/" + "Highscore_DO_NOT_DELETE" + ".bin");

        // Write our C# Unity game data type to a binary file
        formatter.Serialize(saveFile, The_Highscore);

        saveFile.Close();
    }
}
