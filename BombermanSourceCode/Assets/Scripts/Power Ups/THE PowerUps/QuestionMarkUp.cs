using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/QuestionMark")]
public class QuestionMarkUp : PowerupEffects
{
    // Makes the Player invincible
    public override void Apply(GameObject target)
    {
        if (target.GetComponent<PlayerMovement>().Invincible)
        {
            target.GetComponent<PlayerMovement>().I_Timer += target.GetComponent<PlayerMovement>().I_Timer_Max;
        }
        else
        {
            target.GetComponent<PlayerMovement>().Invincible = true;
        }    
    }
}
