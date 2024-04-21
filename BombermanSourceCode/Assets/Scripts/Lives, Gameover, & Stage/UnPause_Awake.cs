using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnPause_Awake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Unpause the game
        Time.timeScale = 1;
    }
}
