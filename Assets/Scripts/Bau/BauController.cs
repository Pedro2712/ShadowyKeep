using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class BauController : MonoBehaviour
{
    [System.Serializable]
    public class SpriteOption
    {
        public string name;
        public Sprite sprite;
    }
    public CinemachineVirtualCamera virtualCamera;
    public Light2D luz; // Corrigi a declara��o do Light
    public ParticleSystem particle;

    public LayerMask playerLayer;
    public float radius;
    public SpriteOption[] sprites;

    public Image icon;

    private Animator animator;

    private bool onRadios;

    void Start()
    {
        animator = GetComponent<Animator>();
        icon.sprite = sprites[GlobalVariables.instance.finalChoose].sprite;
    }

    private void FixedUpdate()
    {
        Interact();
    }

    public void TurnOffCameraWithDelay()
    {
        // Chama o m�todo TurnOffCamera ap�s um atraso de 3 segundos (tempo em segundos).
        Invoke("TurnOffCamera", 3.0f);
    }

    public void TurnOnCamera()
    {
        virtualCamera.m_Priority = 20;
    }
    private void TurnOffCamera()
    {
        virtualCamera.m_Priority = 0;
    }

    public void TurnOffLight()
    {
        luz.enabled = false;
        particle.Stop();
    }

    public void TurnOnLight()
    {
        luz.enabled = true;
        particle.Play();
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
