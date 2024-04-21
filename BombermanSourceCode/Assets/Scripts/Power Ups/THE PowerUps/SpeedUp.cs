using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/SpeedUp")]
public class SpeedUp : PowerupEffects
{
    // Increases the player's movement speed
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerMovement>().moveSpeed += 0.5f;
    }
}
