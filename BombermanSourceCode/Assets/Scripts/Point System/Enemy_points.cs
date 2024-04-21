using UnityEngine;
using TMPro; // Import this namespace for TextMeshPro

public class Enemy : MonoBehaviour
{
    public bool Spawned_In;
    public float Timeleft = 0.5f;
    public int pointsValue = 100; // Points to add when destroyed by an explosion

    // Reference to your score TextMeshPro UI element
    // Make sure to assign this in the Inspector
    public ScoreDisplay scoreText;
    public GameObject Delete_this;
    public GameObject Spawn_This;
    public Transform Spawn_This_Parent;
    private ExitDoor_Control Door;
    private AudioSource Death_Sfx;
    private int Once = 1;
    private void Start()
    {
        if (ExitDoor_Control.DoorSwpawned)
        {
            Door = GameObject.FindWithTag("Door").GetComponent<ExitDoor_Control>();
            Door.Invoke("Awake", 0.5f);
        }
        scoreText = GameObject.FindWithTag("ScoreText").GetComponent<ScoreDisplay>();
        Death_Sfx = GameObject.FindWithTag("Enemy Death").GetComponent<AudioSource>();
        Spawn_This_Parent = GameObject.FindWithTag("Music Sys").GetComponent<Transform>();
    }

    void Update()
    {
        // Invincibility Timer, so the Enemy won't be destroyed instantly
        if (Spawned_In)
        {
            Timeleft -= Time.deltaTime;
            if (Timeleft <= 0)
            {
                Spawned_In = false;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Update the total number of points and the remaining amount of enemies left. Then destroy itself.
        if (other.CompareTag("Explosion"))
        {
            scoreText.AddPoints(pointsValue);
            if (!Spawned_In)
            {
                if (ExitDoor_Control.DoorSwpawned)
                {
                    Door = GameObject.FindWithTag("Door").GetComponent<ExitDoor_Control>();
                    Door.Invoke("Awake", 0.5f);
                }
            }

            if (Once == 1)
            {
                (Instantiate(Spawn_This, gameObject.transform.position, gameObject.transform.rotation) as GameObject).transform.parent = Spawn_This_Parent.transform;
                Death_Sfx.PlayDelayed(0.2f);
                Once++;
            }
            Destroy(Delete_this); // Destroy enemy
        }
    }
}
