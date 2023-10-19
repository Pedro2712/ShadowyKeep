using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{   
    public Player player;

    public float moveSpeed = 5f;
    public float collisionOffset = 0.05f;
    public Rigidbody2D rb;
    public Animator animator;
    public ContactFilter2D movementFilter;
    private Vector2 movement;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private bool canMove = true;

    private bool isWalking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        
        animator = GetComponent<Animator>();

        isWalking = false;
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movement = movementVector;

        isWalking = (movement.x != 0 || movement.y != 0);
        
        if(isWalking){
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
        animator.SetBool("isWalking", isWalking);
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction == Vector2.zero)
            return false;

        float moveDistance = player.entity.speed * Time.fixedDeltaTime + collisionOffset;
        int count = rb.Cast(direction, movementFilter, castCollisions, moveDistance);

        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * player.entity.speed * Time.fixedDeltaTime);
            return true;
        }

        return false;
    }

    void FixedUpdate()
    {
        if (canMove && movement != Vector2.zero)
        {
            if (!TryMove(movement))
            {
                if (!TryMove(new Vector2(movement.x, 0)))
                {
                    TryMove(new Vector2(0, movement.y));
                }
            }
        }
    }
}
