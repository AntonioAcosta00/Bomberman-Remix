using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Detection : MonoBehaviour
{
    public bool Hit;
    public bool Hit2;

    // Detects & reacts to gameobjects with specific tags
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") || collision.CompareTag("Unbreakable"))
        {
            Hit = true;
        }
        if (collision.CompareTag("Breakable") || collision.CompareTag("Wall2"))
        {
            Hit2 = true;
        }
    }


    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") || collision.CompareTag("Unbreakable"))
        {
            Hit = true;
        }
        if (collision.CompareTag("Breakable") || collision.CompareTag("Wall2"))
        {
            Hit2 = true;
        }
    }


    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Breakable"))
        {
            Hit2 = false;
        }
    }
}
