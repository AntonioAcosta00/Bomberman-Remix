using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    private BoxCollider2D Box;
    [Header("Booleans")]
    public bool Activate;
    public bool Timer;
    private bool tick = true;
    public bool cutoff_point = true;
    private bool finish;
    private bool SetOff;
    public bool[] Stop_Spawning;
    [Space(2f)]
    [Header("Firepower and Trackers")]
    public int Firepower;
    public int Firepower_Setter;
    public float Detonator_Timer = 2;
    public int[] Total_Amount_Tracker;
    public float X_Distance = 1.0f;
    public float Y_Distance = 1.0f;
    [Space(2f)]
    [Header("Sprites and Animators")]
    public SpriteRenderer[] Explosions;
    public GameObject[] Explosions_Animators;
    public GameObject[] Explosions_End_Animators;
    [Space(2f)]
    [Header("Explosion Setup")]
    public GameObject Bomb;
    public GameObject The_Explosion_Set;
    public GameObject[] The_Explosion_Spawn;
    public GameObject[] The_Explosion_End_Spawn;
    [Space(2f)]
    [Header("Explosion Parent Directions")]
    public GameObject[] Parents;
    public Transform[] The_Parents;
    public Vector2[] Parent_vectors;
    [Space(2f)]
    [Header("Explosion Range Detection")]
    public int[] Distance_Tracker;
    public GameObject[] The_Hitbox_Detection;
    public Bomb_Ray_Detectoin[] Ray_Hitbox_Detection;
    public Bomb_Ray_Detectoin[] Ray_Children;
    private AudioSource Explosion_Sfx;
    private int Once = 1;

    // Start is called before the first frame update
    void Awake()
    {
        Explosion_Sfx = GameObject.FindWithTag("Bomb Sfx").GetComponent<AudioSource>();
        if (PlayerMovement.Remote_Control)
        {
            Timer = false;
        }
        Box = gameObject.GetComponent<BoxCollider2D>();
        Firepower = Firepower_Setter;
        // Top Explosion
        Parent_vectors[0] = new Vector2(The_Parents[0].position.x, The_Parents[0].position.y + Y_Distance);
        Ray_Hitbox_Detection[0].Range = Firepower + 1.5f;
        Ray_Children[0].Range = Firepower + 1.5f;
        // Right Explosion
        Parent_vectors[1] = new Vector2(The_Parents[1].position.x + X_Distance, The_Parents[1].position.y);
        Ray_Hitbox_Detection[1].Range = Firepower + 1.5f;
        Ray_Children[1].Range = Firepower + 1.5f;
        // Bottom Explosion
        Parent_vectors[2] = new Vector2(The_Parents[2].position.x, The_Parents[2].position.y - Y_Distance);
        Ray_Hitbox_Detection[2].Range = -Firepower + -1.5f;
        Ray_Children[2].Range = -Firepower + -1.5f;
        // Left Explosion
        Parent_vectors[3] = new Vector2(The_Parents[3].position.x - X_Distance, The_Parents[3].position.y);
        Ray_Hitbox_Detection[3].Range = -Firepower + -1.5f;
        Ray_Children[3].Range = -Firepower + -1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        // Starts up the explosion range setup  
        if (tick)
        {
            // Stop increasing explosion range if the hitboxes have collided with a wall or destroyable wall

            // Top hitbox Detection
            if (Ray_Hitbox_Detection[0].Hit || Ray_Hitbox_Detection[0].Hit2 || Ray_Children[0].Hit || Ray_Children[0].Hit2)
            {
                Distance_Tracker[0] = Mathf.FloorToInt(Mathf.Min(Ray_Hitbox_Detection[0].The_distance, Ray_Children[0].The_distance));
                Stop_Spawning[0] = true;
            }
            else if ((!Ray_Hitbox_Detection[0].Hit && !Ray_Hitbox_Detection[0].Hit2) || (!Ray_Children[0].Hit && !Ray_Children[0].Hit2))
            {
                Distance_Tracker[0] = Firepower;
                Stop_Spawning[0] = false;
            }

            // Right hitbox Detection
            if (Ray_Hitbox_Detection[1].Hit || Ray_Hitbox_Detection[1].Hit2 || Ray_Children[1].Hit || Ray_Children[1].Hit2)
            {
                Distance_Tracker[1] = Mathf.FloorToInt(Mathf.Min(Ray_Hitbox_Detection[1].The_distance, Ray_Children[1].The_distance));
                Stop_Spawning[1] = true;
            }
            else if ((!Ray_Hitbox_Detection[1].Hit && !Ray_Hitbox_Detection[1].Hit2) || (!Ray_Children[1].Hit && !Ray_Children[1].Hit2))
            {
                Distance_Tracker[1] = Firepower;
                Stop_Spawning[1] = false;
            }

            // Bottom hitbox Detection
            if (Ray_Hitbox_Detection[2].Hit || Ray_Hitbox_Detection[2].Hit2 || Ray_Children[2].Hit || Ray_Children[2].Hit2)
            {
                Distance_Tracker[2] = Mathf.FloorToInt(Mathf.Min(Ray_Hitbox_Detection[2].The_distance, Ray_Children[2].The_distance));
                Stop_Spawning[2] = true;
            }
            else if ((!Ray_Hitbox_Detection[2].Hit && !Ray_Hitbox_Detection[2].Hit2) || (!Ray_Children[2].Hit && !Ray_Children[2].Hit2))
            {
                Distance_Tracker[2] = Firepower;
                Stop_Spawning[2] = false;
            }

            // Left hitbox Detection
            if (Ray_Hitbox_Detection[3].Hit || Ray_Hitbox_Detection[3].Hit2 || Ray_Children[3].Hit || Ray_Children[3].Hit2)
            {
                Distance_Tracker[3] = Mathf.FloorToInt(Mathf.Min(Ray_Hitbox_Detection[3].The_distance, Ray_Children[3].The_distance));
                Stop_Spawning[3] = true;
            }
            else if ((!Ray_Hitbox_Detection[3].Hit && !Ray_Hitbox_Detection[3].Hit2) || (!Ray_Children[3].Hit && !Ray_Children[3].Hit2))
            {
                Distance_Tracker[3] = Firepower;
                Stop_Spawning[3] = false;
            }

            // Activate the next part before just Detonator_Timer ends
            if ((Detonator_Timer - 0.5) <= 0)
            {
                tick = false;
                Activate = true;
            }
        }

        if (Activate)
        {
            // Increases the explosion range for all 4 sides 
            for (int i = 0; i < Firepower; i++)
            {
                // Top Explosion side
                if (i < Distance_Tracker[0])
                {
                    if (i == Distance_Tracker[0] - 1 && Stop_Spawning[0])
                    {
                        Parent_vectors[0].y = Parent_vectors[0].y - 0.5f;
                    }
                    if (i == Distance_Tracker[0] - 1 && !Stop_Spawning[0])
                    {
                        (Instantiate(The_Explosion_End_Spawn[0], Parent_vectors[0], The_Parents[0].rotation) as GameObject).transform.parent = The_Parents[0].transform;
                    }
                    else
                    {
                        (Instantiate(The_Explosion_Spawn[0], Parent_vectors[0], The_Parents[0].rotation) as GameObject).transform.parent = The_Parents[0].transform;
                    }

                    The_Hitbox_Detection[0].GetComponent<Transform>().position = Parent_vectors[0];
                    Parent_vectors[0].y += Y_Distance;
                    Total_Amount_Tracker[0]++;
                }

                // Right Explosion side
                if (i < Distance_Tracker[1])
                {
                    if (i == Distance_Tracker[1] - 1 && Stop_Spawning[1])
                    {
                        Parent_vectors[1].x = Parent_vectors[1].x - 0.5f;
                    }
                    if (i == Distance_Tracker[1] - 1 && !Stop_Spawning[1])
                    {
                        (Instantiate(The_Explosion_End_Spawn[1], Parent_vectors[1], The_Parents[1].rotation) as GameObject).transform.parent = The_Parents[1].transform;
                    }
                    else
                    {
                        (Instantiate(The_Explosion_Spawn[1], Parent_vectors[1], The_Parents[1].rotation) as GameObject).transform.parent = The_Parents[1].transform;
                    }
                    The_Hitbox_Detection[1].GetComponent<Transform>().position = Parent_vectors[1];
                    Parent_vectors[1].x += X_Distance;
                    Total_Amount_Tracker[1]++;
                }

                // Bottom Explosion side
                if (i < Distance_Tracker[2])
                {
                    if (i == Distance_Tracker[2] - 1 && Stop_Spawning[2])
                    {
                        Parent_vectors[2].y = Parent_vectors[2].y + 0.5f;
                    }
                    if (i == Distance_Tracker[2] - 1 && !Stop_Spawning[2])
                    {
                        (Instantiate(The_Explosion_End_Spawn[2], Parent_vectors[2], The_Parents[2].rotation) as GameObject).transform.parent = The_Parents[2].transform;
                    }
                    else
                    {
                        (Instantiate(The_Explosion_Spawn[2], Parent_vectors[2], The_Parents[2].rotation) as GameObject).transform.parent = The_Parents[2].transform;
                    }
                    The_Hitbox_Detection[2].GetComponent<Transform>().position = Parent_vectors[2];
                    Parent_vectors[2].y -= Y_Distance;
                    Total_Amount_Tracker[2]++;
                }

                // Left Explosion side
                if (i < Distance_Tracker[3])
                {
                    if (i == Distance_Tracker[3] - 1 && Stop_Spawning[3])
                    {
                        Parent_vectors[3].x = Parent_vectors[3].x + 0.5f;
                    }
                    if (i == Distance_Tracker[3] - 1 && !Stop_Spawning[3])
                    {
                        (Instantiate(The_Explosion_End_Spawn[3], Parent_vectors[3], The_Parents[3].rotation) as GameObject).transform.parent = The_Parents[3].transform;
                    }
                    else
                    {
                        (Instantiate(The_Explosion_Spawn[3], Parent_vectors[3], The_Parents[3].rotation) as GameObject).transform.parent = The_Parents[3].transform;
                    }
                    The_Hitbox_Detection[3].GetComponent<Transform>().position = Parent_vectors[3];
                    Parent_vectors[3].x -= X_Distance;
                    Total_Amount_Tracker[3]++;
                }
            }
            Activate = false;
            cutoff_point = true;
        }

        // The cutoff point for when an explosion reaches a wall or a destroyable wall (prevents the explosion from going through walls)
        if (cutoff_point)
        {
            for (int i = 0; i <= 3; i++)
            {
                if (Firepower > 0)
                {
                    if (Total_Amount_Tracker[i] == 0 && (Ray_Hitbox_Detection[i].Hit && Ray_Children[i].Hit2 || (Ray_Hitbox_Detection[i].Hit2 && Ray_Children[i].Hit)))
                    {
                        if (Ray_Hitbox_Detection[i].The_distance <= 0.7f)
                        {
                            Explosions[i].enabled = false;
                            Reduce_Range(i);
                        }
                    }

                    else if (Total_Amount_Tracker[i] == 0 && (Ray_Hitbox_Detection[i].Hit || Ray_Children[i].Hit))
                    {
                        if (Ray_Hitbox_Detection[i].The_distance <= 0.7f)
                        {
                            Parents[i].GetComponent<Rigidbody2D>().simulated = false;
                            Parents[i].SetActive(false);
                        }
                    }
                    else if (Total_Amount_Tracker[i] == 0 && (Ray_Hitbox_Detection[i].Hit2 || Ray_Children[i].Hit2))
                    {
                        if (Ray_Hitbox_Detection[i].The_distance <= 0.7f)
                        {
                            Explosions[i].enabled = false;
                            Reduce_Range(i);
                        }
                    }
                    if (Total_Amount_Tracker[i] > 0 && Ray_Hitbox_Detection[i].Hit)
                    {
                        Parents[i].GetComponent<Rigidbody2D>().simulated = false;
                        Parents[i].SetActive(false);
                    }
                }
                else
                {
                    if (Ray_Hitbox_Detection[i].Hit && Ray_Children[i].Hit2 || (Ray_Hitbox_Detection[i].Hit2 && Ray_Children[i].Hit))
                    {
                        if (Ray_Hitbox_Detection[i].The_distance <= 0.7f)
                        {
                            Explosions[i].enabled = false;
                            Reduce_Range(i);
                        }
                    }
                    else if (Ray_Hitbox_Detection[i].Hit || Ray_Children[i].Hit)
                    {
                        if (Ray_Hitbox_Detection[i].The_distance <= 0.7f)
                        {
                            Parents[i].GetComponent<Rigidbody2D>().simulated = false;
                            Parents[i].SetActive(false);
                        }

                    }
                    else if (Ray_Hitbox_Detection[i].Hit2 || Ray_Children[i].Hit2)
                    {
                        if (Ray_Hitbox_Detection[i].The_distance <= 0.7f)
                        {
                            Explosions[i].enabled = false;
                            Reduce_Range(i);
                        }

                    }
                    else if ((!Ray_Hitbox_Detection[i].Hit && !Ray_Hitbox_Detection[i].Hit2) || (!Ray_Children[i].Hit && !Ray_Children[i].Hit2))
                    {
                        Explosions_Animators[i].SetActive(false);
                        Explosions_End_Animators[i].SetActive(true);
                    }
                }
            }
            cutoff_point = false;
            finish = true;
        }

        // Allows the player to activate the bombs whenever they push X and shift
        if (PlayerMovement.Remote_Control)
        {
            if (!(Pausing.IsPaused))
            {
                if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.LeftShift))
                {
                    Detonator_Timer = 0;
                    SetOff = true;
                }
                if (finish && SetOff)
                {
                    Bomb.SetActive(false);
                    The_Explosion_Set.SetActive(true);
                    Box.isTrigger = true;
                    if (Once == 1)
                    {
                        Explosion_Sfx.Play();
                        Once++;
                    }
                }
            }
        }

        // Timer when the explosion happens
        if (Timer)
        {
            Detonator_Timer -= Time.deltaTime;
            if (Detonator_Timer <= 0)
            {
                Bomb.SetActive(false);
                The_Explosion_Set.SetActive(true);
                if (Once == 1)
                {
                    Explosion_Sfx.Play();
                    Once++;
                }
                Timer = false;
                Box.isTrigger = true;

            }
        }
    }

    // Reduces the positions of the final hitbox of the explosion so that it doesn't clip into other blocks
    void Reduce_Range(int i)
    {
        if (i == 0)
        {
            Vector2 P_V = new Vector2(The_Parents[0].position.x, The_Parents[0].position.y);
            P_V.y = P_V.y - 0.5f;
            The_Parents[0].position = P_V;
        }
        else if (i == 1)
        {
            Vector2 P_V = new Vector2(The_Parents[1].position.x, The_Parents[1].position.y);
            P_V.x = P_V.x - 0.5f;
            The_Parents[1].position = P_V;

        }
        else if (i == 2)
        {
            Vector2 P_V = new Vector2(The_Parents[2].position.x, The_Parents[2].position.y);
            P_V.y = P_V.y + 0.5f;
            The_Parents[2].position = P_V;
        }
        else if (i == 3)
        {
            Vector2 P_V = new Vector2(The_Parents[3].position.x, The_Parents[3].position.y);
            P_V.x = P_V.x + 0.5f;
            The_Parents[3].position = P_V;
        }
    }


    // Allows the player to stand inside the bomb when placed, but when they move of its hitbox they can't go through it anymore
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Detonator_Timer > 0 && !PlayerMovement.Bomb_Pass)
            {
                Box.isTrigger = false;
            }
        }
    }

    // Detonates instantly when colliding with an explosion
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion"))
        {
            Detonator_Timer = 0;
            SetOff = true;
        }
    }

    // Detonates instantly when colliding with an explosion
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion"))
        {
            Detonator_Timer = 0;
            SetOff = true;
        }
    }

}
