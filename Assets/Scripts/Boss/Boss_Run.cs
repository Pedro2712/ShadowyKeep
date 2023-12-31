using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    Boss boss;
    public float speed = 5f;
    public float attackRange = 4f;
    private float cooldownTimer = 0f;
    public List<string> triggerNames;
    public GameObject bossObject; 

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
    }


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();

        if (boss.isInsideRange){
            if (rb.position.x < player.position.x){
                Vector2 target = new Vector2(player.position.x - 3, player.position.y - 1);
                Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);
            }
            else {
                Vector2 target = new Vector2(player.position.x + 3, player.position.y - 1);
                Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);
            }
            if (Vector2.Distance(player.position, rb.position) <= attackRange){

                cooldownTimer -= Time.deltaTime;
                if (cooldownTimer <= 0)
                {
                    cooldownTimer = 1;
                    // Attack 
                    int randomIndex = Random.Range(0, triggerNames.Count);
                    string triggerName = triggerNames[randomIndex];
                    switch (triggerName)
                    {
                        case "Attack":
                            boss.managerSFX.bossCleaveSound();
                            break;
                        case "SmashHit":
                            boss.managerSFX.bossSmashSound();
                            break;
                        case "FireBreath":
                            boss.managerSFX.bossFireBreathSound();
                            break;
                        case "CastSpell":
                            boss.managerSFX.bossFireBallSound();
                            break;
                    }
                    animator.SetTrigger(triggerName);
                }
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.ResetTrigger("Attack");
    }

}
