using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using static GlobalVariables;

public class Player : MonoBehaviour
{
    public Entity entity = new();

    [Header("Player Regen System")]
    public bool regenHPEnabled = true;
    public float regenHPTime = 1f;
    public int regenHPValue = 1;
    public bool regenMPEnabled = true;
    public float regenMPTime = 1f;
    public int regenMPValue = 1;

    [Header("Game Manager")]
    public GameManagerBattle manager;

    [Header("Player UI")]
    public Slider health;
    public Slider mana;
    public Slider stamina;

    [Header("Knockback")]
    public float knockbackForce = 25f;

    private int receivedDamage = 0;
    private int playerDefense = 0;
    private int totalDamage = 0;
    private Rigidbody2D rb;
    private Vector3 lastPosition;
    public Animator animator;
    public Boss BossFireball;

    public GameObject LevelUp;

    [Header("Music and Sounds")]
    public ManagerSFX managerSFX;

    [Header("Ads")]
    private ActivateAds activateAds;

    void Start()
    {
        if (manager == null)
        {
            Debug.LogError("Você precisa anexar o Game Manager aqui no player.");
            return;
        }

        rb = GetComponent <Rigidbody2D>();
        lastPosition = transform.position;

        manager.UpdateHealth(GlobalVariables.instance.boughtHealth, GlobalVariables.instance.tempMaxHealth);
        manager.UpdateStamina(GlobalVariables.instance.boughtStamina, GlobalVariables.instance.tempMaxStamina);
        manager.UpdateStrength(GlobalVariables.instance.boughtStrength, GlobalVariables.instance.tempStrength);
        manager.UpdateDefense(GlobalVariables.instance.boughtDefense, GlobalVariables.instance.tempDefense);
        manager.UpdateSpeed(GlobalVariables.instance.boughtSpeed, GlobalVariables.instance.tempSpeed);

        entity.maxHealth = manager.CalculateHealth(entity);
        entity.maxStamina = manager.CalculateStamina(entity);

        entity.currentHealth = entity.maxHealth;
        entity.currentStamina = entity.maxStamina;
        entity.currentMana = entity.maxMana;

        health.maxValue = entity.maxHealth;
        health.value = health.maxValue;

        mana.maxValue = entity.maxMana;
        mana.value = mana.maxValue;

        stamina.maxValue = entity.maxStamina;
        stamina.value = stamina.maxValue;

        entity.level = GlobalVariables.instance.lastPlayerLevel;
        entity.experienceToNextLevel = GlobalVariables.instance.lastExperienceToNextLevel;

        StartCoroutine(RegenHealth());
        StartCoroutine(RegenStamina());
    }

    void Update()
    {

        health.value = entity.currentHealth;
        mana.value = entity.currentMana;
        stamina.value = entity.currentStamina;
        

        if (entity.experience >= entity.experienceToNextLevel)
        {
            entity.level += 1;
            GlobalVariables.instance.lastPlayerLevel = entity.level;
            entity.experience -= entity.experienceToNextLevel;
            entity.experienceToNextLevel += 100;
            GlobalVariables.instance.lastExperienceToNextLevel = entity.experienceToNextLevel;

            managerSFX.LevelUp();
            Instantiate(LevelUp, transform.position, Quaternion.identity, transform);
        }
    }

    IEnumerator RegenHealth()
    {
        while (true)
        {
            if (regenHPEnabled && !entity.dead)
            {
                if (entity.currentHealth < entity.maxHealth)
                {
                    entity.currentHealth += regenHPValue;
                    yield return new WaitForSeconds(regenHPTime);
                }
                else
                {
                    yield return null;
                }
            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator RegenStamina()
    {
        while (true)
        {
            if (regenMPEnabled && !entity.dead)
            {
                if (entity.currentStamina < entity.maxStamina)
                {
                    entity.currentStamina += regenMPValue;
                    yield return new WaitForSeconds(regenMPTime);
                }
                else
                {
                    yield return null;
                }
            }
            else
            {
                yield return null;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Damage") && !entity.dead)
        {

            Enemy enemy = collider.GetComponentInParent<Enemy>();
            Boss boss = collider.GetComponentInParent<Boss>();
            FireballController fireball = collider.GetComponentInParent<FireballController>();
            AttackHit attackHit = collider.GetComponentInParent<AttackHit>();
            if (enemy != null){
                ApplyDamage(enemy.entity);
            }
            else if (boss != null) {
                ApplyDamage(boss.entity);
            }
            else if (fireball != null){
                ApplyDamage(BossFireball.entity);
            }
            else if (attackHit != null) {
                EnemyRange enemyRange = attackHit.GetComponentInParent<EnemyRange>();

                if (enemyRange != null){
                    ApplyDamage(enemyRange.entity);
                    if (attackHit != null){
                        Destroy(attackHit.gameObject);
                    }
                }
            }

            Vector3 direction = (transform.position - collider.transform.position);
            direction.Normalize();
            animator.SetTrigger("damage");
            rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        }
    }

    private void ApplyDamage(Entity enemyEntity)
    {
        receivedDamage = manager.CalculateDamage(enemyEntity, enemyEntity.damage);
        playerDefense = manager.CalculateDefense(entity, entity.defense);
        totalDamage = receivedDamage - playerDefense;
        if (totalDamage <= 0)
        {
            totalDamage = 0;
        }
        entity.currentHealth -= totalDamage;

        if (entity.currentHealth < 0)
        {
            animator.SetTrigger("isDead");
            entity.dead = true;

            // Activate Ads Script ativa a talea de GameOver ou de Ads
            activateAds = FindObjectOfType<ActivateAds>();
            activateAds.loadAds();
            
            if (entity.dead)
            {
                GlobalVariables.instance.roomsVisited = 0;
                GlobalVariables.instance.tempSpeed = 0;
                GlobalVariables.instance.tempStrength = 0;
                GlobalVariables.instance.tempDefense = 0;
                GlobalVariables.instance.tempMaxHealth = 0;
                GlobalVariables.instance.tempMaxStamina = 0;
            }
        }
    }

}
