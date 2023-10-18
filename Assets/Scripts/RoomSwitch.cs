using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomSwitch : MonoBehaviour
{
    public Canvas canvas;
    private Animator animator;


    public LayerMask playerLayer;
    public float radius;

    private bool onRadios;

    

    void Start()
    {
        canvas.gameObject.SetActive(false);
        //animator = canvas.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Interact();
    }

    private void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

        onRadios = hit != null;

        // Ativa ou desativa o objeto keyspace com base na interação e no diálogo em andamento.
        if (canvas != null)
        {
            canvas.gameObject.SetActive(onRadios);
        }
    } 

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
