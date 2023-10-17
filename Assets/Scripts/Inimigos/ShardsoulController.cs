using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardsoulController : MonoBehaviour
{
    public Transform player;
    public float speed = 2.5f;
    public Animator animator;
    private Rigidbody2D rb;
    private bool isWalking = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Get player position
        if (player == null)
        {
            GetTarget();
            if (isWalking)
            {
                isWalking = false;
                Idle();
            }
        }

        // Set direction of animation based on player position
        if (player)
        {
            if (!isWalking)
            {
                Walk();
                isWalking = true;
            }            
        }
    }

    private void FixedUpdate()
    {
        // Move towards player
        Vector2 direction = player.position - transform.position;
        rb.velocity = direction.normalized * speed;

    }

    private void GetTarget() 
    {
        // Get player position
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // // Destroy on collision with player
        // if (collision.gameObject.tag == "Player")
        // {
           
        // }
    }

    public void Walk()
    {
        // Walk animation
        animator.SetTrigger("Walk");
    }

    public void Idle()
    {
        // Walk animation
        animator.SetTrigger("Idle");
    }
}
