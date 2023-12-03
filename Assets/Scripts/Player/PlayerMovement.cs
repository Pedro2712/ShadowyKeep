using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    public Player player;
    public ManagerSFX managerSFX;
    public float collisionOffset = 0.05f;
    public Rigidbody2D rb;
    public Animator animator;
    public ContactFilter2D movementFilter;
    private Vector2 movement;
    private bool canMove = true;
    private bool isWalking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        isWalking = false;
    }

    static double CalcularExpressao(double x)
    {
        double numerador = Math.Exp(2 * x) - 1;
        double denominador = Math.Exp(2 * x) + 1;

        // Verifica se o denominador n�o � zero antes de fazer a divis�o
        if (denominador != 0)
        {
            return numerador / denominador;
        }
        else
        {
            // Tratar caso o denominador seja zero (evitar divis�o por zero)
            Console.WriteLine("Erro: Divis�o por zero.");
            return double.NaN; // Retorna NaN (Not a Number) para indicar um resultado indefinido
        }
    }

    private Vector2 TravaVelocidade(Vector2 movement)
    {
        double xResult = CalcularExpressao(movement.x);
        double yResult = CalcularExpressao(movement.y);

        return new Vector2 { x = (float) xResult, y = (float) yResult };
    }

    public void MovePlayer(Vector2 movementValue)
    {
        movement = TravaVelocidade(movementValue);

        isWalking = (movement.x != 0 || movement.y != 0);

        if (isWalking)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);

            managerSFX.walkSound();
        }
        else
        {
            managerSFX.stopWalkSound();
        }

        animator.SetBool("isWalking", isWalking);
    }


    private bool TryMove(Vector2 direction)
    {
        if (!player.entity.dead) {
            rb.MovePosition(rb.position + direction * player.manager.CalculateSpeed(player.entity) * Time.fixedDeltaTime);
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