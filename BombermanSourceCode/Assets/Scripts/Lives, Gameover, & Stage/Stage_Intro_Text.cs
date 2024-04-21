using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Stage_Intro_Text : MonoBehaviour
{
    public Lives_System Curr_Stage;
    public TextMeshProUGUI Stage_num;
    public TextMeshProUGUI Stage_num_Shadow;
    public TextMeshProUGUI Lives_num;
    public TextMeshProUGUI Lives_Shadow_Num;

    // Start is called before the first frame update
    void Start()
    {
        // Update the UI text display for Stage and Lives
        Stage_num.text = Lives_System.Stages.ToString();
        Stage_num_Shadow.text = Stage_num.text;

        Lives_num.text = Lives_System.Lives.ToString();
        Lives_num.text = Lives_Shadow_Num.text;
    }
}
