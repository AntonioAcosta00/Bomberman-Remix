using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PowerUps/FireUp")]
public class FireUp : PowerupEffects
{
    // Increases the bomb's explosion range
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerMovement>().BombEffects.Firepower_Setter += 1;
    }
}
