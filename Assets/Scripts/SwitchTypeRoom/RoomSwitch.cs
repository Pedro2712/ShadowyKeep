using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomSwitch : MonoBehaviour
{
    public Canvas canvas;


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

        // Activate canvas if player is in range
        if (canvas != null)
        {
            canvas.gameObject.SetActive(onRadios);
            // if (Input.GetKeyDown(KeyCode.F) && onRadios) {
                
            // }
        }
    } 

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
