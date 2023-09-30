using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float collisionOffset = 0.05f;
    public Rigidbody2D rb;
    public Animator animator;
    public ContactFilter2D movementFilter;
    private Vector2 movement;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movement = movementVector;
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction == Vector2.zero)
            return false;

        float moveDistance = moveSpeed * Time.fixedDeltaTime + collisionOffset;
        int count = rb.Cast(direction, movementFilter, castCollisions, moveDistance);

        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
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
