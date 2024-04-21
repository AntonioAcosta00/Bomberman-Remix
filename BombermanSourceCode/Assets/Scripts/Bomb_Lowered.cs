using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Lowered : MonoBehaviour
{
    [Space(2f)]
    [Header("Explosion Range Detection")]
    public GameObject Player;
    public PlayerMovement Player_Control;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Player_Control = Player.GetComponent<PlayerMovement>();
        Player_Control.Bombs_Dropped--;
    }
}
