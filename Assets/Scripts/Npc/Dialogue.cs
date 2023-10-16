using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public Sprite profile;
    public GameObject keyspace;
    public string[] speechTxt;
    public string actorName;

    public LayerMask playerLayer;
    public float radius;

    private DialogueControl dc;
    private bool onRadios;

    private void Start()
    {
        dc = FindObjectOfType<DialogueControl>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onRadios && !dc.getisRunning())
        {
            dc.Speech(profile, speechTxt, actorName);
        }
    }

    private void FixedUpdate()
    {
        Interact();
    }

    private void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

        onRadios = hit != null;

        // Ativa ou desativa o objeto keyspace com base na interação e no diálogo em andamento.
        if (keyspace != null)
        {
            keyspace.SetActive(onRadios && !dc.getisRunning());
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
