using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumppower;
    [SerializeField] private float wallSlideSpeed = 1.5f;
    [SerializeField] private float wallJumpX = 15f;   // kekuatan loncat horizontal
    [SerializeField] private float wallJumpY = 15f;   // kekuatan loncat vertical
    [SerializeField] private float wallStickTime = 3f; // durasi "diem" sebelum slide
    [SerializeField] public float dashPower = 30f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCooldown = 1f;

    private int facingDirection = 1;
    
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    
    private bool isGrounded;
    private bool isTouchingWall;
    private int wallDirection;      // -1 = wall di kiri, 1 = wall di kanan
    
    private bool jumpRequested;
    private float horizontalInput;
    
    private float wallStickTimer;
    private bool isWallSticking;

    private bool canDash;
    private bool isDashing;
    [SerializeField] private TrailRenderer tr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        canDash = true;
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");
        
        if (horizontalInput > 0.01f) {
            sr.flipX = false;
            facingDirection = 1;
        }
        else if (horizontalInput < -0.01f) {
            sr.flipX = true;
            facingDirection = -1;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
                jumpRequested = true;
            else if (isTouchingWall)
                jumpRequested = true; // wall jump!
        }
        
        // Hitung timer stick
        // Selama player push ke arah wall, timer direset terus
        bool pushingIntoWall = (wallDirection == 1 && horizontalInput > 0.01f) ||
                               (wallDirection == -1 && horizontalInput < -0.01f);
                               
        if (isTouchingWall && !isGrounded && pushingIntoWall)
        {
            wallStickTimer -= Time.deltaTime;
            isWallSticking = wallStickTimer > 0; // masih stick kalau timer belum habis
        }
        else
        {
            wallStickTimer = wallStickTime; // reset timer kalau lepas dari wall
            isWallSticking = false;
        }


        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        if (isTouchingWall && !isGrounded)
        {
            if (isWallSticking)
            {
                // Fase diem: freeze vertical velocity
                rb.linearVelocity = new Vector2(0f, 0f);
            }
            else
            {
                // Fase slide: turun pelan, makin lama makin cepat
                float yVel = Mathf.Max(rb.linearVelocityY, -wallSlideSpeed);
                rb.linearVelocity = new Vector2(0f, yVel);
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocityY);
        }

        if (jumpRequested)
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, jumppower);
            }
            else if (isTouchingWall)
            {
                // Loncat ke arah BERLAWANAN dari wall
                rb.linearVelocity = new Vector2(-wallDirection * wallJumpX, wallJumpY);
                isTouchingWall = false;
            }
            
            isGrounded = false;
            jumpRequested = false;
        }

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Location"))
            print(collision.gameObject.name) ;  
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

    private IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(facingDirection * dashPower, 0f);
        if (tr != null)
        {
            tr.emitting = true;
        }
        yield return new WaitForSeconds(dashTime);
        if (tr != null)
        {
            tr.emitting = false;
        }
        rb.gravityScale = originalGravity;
        rb.linearVelocity = new Vector2(0f, rb.linearVelocityY);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}