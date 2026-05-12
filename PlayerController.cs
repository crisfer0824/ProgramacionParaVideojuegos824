using UnityEngine;

// Controls player movement, jumping, animations, audio, and game over logic.
public class PlayerController : MonoBehaviour
{
    // Horizontal movement speed.
    public float speed = 5f;

    // Upward force applied when the player jumps.
    public float jumpForce = 7f;

    // Reference to the GameManager to control the game state.
    public GameManager gameManager;

    // Handles character animations.
    private Animator animator;

    // Controls 2D physics and movement.
    private Rigidbody2D rb;

    // Indicates whether the player is touching the ground.
    private bool isGrounded;

    // Component used to play sound effects.
    private AudioSource audioSource;

    // Audio clip played when the player jumps.
    public AudioClip jumpSound;

    // Initializes required components when the game starts.
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Called once per frame to process movement, jumping, and animation updates.
    void Update()
    {
        Move();
        Jump();

        // Updates animation parameters based on current movement and ground state.
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        animator.SetBool("isGrounded", isGrounded);
    }

    // Handles horizontal movement and flips the character sprite.
    void Move()
    {
        // Reads keyboard input for left and right movement.
        float moveInput = Input.GetAxis("Horizontal");

        // Applies horizontal velocity while preserving vertical velocity.
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        // Flips the character depending on movement direction.
        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    // Allows the player to jump only when standing on the ground.
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // Applies vertical force to perform the jump.
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            // Plays the jump sound effect.
            audioSource.PlayOneShot(jumpSound);
        }
    }

    // Detects collisions with ground and enemies.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Marks the player as grounded when touching the floor.
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        // Ends the game if the player touches an enemy.
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameOver();
        }
    }

    // Detects when the player leaves the ground.
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Marks the player as airborne.
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    // Handles the game over event.
    void GameOver()
    {
        Debug.Log("GAME OVER");
        gameManager.EndGame();
    }
}