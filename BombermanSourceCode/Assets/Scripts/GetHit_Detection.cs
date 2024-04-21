using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHit_Detection : MonoBehaviour
{
    public bool Delete;
    public static int Exit = 1;
    private bool Checking = true;
    public int SpawnNum_Item = 50;
    public int SpawnNum_Door = 25;
    public ExplosionDamage GotHit;
    public GameObject ExitDoor;
    public GameObject[] PowerUp_Spawn;
    public bool Repeat_Num;
    public int random_number;
    public GameObject Spawner_Parent;
    private Animator Block_Anim;

    void Start()
    {
        Spawner_Parent = GameObject.FindWithTag("PowerUp Spawning");
        Block_Anim = gameObject.GetComponent<Animator>();
        random_number = Random.Range(0, (SpawnNum_Item + 1));
        if (random_number == SpawnNum_Door)
        {
            if (Exit == 1)
            {
                Exit = 0;
                gameObject.name = "Breakable Block: Exit Door";
            }
            else
            {
                Repeat_Num = true;
                int Rand_1 = Random.Range(0, SpawnNum_Door);
                int Rand_2 = Random.Range((SpawnNum_Door + 1), (SpawnNum_Item + 1));
                random_number = Random_2(Rand_1, Rand_2);
            }
        }

        if (random_number == SpawnNum_Item)
        {
            gameObject.name = "Breakable Block: Item";
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Once the explosion collision is detected, the block will be destroyed and has a chance to leave behind an item
        if (GotHit.Hit)
        {
            Block_Anim.enabled = true;
            GotHit.Hit = false;
        }
        if (Delete)
        {
            if (Checking)
            {
                if (random_number == SpawnNum_Item)
                {
                    ItemChance();
                }
                else if (random_number == SpawnNum_Door)
                {
                    (Instantiate(ExitDoor, gameObject.transform.position, gameObject.transform.rotation) as GameObject).transform.parent = Spawner_Parent.transform;
                }
                Checking = false;
            }
            Destroy(gameObject);
        }
    }

    // Spawns in a random powerup
    public void ItemChance()
    {
        int random_spawn = Random.Range(0, PowerUp_Spawn.Length);
        (Instantiate(PowerUp_Spawn[random_spawn], gameObject.transform.position, gameObject.transform.rotation) as GameObject).transform.parent = Spawner_Parent.transform;
    }

    public int Random_2(int R1, int R2)
    {
        int[] Sets = { R1, R2 };
        int Rnd_Var = Random.Range(0, Sets.Length);
        return Sets[Rnd_Var];
    }
}