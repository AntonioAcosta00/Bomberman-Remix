using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreen_Tracker : MonoBehaviour
{
    public bool Check_Full;
    public bool Check_Win;
    public bool On;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        Screen.SetResolution(640, 480, false);
    }

    // Update is called once per frame
    void Update()
    {
        // Checks to see if in full screen or window
        if (On)
        {
            if (Screen.fullScreen)
            {
                Full_Mode();
                Check_Win = false;
            }
            else
            {
                Win_Mode();
                Check_Full = false;
            }
        }
    }

    // Change into the right size for full screen mode
    public void Full_Mode()
    {
        if (Check_Full == false)
        {
            Screen.SetResolution(640, 480, FullScreenMode.FullScreenWindow);
            Check_Full = true;
        }
    }

    // Change into the right size for full screen mode
    public void Win_Mode()
    {
        if (Check_Win == false)
        {
            Screen.SetResolution(640, 480, FullScreenMode.Windowed);
            Check_Win = true;
        }
    }
}
