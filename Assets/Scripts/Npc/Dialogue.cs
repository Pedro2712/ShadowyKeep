using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    public Sprite profile;
    public bool store;
    public string[] speechTxt;
    public string actorName;

    public LayerMask playerLayer;
    public float radius;

    private DialogueControl dc;
    private bool onRadios;
    private bool inside = false;

    private void Start()
    {
        dc = FindObjectOfType<DialogueControl>();
    }

    private void Update()
    {
        if ( onRadios && !dc.getisRunning() && !inside)
        {
            dc.isStore(store);
            dc.Speech(profile, speechTxt, actorName);
            inside = true;
        }  
    }

    private void FixedUpdate()
    {
        Interact();
    }

    private void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

        onRadios = (hit != null);

        if (!onRadios && inside) {
            inside = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
