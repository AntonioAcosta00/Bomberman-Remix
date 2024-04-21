using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Input for this example. Replace with your enemy's movement logic.
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Update the animator parameters
        UpdateAnimation();
    }

    void FixedUpdate()
    {
        // Move the enemy
        MoveCharacter(movement);
    }

    private void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.fixedDeltaTime));
    }

    private void UpdateAnimation()
    {
        if (movement != Vector2.zero)
        {
            // Set animator parameters
            animator.SetFloat("moveX", movement.x);
            animator.SetFloat("moveY", movement.y);
        }
    }
}
