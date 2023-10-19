using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardsoulController : MonoBehaviour
{
    public float speed = 2.5f;
    public Animator animator;
    public LayerMask playerLayer;
    public float radius = 7f;
    private Rigidbody2D rb;
    private bool walk = false;
    // private bool attack = false;
    // private bool takeDamage = false;
    // private bool die = false;
    private Transform player;
    private float distance;
    private Collider2D hit;
    private bool onRadios;
    private Vector2 direction;
    private bool facingRight = true;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
        GetTarget();
        if (player == null)
        {
            walk = false;
            animator.SetBool("Walk", walk);
        }
        if (player != null && !walk)
        {
            walk = true;
            animator.SetBool("Walk", walk);
        }
        if (player != null && walk)
        {
            // Flip sprite
            if (direction.x < 0 && facingRight)
            {
                Flip();
            }
            else if (direction.x > 0 && !facingRight)
            {
                Flip();
            }
        }
    }

    private void FixedUpdate()
    {
        if (player != null && walk)
        {
            // Move towards player
            direction = player.position - transform.position;
            rb.velocity = direction.normalized * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void GetTarget() 
    {
         
        hit = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

        onRadios = hit != null;

        if (onRadios)
        {
            player = hit.transform;
        }
        else
        {
            player = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     // // Destroy on collision with player
    //     // if (collision.gameObject.tag == "Player")
    //     // {
           
    //     // }
    // }
}
