using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumppower;
    [SerializeField] private float wallSlideSpeed;
    [SerializeField] private float wallJumpX;   // kekuatan loncat horizontal
    [SerializeField] private float wallJumpY;   // kekuatan loncat vertical
    [SerializeField] private float wallStickTime; // durasi "diem" sebelum slide
    
    private Rigidbody2D rb;
    private bool grounded;
    private float horizontalInput; 
    private bool jumpRequested;    
    private SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput > 0.01f)
            sr.flipX = false;
        else if (horizontalInput < -0.01f)
            sr.flipX = true;

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
            jumpRequested = true;
    }


    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocityY);

        if (jumpRequested)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumppower);
            grounded = false;
            jumpRequested = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
            
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = true;
            wallStickTimer = wallStickTime;
            // Detect wall ada di kiri atau kanan player
            wallDirection = transform.position.x < collision.transform.position.x ? 1 : -1;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
        if (collision.gameObject.CompareTag("Wall"))
            isTouchingWall = false;
    }


}