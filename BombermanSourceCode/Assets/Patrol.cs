using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    public Transform[] moveSpots;

    public float startWaitTime;
    private float waitTime;
    private int randomSpot;

    void Start()
    {
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    void Update()
    {
        MoveTowardsNextSpot();
    }

    void MoveTowardsNextSpot()
    {
        Vector2 targetPosition = moveSpots[randomSpot].position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetPosition - (Vector2)transform.position, Vector2.Distance(transform.position, targetPosition));

        if (hit.collider == null)
        {
            // No obstacle, move towards the target position
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Check if the object has reached the target position
            if (Vector2.Distance(transform.position, targetPosition) < 0.2f)
            {
                if (waitTime <= 0)
                {
                    // Select a new random spot
                    randomSpot = Random.Range(0, moveSpots.Length);
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
        // If there's an obstacle, you can handle it here (e.g., stop, change direction, etc.)
        else
        {
         //   Debug.Log("Obstacle detected!");
            // Handle the obstacle (e.g., change direction or stop)
        }
    }
}