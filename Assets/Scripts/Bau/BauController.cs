using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using static GlobalVariables;

public class BauController : MonoBehaviour
{
    public List<string> nameSprites = new List<string>();
    public List<Sprite> sprites = new List<Sprite>();

    public CinemachineVirtualCamera virtualCamera;
    public Light2D luz; // Corrigi a declara  o do Light
    public ParticleSystem particle;
    public Button button;

    public LayerMask playerLayer;
    public float radius;
    public Player player;
    public bool isOpen = false;

    public Image icon;
    private Animator animator;
    private bool onRadios;
    private string buff;
    private float buffValue;

    [SerializeField] private GameObject floatingTextPrefab;

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

    private bool isAnimated = true;
    public Canvas KeyF;
    private bool isClicked = false;
    private Dictionary<string, Sprite> sortSprites = new Dictionary<string, Sprite>();

    void Start()
    {
        animator = GetComponent<Animator>();
        MontaDict();
    }

    public void MontaDict() {
        for (int i = 0; i < 5; i++){
            sortSprites.Add(nameSprites[i], sprites[i]);
        }
    }

    public void ButtonClicado () {
        isClicked = true;
    }

    private void Update()
    {
        if (isOpen)
        {
            KeyF.gameObject.SetActive(false);
            return;
        }
        Interact();
    }

    public void TurnOffCameraWithDelay()
    {
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

        KeyF.gameObject.SetActive(onRadios && !isOpen);
        if (onRadios && isClicked && !isAnimated)
        {
            KeyF.gameObject.SetActive(false);
            icon.sprite = sortSprites[GlobalVariables.instance.finalChoose];
            buff = GlobalVariables.instance.finalChoose;
            StartCoroutine(OpenChest());
        }
        else {
            isClicked = false;
        }
    }

    public void ChangeIsAnimated() {
        isAnimated = false;
    }

    IEnumerator OpenChest()
    {
        animator.SetTrigger("BauOpen");

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("BauIdleOpen"));

        switch (buff)
        {
            case "Coins":
                buffValue = CoinsBuff();
                player.entity.coins += (int) buffValue;
                GlobalVariables.instance.coins = player.entity.coins;
                break;
            case "Speed":
                buffValue = SpeedBuff();
                player.entity.speed += buffValue;
                break;
            case "Health":
                buffValue = HealthBuff();
                player.entity.maxHealth += (int) buffValue;
                break;
            case "Strength":
                buffValue = StrengthBuff();
                player.entity.strength += (int) buffValue;
                break;
            case "Defense":
                buffValue = DefenseBuff();
                player.entity.defense += (int) buffValue;
                break;
            // case "CooldownReduce":
            //     player.entity.cooldown -= 0.1f;
            //     break;
        }
        showBuff(buff, buffValue.ToString("F1"));

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

    private void showBuff(string buff, string value) {

        if (floatingTextPrefab) {
            GameObject prefab = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = "+ " + value + " " + buff;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}