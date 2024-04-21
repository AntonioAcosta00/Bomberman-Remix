using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelTimer : MonoBehaviour
{
    public bool Timer;
    public float The_Timer;
    public GameObject DelObject;

    // Update is called once per frame
    void Update()
    {
        // Timer to delete an object
        if (Timer)
        {
            The_Timer -= Time.deltaTime;
            if (The_Timer <= 0)
            {
                Destroy(DelObject);
                Timer = false;
            }
        }
    }
}
