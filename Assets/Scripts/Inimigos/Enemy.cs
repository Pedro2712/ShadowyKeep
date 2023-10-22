using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    public Entity entity = new Entity();
    public GameManagerBattle manager;

    public LayerMask playerLayer;
    public GameObject attackHitBox;
    public float detectionRadius = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private Collider2D targetOnRange;
    private Transform targetTransform;
    private Vector2 direction;
    private Vector2 newPosition;
    private Vector2 targetPositionTilted;
    private bool onRange;
    private bool facingRight = true;
    private float cooldownTimer = 0f;


    public float attackRange = 0.7f;


    [Header("Knockback")]
    public float knockbackForce = 25f;

    private int receivedDamage = 0;
    private int enemyDefense = 0;
    private int totalDamage = 0;

    public GameObject enemy;

    [Header("Player UI")]
    public Slider health;
    public Image exclamation;

    private bool find = false;

    [SerializeField] private GameObject floatingTextPrefab;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        // Use FindObjectOfType para encontrar o GameManagerBattle
        manager = FindObjectOfType<GameManagerBattle>();

        entity.maxHealth = manager.CalculateHealth(entity);

        entity.currentHealth = entity.maxHealth;

        health.maxValue = entity.maxHealth;
        health.value = health.maxValue;

        exclamation.enabled = false;
    }

    private void Update()
    {
        if (entity.dead)
        {
            return;
        }

        if (entity.currentHealth <= 0)
        {
            entity.currentHealth = 0;
            Die();
        }

        if (entity.experience <= 0)
        {
            entity.experience = manager.CalculateEnemyExperience(entity);
        }

        if (!entity.inCombat)
        {
            GetTarget();
        }

        if (targetTransform != null)
        {
            direction = targetTransform.position - transform.position;
            // Flip sprite
            if (direction.x < 0 && facingRight)
            {
                Flip();
            }
            else if (direction.x > 0 && !facingRight)
            {
                Flip();
            }
        }

        if (entity.target != null && entity.inCombat)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                cooldownTimer = entity.cooldown;
                Attack();
            }
        }
    }

    private void FixedUpdate()
    {
        if (entity.dead)
        {
            return;
        }

        if (targetTransform != null && !entity.dead) {
            float distance = Vector2.Distance(transform.position, targetTransform.position);
            Vector2 direction = targetTransform.position - transform.position;
            direction.Normalize();

            if (distance > attackRange)
            {
                entity.inCombat = false;
                entity.target = null;

                transform.position = Vector2.MoveTowards(this.transform.position, targetTransform.position, entity.speed * Time.deltaTime);
            }
            else
            {
                entity.inCombat = true;
                entity.target = targetTransform.gameObject;

                animator.SetBool("Walk", false);
                Attack();
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("weapon"))
        {
            ApplyDamage(collider.GetComponentInParent<Player>().entity);
            Vector3 direction = (transform.position - collider.transform.position);
            direction.Normalize();
            animator.SetTrigger("Damage");
            rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        }
    }

    private void ApplyDamage(Entity enemyEntity)
    {
        receivedDamage = manager.CalculateDamage(enemyEntity, enemyEntity.damage);
        enemyDefense = manager.CalculateDefense(entity, entity.defense);
        totalDamage = receivedDamage - enemyDefense;
        if (totalDamage <= 0)
        {
            totalDamage = 0;
        }
        entity.currentHealth -= totalDamage;

        showDamage(damageDealt.ToString());

        health.value = entity.currentHealth;
    }

    private void showDamage(string damage) {

        if (floatingTextPrefab) {
            GameObject prefab = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = damage;
        }
    }

    private void GetTarget()
    {
        if (entity.dead)
        {
            return;
        }

        targetOnRange = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);

        onRange = targetOnRange != null;

        if (onRange)
        {
            animator.SetBool("Walk", true);
            targetTransform = targetOnRange.transform;

            if (!find) {
                exclamation.enabled = true;
                StartCoroutine(DelayedExclamation());
                find= true;
            }
        }
        else
        {
            animator.SetBool("Walk", false);
            targetTransform = null;
            find= false;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
        exclamation.transform.Rotate(0f, 180f, 0f);
    }

    private void Attack()
    {
        if (entity.target != null && !entity.target.GetComponent<Player>().entity.dead)
        {
            rb.velocity = Vector2.zero;
            animator.SetTrigger("Attack");
        }
    }

    private void EnableHitbox()
    {
        attackHitBox.SetActive(true);
    }

    private void DisableHitbox()
    {
        attackHitBox.SetActive(false);
    }

    private void Die()
    {
        entity.dead = true;
        entity.inCombat = false;
        entity.target = null;

        animator.SetBool("Dead", true);
        Destroy(enemy, 3f);
    }

    private IEnumerator DelayedExclamation()
    {
        // Espera por 3 segundos
        yield return new WaitForSeconds(1f);

        // Chama outra fun��o ap�s a espera
        exclamation.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}