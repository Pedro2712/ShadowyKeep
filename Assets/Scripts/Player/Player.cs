using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Entity entity;
    public Transform RespawnPosition;

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

    private int takenDamage = 0;
    private int playerDefense = 0;
    private int damageDealt = 0;
    private Rigidbody2D rb;
    private Vector3 lastPosition;
    public Animator animator;

    static public bool isDead = false;

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

        StartCoroutine(RegenHealth());
        StartCoroutine(RegenStamina());
    }

    void Update()
    {   
        if(entity.dead){
            respawnPlayer();
            return;
        }

        if(entity.currentHealth <=0 ){
            entity.currentHealth = 0;
            entity.dead = true;
        }

        health.value = entity.currentHealth;
        mana.value = entity.currentMana;
        stamina.value = entity.currentStamina;

    }

    IEnumerator RegenHealth()
    {
        while (true)
        {
            if (regenHPEnabled)
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
            if (regenMPEnabled)
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
        if (collider.CompareTag("Damage") && !isDead)
        {
            Enemy enemy = collider.GetComponentInParent<Enemy>();
            Boss boss = collider.GetComponentInParent<Boss>();
            if (enemy != null){
                ApplyDamage(enemy.entity);
            }
            else {
                ApplyDamage(boss.entity);
            }
            Vector3 direction = (transform.position - collider.transform.position);
            direction.Normalize();
            animator.SetTrigger("damage");
            rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        }
    }

    private void ApplyDamage(Entity enemyEntity)
    {
        takenDamage = manager.CalculateDamage(enemyEntity, enemyEntity.damage);
        playerDefense = manager.CalculateDefense(entity, entity.defense);
        damageDealt = takenDamage - playerDefense;
        if (damageDealt <= 0)
        {
            damageDealt = 0;
        }
        entity.currentHealth -= damageDealt;

        if (entity.currentHealth < 0)
        {
            animator.SetTrigger("isDead");
            isDead = true;
        }
    }

    private void respawnPlayer(){
        Debug.LogFormat("Nada aqui");
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
