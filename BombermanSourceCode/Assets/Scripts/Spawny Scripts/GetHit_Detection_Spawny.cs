using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHit_Detection_Spawny : MonoBehaviour
{
    public bool Deleting;
    public Animator Spawny_An;
    public ExplosionDamage GotHit;
    public GameObject Enemy_Spawn;
    public Transform Spawner_Parent;
    private int Once = 1;
    private AudioSource Reverse;
    void Start()
    {
        Spawner_Parent = GameObject.FindWithTag("Spawny_Spawner").GetComponent<Transform>();
        Reverse = GameObject.FindWithTag("Reverse").GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        // Once the explosion collision is detected, the block will be destroyed and has a chance to leave behind an item (W.I.P)
        if (GotHit.Hit && Once == 1)
        {
            Reverse.PlayDelayed(0.2f);
            Spawny_An.Play("S_Destroyed");
            (Instantiate(Enemy_Spawn, gameObject.transform.position, gameObject.transform.rotation) as GameObject).transform.parent = Spawner_Parent.transform;
            Once++;
        }
        if (Deleting)
        {
            Destroy(gameObject);
            Deleting = false;
        }

    }
}
