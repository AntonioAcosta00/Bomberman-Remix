using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Restart : MonoBehaviour
{
    public float Timer;
    public PlayerMovement Player;
    public AudioSource Gameover_Sfx;
    private Lives_System The_Lives;
    private int Once = 1;
    private int Once_Again = 1;
    // Start is called before the first frame update
    void Start()
    {
        GameObject find = GameObject.FindWithTag("Stage n Life Sys");
        The_Lives = find.GetComponent<Lives_System>();
        Gameover_Sfx.ignoreListenerPause = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.Dead)
        {
            Time.timeScale = 0;
            Pausing.CantPause = true;
            if (Once == 1)
            {
                Gameover_Sfx.Play();
                Once++;
            }
            Timer -= Time.unscaledDeltaTime;
            if (Timer <= 0 && Once_Again == 1)
            {
                if (The_Lives.Lives_Status > 0)
                {
                    Lives_System.Lives--;
                    // Load Stage
                    SceneManager.LoadScene(2);
                }
                else
                {
                    // Load Gameover
                    SceneManager.LoadScene(3);
                }
                Once_Again++;
            }
        }
    }
}
