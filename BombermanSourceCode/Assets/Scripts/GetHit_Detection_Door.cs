using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GetHit_Detection_Door : MonoBehaviour
{
    public bool Complete;
    public bool timer;
    public float Timeleft = 0.5f;
    public float timer_Load;
    public ExitDoor_Control Door;
    public Rigidbody2D Hitbox;
    public AudioSource Complete_Sfx;
    private bool One_Time = true;
    private AudioSource Main_Music;
    private AudioSource Powerup_Music;
    private int Once = 1;
    private PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        Hitbox = gameObject.GetComponent<Rigidbody2D>();
        Complete_Sfx.ignoreListenerPause = true;
        Main_Music = GameObject.FindWithTag("Main Music").GetComponent<AudioSource>();
        Powerup_Music = GameObject.FindWithTag("Powerup Music").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Enables the object to be able to interact with other objects
        if (timer)
        {
            Timeleft -= Time.deltaTime;
            if (Timeleft <= 0)
            {
                Hitbox.simulated = true;
                timer = false;
            }
        }

        // Load to the next stage
        if (Complete)
        {
            timer_Load -= Time.unscaledDeltaTime;
            if (timer_Load <= 0 && Once == 1)
            {
                if (Lives_System.Stages > 50)
                {
                    Lives_System.Stages = 1;
                    SceneManager.LoadScene(2);
                }
                else
                {
                    if (Lives_System.Stages % 5 == 0)
                    {
                        Lives_System.Lives++;
                    }
                    Lives_System.Stages++;
                    SceneManager.LoadScene(2);
                }
                Complete = false;
                Once++;
            }
        }

    }

    // Checking for Trigger collisions with other objects 
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Door.Enemies_Defeated)
            {
                if (One_Time)
                {
                    player = collision.GetComponent<PlayerMovement>();
                    player.Player_Anim.Play("Victory");
                    Main_Music.volume = 0;
                    Powerup_Music.volume = 0;
                    player.enabled = false;
                    Time.timeScale = 0;
                    Pausing.CantPause = true;
                    Complete = true;
                    Complete_Sfx.Play();
                    One_Time = false;
                }

            }
        }
    }

}
