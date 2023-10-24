using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomSwitch : MonoBehaviour
{
    public Canvas canvas;
    public Canvas KeyF;
    public Switch switchScript;


    public LayerMask playerLayer;
    public float radius;

    private bool onRadios;

    

    void Start()
    {
        canvas.gameObject.SetActive(false);
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
            print(onRadios);
            canvas.gameObject.SetActive(onRadios);
            KeyF.gameObject.SetActive(onRadios);
            if (Input.GetKeyDown(KeyCode.F) && onRadios) {
                switchScript.ChooseIcon();
            }
        }
    } 

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
