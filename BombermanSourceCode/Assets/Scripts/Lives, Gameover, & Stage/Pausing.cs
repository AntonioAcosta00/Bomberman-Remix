using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausing : MonoBehaviour
{
    public bool Paused;
    float PrevTimeScale = 1;
    public static bool IsPaused;
    public AudioSource Pause_Sfx;
    public static bool CantPause;
    // Start is called before the first frame update
    void Start()
    {
        Pause_Sfx.ignoreListenerPause = true;
    }

    // Update is called once per frame
    void Update()
    {
        Paused = IsPaused;
        if (!CantPause)
        {
            if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.P))
            {
                Pause_Status();
                Pause_Sfx.Play();
            }
        }
    }

    // Pauses the screen
    public void Pause_Status()
    {
        if (Time.timeScale > 0)
        {
            PrevTimeScale = Time.timeScale;
            Time.timeScale = 0;
            AudioListener.pause = true;
            IsPaused = true;
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = PrevTimeScale;
            AudioListener.pause = false;
            IsPaused = false;
        }
    }

}
