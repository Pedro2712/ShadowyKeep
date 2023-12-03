using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MobileMovement : MonoBehaviour
{
    //[SerializeField] private float velocidade = 4; // Velocidade que o personagem irá se mover
    public Player player;

    public ManagerSFX managerSFX;
    public float collisionOffset = 0.05f;
    private Vector2 myInput; // Vector2 que armazena os inputs do joystick de movimento
    //private CharacterController characterController; // Referência ao componente de CharacterController do personagem
    public Rigidbody2D rb;
    //private Transform myCamera; // Referência a câmera principal da cena
    public Animator animator; // Referência ao componente Animator do personagem

    public ContactFilter2D movementFilter;
    private Vector2 movement;
    
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private bool canMove = true;
    private bool isWalking = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Nesse código é feito a referenciação
        animator = GetComponent<Animator>(); // Nesse código é feito a referenciação

        isWalking = false;
    }

    void OnMove(InputValue movementValue){
        Vector2 movementVector = movementValue.Get<Vector2>();
        movement = movementVector;
        isWalking = (movement.x != 0 || movement.y != 0);
        if(isWalking){
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            
            managerSFX.walkSound();
            
        }else{
            managerSFX.stopWalkSound();
        }

        animator.SetBool("isWalking", isWalking);
    }

    private bool TryMove(Vector2 direction)
    {
        if (!player.entity.dead) {
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