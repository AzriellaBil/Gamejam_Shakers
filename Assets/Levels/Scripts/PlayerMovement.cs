using System.Net.Sockets;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed ;
    [SerializeField] private float jumppower ;
    private Rigidbody2D rb;
    private bool grounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    

    void Update()
    {
        float horizontalinput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(horizontalinput * speed, rb.linearVelocityY);


        if(horizontalinput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalinput < -0.01f) 
            transform.localScale = new Vector3(-1,1,1);


        if (Input.GetKey(KeyCode.Space) && grounded)
           Jump();
    }


    private void Jump()
    {
       rb.linearVelocity = new Vector2(rb.linearVelocityX, jumppower);
       grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
            grounded = true;
    }
}