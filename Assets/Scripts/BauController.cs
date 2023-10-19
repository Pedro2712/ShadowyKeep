using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BauController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Light2D luz; // Corrigi a declaração do Light

    public LayerMask playerLayer;
    public float radius;

    private Animator animator;

    private bool onRadios;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Interact();
    }

    public void TurnOffCameraWithDelay()
    {
        // Chama o método TurnOffCamera após um atraso de 3 segundos (tempo em segundos).
        Invoke("TurnOffCamera", 3.0f);
    }

    public void TurnOnCamera()
    {
        virtualCamera.m_Priority = 20;
    }

    public void TurnOffLight()
    {
        luz.enabled = false;
    }

    public void TurnOnLight()
    {
        luz.enabled = true;
    }

    private void TurnOffCamera()
    {
        virtualCamera.m_Priority = 0;
    }

    private void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

        onRadios = hit != null;

        if (onRadios && Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("BauOpen");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
