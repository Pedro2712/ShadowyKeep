using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public Animator animator;
    public float detectionRadius = 5f;
    public LayerMask playerLayer;
    public bool isInsideRange = false;
    private Collider2D targetOnRange;

    private int receivedDamage = 0;
    private int enemyDefense = 0;
    private int totalDamage = 0;

    public bool isEnraged = false;

    public GameObject enemy;

    public BoxCollider2D boxCollider; 

    public GameObject BossBasicHit;
    public GameObject BossSmashHit;
    public GameObject BossFireBreath;

    public GameObject[] FireballSpawnerList;

    public float knockbackForce = 20f;

    public int HealthMax;

     public ManagerSFX managerSFX;

    private void Start()
    {
        manager = FindObjectOfType<GameManagerBattle>();

        GameObject temp2 = GameObject.FindGameObjectsWithTag("ManagerSFX")[0];
        managerSFX = temp2.GetComponent<ManagerSFX>();

        boxCollider = GetComponent<BoxCollider2D>();

        entity.maxHealth = HealthMax;

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
    
    

   public void Update(){

        targetOnRange = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);

        isInsideRange = targetOnRange != null;
        animator.SetBool("isInsideRange", isInsideRange);
        if (entity.currentHealth <= (int)(0.8*(float)HealthMax) && entity.currentHealth > 0 && isEnraged == false){
            animator.SetTrigger("Enraged");
            isEnraged = true;
            boxCollider.size = new Vector2(5f, 7f);
            boxCollider.offset = new Vector2(0f, 3.5f);
            detectionRadius = 15f;
        }
        else if (entity.currentHealth <= 0){
            animator.SetTrigger("BossDeath");
            StartCoroutine(DelayedWin());
        }
   }

    private IEnumerator DelayedWin(){
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);
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

    private void EnableBossSmashHitbox()
    {
        BossSmashHit.SetActive(true);
    }

    private void DisableBossSmashHitbox()
    {
        BossSmashHit.SetActive(false);
    }

    private void EnableBossFireBreathbox()
    {
        BossFireBreath.SetActive(true);
    }

    private void DisableBossFireBreathbox()
    {
        BossFireBreath.SetActive(false);
    }

    void EnableFireballs()
    {
        foreach (GameObject objeto in FireballSpawnerList)
        {
            objeto.GetComponent<FireballSpawner>().StartSpawnFireballs();
            objeto.SetActive(true);
        }
    }

    void DisableFireballs()
    {
        foreach (GameObject objeto in FireballSpawnerList)
        {
            objeto.GetComponent<FireballSpawner>().StopSpawnFireballs();
            objeto.SetActive(false);
        }
    }

    private void ApplyDamage(Entity enemyEntity)
    {
        if (entity.dead)
        {
            return;
        }
        receivedDamage = manager.CalculateDamage(enemyEntity, enemyEntity.damage);
        enemyDefense = manager.CalculateDefense(entity, entity.defense);
        totalDamage = receivedDamage - enemyDefense;
        if (totalDamage <= 0)
        {
            totalDamage = 0;
        }

        entity.currentHealth -= totalDamage;

    }

}
