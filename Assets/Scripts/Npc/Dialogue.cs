using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public Sprite profile;
    public string[] speechTxt;
    public string actorName;

    public LayerMask playerLayer;
    public float radios;

    private DialogueControl dc;
    bool onRadios;

    private void Start()
    {
        dc = FindObjectOfType<DialogueControl>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onRadios) {
            dc.Speech(profile, speechTxt, actorName);
        }
    }

    private void FixedUpdate()
    {
        Interact();
    }

    public void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radios, playerLayer);
        
        if (hit != null)
        {
            onRadios= true;
        } else {
            onRadios= false;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radios);
    }
}
