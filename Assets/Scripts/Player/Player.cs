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
    public ManagerSFX managerSFX;

    public GameObject LevelUp;

    void Start()
    {
        if (manager == null)
        {
            Debug.LogError("Você precisa anexar o Game Manager aqui no player.");
            return;
        }

        rb = GetComponent <Rigidbody2D>();
        lastPosition = transform.position;

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

        manager.UpdateHealth(GlobalVariables.instance.life);
        manager.UpdateStamina(GlobalVariables.instance.stamina);
        manager.UpdateStrength(GlobalVariables.instance.strength);
        manager.UpdateDefense(GlobalVariables.instance.defense);
        manager.UpdateSpeed(GlobalVariables.instance.speed);

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

            if (enemy != null){
                ApplyDamage(enemy.entity);
            }
            else if (boss != null) {
                ApplyDamage(boss.entity);
            }
            else if (fireball != null){
                ApplyDamage(BossFireball.entity);
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
            GlobalVariables.instance.roomsVisited = 0;
            StartCoroutine(DelayedGameOver());
        }
    }

    private IEnumerator DelayedGameOver(){
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(3);
    }

    
    // Comentado para uso mobile : Função associada ao botão no arquivo MobileAttack
    // void OnSimpleSwordAttack(){
    //     if (entity.currentStamina >= entity.staminaCost)
    //     {
    //         entity.currentStamina -= entity.staminaCost;
    //         animator.SetTrigger("attack");
    //         managerSFX.swordSound();
    //     }
    // }
}
