using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The main control for not only player movement but also bomb placement and potentially more
public class PlayerMovement : MonoBehaviour
{
    public bool Dying;
    public bool Dead;
    private Transform Player_Pos;
    [Header("Booleans")]
    public bool CanPlaceBomb = true;
    public bool Inside_Wall;
    public bool Lay_Bomb;
    private bool Stop = true;
    [Space(2f)]
    [Header("Bombs and Cooldown")]
    public static bool Remote_Control;
    public bool RC_Status;
    public int Bombs_Dropped;
    public int Bombs_Maxed_Status;
    public static int Bombs_Maxed;
    public float Cooldown;
    public float Cooldown_OG;
    public GameObject Spawn_Bombs;
    public BombController BombEffects;
    [Space(2f)]
    [Header("Invincibility and Fire Proof")]
    public static bool FireProof;
    public bool FireProof_Status;
    public bool Invincible;
    public float I_Timer;
    public float I_Timer_Max;
    [Space(2f)]
    [Header("Passing Through PowerUps")]
    public bool Wall_Status;
    public bool Bomb_Status;
    public bool Fire_Status;
    public static bool Wall_Pass;
    public static bool Bomb_Pass;
    public static bool Fire_Pass;
    [Space(2f)]
    [Header("Player Movement")]
    public Rigidbody2D rb;
    public float moveSpeed;
    public Vector2 PlayerInput;
    public Animator Player_Anim;
     SpriteRenderer Player_Spr;
    private AudioSource Main_Music;
    private AudioSource Powerup_Music;
    void Start()
    {
        Player_Pos = gameObject.GetComponent<Transform>();
        Player_Spr = gameObject.GetComponent<SpriteRenderer>();
        BombEffects = Spawn_Bombs.GetComponent<BombController>();
        Player_Anim = GetComponent<Animator>();
        Main_Music = GameObject.FindWithTag("Main Music").GetComponent<AudioSource>();
        Powerup_Music = GameObject.FindWithTag("Powerup Music").GetComponent<AudioSource>();
    }

    void Update()
    {
        RC_Status = Remote_Control;
        Wall_Pass = Wall_Status;
        Bomb_Status = Bomb_Pass;
        Fire_Status = Fire_Pass;
        Bombs_Maxed_Status = Bombs_Maxed;
        if (!Dying)
        {
            if (!(Pausing.IsPaused))
            {
                if (Invincible)
                {
                    float random = Random.Range(0, 1f);
                    Player_Spr.color = new Color(1f, 1f, 1f, random);
                    I_Timer -= Time.deltaTime;
                    if (I_Timer <= 0)
                    {
                        Invincible = false;
                        Player_Spr.color = new Color(1f, 1f, 1f, 1f);
                        I_Timer = I_Timer_Max;
                    }
                }


                if (Bombs_Dropped < 0)
                {
                    Bombs_Dropped = 0;
                }

                // Allows the player to place a bomb (Used to fix a bug)
                if (CanPlaceBomb == false && Lay_Bomb == false && Cooldown == Cooldown_OG && Inside_Wall)
                {
                    CanPlaceBomb = true;
                }

                rb.simulated = true;
                // Player input movement
                PlayerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

                // Player animation
                if (PlayerInput != Vector2.zero)
                {
                    Player_Anim.SetBool("isWalking", true);
                    Player_Anim.SetFloat("input_x", PlayerInput.x);
                    Player_Anim.SetFloat("input_y", PlayerInput.normalized.y);
                }
                else
                {
                    Player_Anim.SetBool("isWalking", false);
                }

                // Bomb placement 
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))
                {
                    if (!(Pausing.IsPaused))
                    {
                        if (CanPlaceBomb && !Inside_Wall)
                        {
                            Lay_Bomb = true;
                            if (Stop)
                            {
                                // Stop spawning bombs if the total amount of bomb placement has reached its max
                                if (Bombs_Dropped < Bombs_Maxed)
                                {
                                    Instantiate(Spawn_Bombs, Player_Pos.position, Player_Pos.rotation);
                                    Bombs_Dropped++;
                                }
                                else
                                {
                                    Stop = false;
                                }
                            }
                        }
                    }
                }

                // Bomb Cooldown timer that will reset the total amount of bombs placed
                if (Lay_Bomb)
                {
                    Cooldown -= Time.deltaTime;
                    if (Cooldown <= 0)
                    {
                        Stop = true;
                        Lay_Bomb = false;
                        Cooldown = Cooldown_OG;
                        if (!RC_Status)
                        {
                            Bombs_Dropped = 0;
                        }
                    }
                }
            }
        }
        else
        {
            // Prevents player from moving while the dying phase is active 
            moveSpeed = 0;
            rb.simulated = false;
            PlayerInput = Vector2.zero;
            Player_Anim.Play("Dead");
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            Main_Music.enabled = false;
            Powerup_Music.enabled = false;
        }
    }

    // Updates the player's movement & speed by using it's Rigidbody
    void FixedUpdate()
    {
        if (!Dying)
        {
            if (!(Pausing.IsPaused))
            {
                Vector2 moveForce = PlayerInput * moveSpeed;
                rb.velocity = moveForce;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }


    // All 3 of these triggers prevent the player from placing a bomb on top of another bomb, the exit door, Spawny, and walls
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bomb"))
        {
            CanPlaceBomb = true;
        }
        if (collision.CompareTag("Breakable2") || collision.CompareTag("Door") || collision.CompareTag("Spawny"))
        {
            Inside_Wall = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bomb"))
        {
            CanPlaceBomb = false;
        }

        if (collision.CompareTag("Breakable2") || collision.CompareTag("Door") || collision.CompareTag("Spawny"))
        {
            Inside_Wall = true;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Bomb"))
        {
            CanPlaceBomb = false;
        }
        if (collision.CompareTag("Breakable2") || collision.CompareTag("Door") || collision.CompareTag("Spawny"))
        {
            Inside_Wall = true;
        }
    }
}
