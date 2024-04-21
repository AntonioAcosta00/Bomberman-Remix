using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Points_Loaded : MonoBehaviour
{
    public string saveDirectory = "Highscore";
    public string saveName = "Highscore_DO_NOT_DELETE";
    public Highscore The_Highscore;

    // Access and return the current highscore
    public int The_Curr_Highscore()
    {
        if (!Directory.Exists(saveDirectory) || !File.Exists(saveDirectory + "/" + saveName + ".bin"))
        {
            The_Highscore.Curr_Highscore = 0;
            return 0;
        }
        else
        {
            BinaryFormatter formatter = new BinaryFormatter();

            // Choosing the saved file to open
            FileStream saveFile = File.Open(saveDirectory + "/" + saveName + ".bin", FileMode.Open);

            // Convert the file data into SaveGameData format for use in game
            Highscore loadData = (Highscore)formatter.Deserialize(saveFile);
            saveFile.Close();
            return loadData.Curr_Highscore;
        }
    }
}
