using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Setup : MonoBehaviour
{
    public bool Full_Restart;
    public BombController BombEffects;
    public RandomPlacementsOnTilemap Spawny_Sys;
    public LevelFill_Enemies Enemy_Sys;
    public SpriteRenderer Background;
    public Color32[] Color_Stages;
    public GameObject[] The_Enemies;
    public GameObject[] Spawny_Levels;
    // Start is called before the first frame update
    void Start()
    {
        // Reset all upgrades, lives, and current stage
        if (Full_Restart)
        {
            if (Title_Controls.Continue_Load)
            {
                Title_Controls.Continue_Load = false;
            }
            else
            {
                Lives_System.Stages = 1;
            }
            BombEffects.Firepower_Setter = 0;
            PlayerMovement.Remote_Control = false;
            PlayerMovement.Wall_Pass = false;
            PlayerMovement.Bomb_Pass = false;
            PlayerMovement.Fire_Pass = false;
            PlayerMovement.FireProof = false;
            PlayerMovement.Bombs_Maxed = 1;
            Physics2D.IgnoreLayerCollision(7, 9, false);
            Physics2D.IgnoreLayerCollision(7, 8, false);
            Lives_System.Lives = 2;
        }
        GetHit_Detection.Exit = 1;
        ExitDoor_Control.DoorSwpawned = false;
        Time.timeScale = 1;
        Stage_Color();
        Stage_Enemies();
    }

    // Changes the color of the stage every 10th floor.
    public void Stage_Color()
    {
        if (Lives_System.Stages >= 1 && Lives_System.Stages < 11)
        {
            Background.color = Color_Stages[0];
        }
        else if (Lives_System.Stages >= 11 && Lives_System.Stages < 21)
        {
            Background.color = Color_Stages[1];
        }
        else if (Lives_System.Stages >= 21 && Lives_System.Stages < 31)
        {
            Background.color = Color_Stages[2];
        }
        else if (Lives_System.Stages >= 31 && Lives_System.Stages < 41)
        {
            Background.color = Color_Stages[3];
        }
        else if (Lives_System.Stages >= 41 && Lives_System.Stages < 51)
        {
            Background.color = Color_Stages[4];
        }
    }

    // Increase the difficulty  of each stage by increasing the amount of enemies and Spawny that spawn at the start
    public void Stage_Enemies()
    {
        if (Lives_System.Stages >= 1 && Lives_System.Stages < 3)
        {
            Spawny_Sys.objectToPlacePrefab = Spawny_Levels[0];
            Spawny_Sys.numberOfObjectsToPlace = 3;
            Enemy_Sys.numberOfObjectsToPlace = 3;
            Adding_Enemies(0, 2);
        }
        else if (Lives_System.Stages >= 3 && Lives_System.Stages < 5)
        {
            Enemy_Sys.numberOfObjectsToPlace = 4;
            Adding_Enemies(0, 2);
        }
        else if (Lives_System.Stages >= 5 && Lives_System.Stages < 7)
        {
            Enemy_Sys.numberOfObjectsToPlace = 5;
            Adding_Enemies(0, 2);
        }
        else if (Lives_System.Stages >= 7 && Lives_System.Stages < 11)
        {
            Enemy_Sys.numberOfObjectsToPlace = 6;
            Adding_Enemies(0, 2);
        }
        else if (Lives_System.Stages >= 11 && Lives_System.Stages < 16)
        {
            Spawny_Sys.objectToPlacePrefab = Spawny_Levels[1];
            Spawny_Sys.numberOfObjectsToPlace = 4;
            Enemy_Sys.numberOfObjectsToPlace = 6;
            Adding_Enemies(0, 3);
        }
        else if (Lives_System.Stages >= 16 && Lives_System.Stages < 21)
        {
            Spawny_Sys.objectToPlacePrefab = Spawny_Levels[1];
            Spawny_Sys.numberOfObjectsToPlace = 4;
            Enemy_Sys.numberOfObjectsToPlace = 7;
            Adding_Enemies(1, 3);
        }
        else if (Lives_System.Stages >= 21 && Lives_System.Stages < 26)
        {
            Spawny_Sys.objectToPlacePrefab = Spawny_Levels[2];
            Spawny_Sys.numberOfObjectsToPlace = 5;
            Enemy_Sys.numberOfObjectsToPlace = 7;
            Adding_Enemies(1, 4);
        }
        else if (Lives_System.Stages >= 26 && Lives_System.Stages < 31)
        {
            Spawny_Sys.objectToPlacePrefab = Spawny_Levels[2];
            Spawny_Sys.numberOfObjectsToPlace = 5;
            Enemy_Sys.numberOfObjectsToPlace = 8;
            Adding_Enemies(2, 4);
        }
        else if (Lives_System.Stages >= 31 && Lives_System.Stages < 36)
        {
            Spawny_Sys.objectToPlacePrefab = Spawny_Levels[3];
            Spawny_Sys.numberOfObjectsToPlace = 6;
            Enemy_Sys.numberOfObjectsToPlace = 8;
            Adding_Enemies(2, 4);
        }
        else if (Lives_System.Stages >= 36 && Lives_System.Stages < 41)
        {
            Spawny_Sys.objectToPlacePrefab = Spawny_Levels[3];
            Spawny_Sys.numberOfObjectsToPlace = 6;
            Enemy_Sys.numberOfObjectsToPlace = 8;
            Adding_Enemies(3, 4);
        }
        else if (Lives_System.Stages >= 41 && Lives_System.Stages < 46)
        {
            Spawny_Sys.objectToPlacePrefab = Spawny_Levels[4];
            Spawny_Sys.numberOfObjectsToPlace = 8;
            Enemy_Sys.numberOfObjectsToPlace = 10;
            Adding_Enemies(0, 5);
        }
        else if (Lives_System.Stages >= 46 && Lives_System.Stages < 51)
        {
            Spawny_Sys.objectToPlacePrefab = Spawny_Levels[4];
            Spawny_Sys.numberOfObjectsToPlace = 8;
            Enemy_Sys.numberOfObjectsToPlace = 10;
            Adding_Enemies(3, 5);
        }
    }

    // The amount of enemies that will spawn
    public void Adding_Enemies(int Start, int Amount)
    {
        for (int i = Start; i <= Amount; i++)
        {
            Enemy_Sys.objectToPlacePrefab.Add(The_Enemies[i]);
        }
    }
}
