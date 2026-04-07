using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumppower;
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
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Location"))
            print(collision.gameObject.name) ;  
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) 
            grounded = true;
    }


}