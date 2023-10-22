using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public Entity entity = new Entity();
    public GameManagerBattle manager;

    public Transform player;
    public bool isFlipped = false;
    public int attackDamage = 1;

    public Vector3 attackOffset;
    public float attackRange = 4f;
    public LayerMask attackMask;
    public Animator animator;
    public float detectionRadius = 5f;
    public LayerMask playerLayer;
    public bool isInsideRange = false;
    private Collider2D targetOnRange;

    private int takenDamage = 0;
    private int playerDefense = 0;
    private int damageDealt = 0;

    public bool isEnraged = false;

    public GameObject enemy;

    public BoxCollider2D boxCollider; 

    public GameObject BossBasicHit;

    public float knockbackForce = 25f;


    private void Start()
    {
        manager = FindObjectOfType<GameManagerBattle>();

        boxCollider = GetComponent<BoxCollider2D>();

        entity.maxHealth = 300;

        entity.currentHealth = entity.maxHealth;

    }

    public void LookAtPlayer(){
        Vector3 flipped = transform.localScale;
        flipped.z *=  -1f;
        if (transform.position.x > player.position.x && isFlipped){
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped){
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void NotLookAtPlayer(){
        Vector3 flipped = transform.localScale;
        flipped.z *=  -1f;
        if (transform.position.x < player.position.x && isFlipped){
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x > player.position.x && !isFlipped){
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
    
    public void Attack(){
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
    }

   public void Update(){

        targetOnRange = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);

        isInsideRange = targetOnRange != null;
        animator.SetBool("isInsideRange", isInsideRange);
        if (entity.currentHealth <= 200 && isEnraged == false){
            animator.SetTrigger("Enraged");
            isEnraged = true;
            boxCollider.size = new Vector2(5f, 7f);
            boxCollider.offset = new Vector2(0f, 3.5f);
            detectionRadius = 15f;
        }
   }

   private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("weapon"))
        {
            ApplyDamage(collider.GetComponentInParent<Player>().entity);
            if (isEnraged){
                animator.SetTrigger("BossHit");
            }
            else{
                animator.SetTrigger("MiniBossHit");
            }
        }
    }

    private void EnableHitbox()
    {
        BossBasicHit.SetActive(true);
    }

    private void DisableHitbox()
    {
        BossBasicHit.SetActive(false);
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

    }

}
