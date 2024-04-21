using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/RemoteControl")]
public class RemoteControlUp : PowerupEffects
{
    // Allows the player to denotata bombs on command
    public override void Apply(GameObject target)
    {
        PlayerMovement.Remote_Control = true;
    }
}
