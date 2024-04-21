using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Gameover_Menu : MonoBehaviour
{
    public Points_Saved Saving_P;
    public Points_Loaded Loading_P;
    public Lives_System Curr_Stage;
    private DontDestroy Stage_Destroy;
    private DontDestroy Tracker_Destroy;
    public GameObject Highscore;
    public TextMeshProUGUI Code;
    public TextMeshProUGUI Code_Shadow;
    public TextMeshProUGUI Score;
    public TextMeshProUGUI Score_Shadow;
    private int Once = 1;

    // Start is called before the first frame update
    void Start()
    {
        GameObject find = GameObject.FindWithTag("Stage n Life Sys");
        GameObject find2 = GameObject.FindWithTag("F_Tracker");
        Tracker_Destroy = find.GetComponent<DontDestroy>();
        Curr_Stage = find.GetComponent<Lives_System>();
        Stage_Destroy = find.GetComponent<DontDestroy>();
        Code.text = Curr_Stage.Stage_Password(Lives_System.Stages);
        Code_Shadow.text = Code.text;
        Stage_Destroy.On = false;
        Tracker_Destroy.On = false;
        Lives_System.Lives = 2;
        Lives_System.Stages = 1;

        if (Curr_Stage.The_Score > Loading_P.The_Curr_Highscore())
        {
            Highscore.SetActive(true);
            Saving_P.The_Highscore.Curr_Highscore = Curr_Stage.The_Score;
            Saving_P.SaveToFile();
        }

        Score.text = Curr_Stage.The_Score.ToString();
        Score_Shadow.text = Score.text;
    }

    // Update is called once per frame
    void Update()
    {
        // Back to menu
        if ((Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.RightShift)) && (!Input.GetKeyDown(KeyCode.Return) && !Input.GetKeyDown(KeyCode.Z)))
        {
            if (Once == 1)
            {
                // Load Menu
                SceneManager.LoadScene(0);
                Once++;
            }
        }
        // Try again from the start
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z)) && (!Input.GetKeyDown(KeyCode.X) || !Input.GetKeyDown(KeyCode.RightShift)))
        {
            if (Once == 1)
            {
                // Load Level
                SceneManager.LoadScene(1);
                Once++;
            }
        }
    }
}
