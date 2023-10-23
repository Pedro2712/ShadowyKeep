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
    public Player player;
    public bool isOpen = false;

    public Image icon;
    private Animator animator;
    private bool onRadios;
    private string buff;

    private int BASE_COINS_BUFF = 50;
    private int BASE_HEALTH_BUFF = 10;
    private float BASE_SPEED_BUFF = 0.1f;
    private int BASE_STRENGTH_BUFF = 1;
    private int BASE_DEFENSE_BUFF = 1;

    private int MAX_COINS_BUFF = 100;
    private int MAX_HEALTH_BUFF = 100;
    private float MAX_SPEED_BUFF = 5f;
    private int MAX_STRENGTH_BUFF = 10;
    private int MAX_DEFENSE_BUFF = 10;

    void Start()
    {
        animator = GetComponent<Animator>();
        icon.sprite = sprites[GlobalVariables.instance.finalChoose].sprite;
        buff = sprites[GlobalVariables.instance.finalChoose].name;
    }

    private void Update()
    {
        if (isOpen)
        {
            return;
        }
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
            StartCoroutine(OpenChest());
        }
    }

    IEnumerator OpenChest()
    {
        animator.SetTrigger("BauOpen");

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("BauIdleOpen"));

        switch (buff)
        {
            case "Coins":
                player.entity.coins += CoinsBuff();
                break;
            case "Speed":
                player.entity.speed += SpeedBuff();
                break;
            case "Health":
                player.entity.maxHealth += HealthBuff();
                break;
            case "Strength":
                player.entity.strength += StrengthBuff();
                break;
            case "Defense":
                player.entity.defense += DefenseBuff();
                break;
            // case "CooldownReduce":
            //     player.entity.cooldown -= 0.1f;
            //     break;
        }

        isOpen = true;
    }

    private int CoinsBuff()
    {
        int coins = MAX_COINS_BUFF / ( Random.Range(1, 5) + (int) ( ( MAX_COINS_BUFF / 2 ) / ( player.entity.level * BASE_COINS_BUFF ) ) );
        return coins;
    }

    private int HealthBuff()
    {
        int health = MAX_HEALTH_BUFF / ( Random.Range(1, 5) + (int) ( (MAX_HEALTH_BUFF / 2) / ( player.entity.level * BASE_HEALTH_BUFF) ) );
        
        return health;
    }

    private float SpeedBuff()
    {
        float speed = MAX_SPEED_BUFF / ( Random.Range(1, 5) + ( ( MAX_SPEED_BUFF / 2 ) / ( player.entity.level * BASE_SPEED_BUFF ) ) );
        return speed;
    }

    private int StrengthBuff()
    {
        int strength = MAX_STRENGTH_BUFF / ( Random.Range(1, 5) + (int) ( ( MAX_STRENGTH_BUFF / 2 ) / ( player.entity.level * BASE_STRENGTH_BUFF ) ) );
        return strength;
    }

    private int DefenseBuff()
    {
        int defense = MAX_DEFENSE_BUFF / ( Random.Range(1, 5) + (int) ( ( MAX_DEFENSE_BUFF / 2 ) / ( player.entity.level * BASE_DEFENSE_BUFF ) ) );
        return defense;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
