using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate_Timer : MonoBehaviour
{
    public bool Deactivate_Self;
    public bool Timer;
    public float The_Timer;
    public GameObject Turn_Off;

    // Update is called once per frame
    void Update()
    {
        // Timer to activate an object
        if (Timer)
        {
            The_Timer -= Time.deltaTime;
            if (The_Timer <= 0)
            {
                if (Deactivate_Self)
                {
                    gameObject.SetActive(false);
                }
                Turn_Off.SetActive(false);
                Timer = false;
            }
        }
    }
}
