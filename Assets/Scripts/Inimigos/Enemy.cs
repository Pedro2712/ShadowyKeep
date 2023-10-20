using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [Header("Controller")]
    public Entity entity = new Entity();
    public GameManagerBattle manager;

    public LayerMask playerLayer;
    public float detectionRadius = 5f;

    // [Header("Enemy UI")]
    // public Slider health;

    // [Header("Experience Reward")]
    // public int rewardExperience = 10;
    // public int lootGoldMin = 0;
    // public int lootGoldMax = 10;
    
    private Rigidbody2D rb;
    private Animator animator;
    
    private Collider2D targetOnRange;
    private Transform targetTransform;
    private Vector2 direction;
    private bool onRange;
    private bool facingRight = true;
    private int damage = 0;
    private int targetDefense = 0;
    private int damageDealt = 0;
    private float cooldownTimer = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        manager = GameObject.Find("GameManagerBattle").GetComponent<GameManagerBattle>();

        entity.maxHealth = manager.CalculateHealth(entity);

        entity.currentHealth = entity.maxHealth;

        cooldownTimer = entity.cooldown;

    }

    // Update is called once per frame
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

        if (!entity.inCombat)
        {
            GetTarget();
        }

        if (targetTransform != null)
        {
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

        if (!entity.inCombat && targetTransform != null)
        {
            direction = targetTransform.position - transform.position;
            rb.MovePosition(rb.position + direction.normalized * entity.speed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Player" && !entity.dead)
        {
            entity.inCombat = true;
            entity.target = collider.gameObject;
        }
            
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            entity.inCombat = false;
            entity.target = null;
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
            targetTransform = targetOnRange.transform;
        }
        else
        {
            targetTransform = null;
        }
    }
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void Attack()
    {        
        if (entity.target != null && !entity.target.GetComponent<Player>().entity.dead)
        {
            rb.velocity = Vector2.zero;
            animator.SetTrigger("Attack");
            damage = manager.CalculateDamage(entity, entity.damage);
            targetDefense = manager.CalculateDefense(entity.target.GetComponent<Player>().entity, entity.target.GetComponent<Player>().entity.defense);
            damageDealt = damage - targetDefense;
            if (damageDealt <= 0)
            {
                damageDealt = 0;
            }
            entity.target.GetComponent<Player>().entity.currentHealth -= damageDealt;

        }
    }

    private void Die()
    {
        entity.dead = true;
        entity.inCombat = false;
        entity.target = null;

        animator.SetTrigger("Die");

        // para add exp no player
        // manager.GainExp(rewardExperience)
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

}
