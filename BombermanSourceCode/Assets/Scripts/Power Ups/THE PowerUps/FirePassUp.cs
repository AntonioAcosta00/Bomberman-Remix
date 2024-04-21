using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/FirePass")]
public class FirePassUp : PowerupEffects
{
    // Allows the player to pass through explosions 
    public override void Apply(GameObject target)
    {
        PlayerMovement.Fire_Pass = true;
        Physics2D.IgnoreLayerCollision(7, 9);
    }
}