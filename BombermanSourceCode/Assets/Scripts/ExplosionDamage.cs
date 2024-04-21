using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    public bool Hit;

    // Detects & reacts to gameobjects with specific tags
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion"))
        {
            Hit = true;
        }
    }
}
