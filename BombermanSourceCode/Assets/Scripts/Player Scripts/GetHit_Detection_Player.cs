using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHit_Detection_Player : MonoBehaviour
{
    public bool Hit;
    public PlayerMovement Player;
    public AudioSource Died;
    private int Once = 1;

    // Detects & reacts to gameobjects with specific tags
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Actives the Dying phase if player touches the enemy or the explosion
        if (!Player.Invincible)
        {
            if (!Player.FireProof_Status)
            {
                if (collision.CompareTag("Explosion")|| collision.CompareTag("Explosion2"))
                {
                    if (Once == 1)
                    {
                        Died.Play();
                        Player.Dying = true;
                        Hit = true;
                        Once++;
                    }
                }
            }
            if (collision.CompareTag("Enemies"))
            {
                if (Once == 1)
                {
                    Died.Play();
                    Player.Dying = true;
                    Hit = true;
                    Once++;
                }
            }
        }
    }
}
