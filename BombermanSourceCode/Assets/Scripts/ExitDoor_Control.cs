using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor_Control : MonoBehaviour
{
    public static bool DoorSwpawned;
    public bool Checking;
    public bool Door_Spawn;
    public bool Enemies_Defeated;
    public AudioSource Enemies_Clear_Sfx;
    public GameObject[] Enemies;
    public BoxCollider2D Hitbox;
    public BoxCollider2D Finish_Hitbox;
    private int Once;

    // Start is called before the first frame update
    void Awake()
    {
        Door_Spawn = true;
        DoorSwpawned = true;
        Hitbox = gameObject.GetComponent<BoxCollider2D>();
        Check_Enemies();
    }

    // Checks to see if there are any enemies on the stage, if not then the player can continue to the next level
    public void Check_Enemies()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemies");
        if (Enemies == null || Enemies.Length == 0)
        {
            Hitbox.isTrigger = true;
            Enemies_Defeated = true;
            Finish_Hitbox.isTrigger = true;
            Checking = false;
            All_Clear_Sfx();
        }
        else
        {
            Hitbox.isTrigger = false;
            Enemies_Defeated = false;
            Finish_Hitbox.isTrigger = false;
            Checking = true;
            Once = 0;
        }
    }

    // Play the clear sfx once
    public void All_Clear_Sfx()
    {
        if (Once == 0)
        {
            Enemies_Clear_Sfx.Play();
            Once++;
        }
    }

}
