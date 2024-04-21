using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawny_Near_Player : MonoBehaviour
{
    public bool Happy;
    public Animator Spawny_Anim;
    public string Dir_Look;

    // All 3 of these triggers prevent the player from placing a bomb on top of another bomb
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bomb"))
        {
            Spawny_Anim.Play("S_Looking");
            Happy = false;
        }
    }

    // Change Spawny's expression when near a bomb
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bomb"))
        {
            Happy = true;
            LookingAt();
        }
    }

    // Changing of expression
    public void LookingAt()
    {
        Spawny_Anim.Play(Dir_Look);
    }
}
