using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/BombPass")]
public class BombPassUp : PowerupEffects
{
    // Allows the player to pass through bombs 
    public override void Apply(GameObject target)
    {
        PlayerMovement.Bomb_Pass = true;
    }
}
