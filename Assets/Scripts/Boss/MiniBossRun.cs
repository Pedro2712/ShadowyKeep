using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossRun : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    Boss boss;
    public float speed = 2f;
    public LayerMask obstacleMask;

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
        boss.NotLookAtPlayer();

        if (boss.isInsideRange){

            Vector2 moveDirection = (rb.position - (Vector2)player.position).normalized;
            Vector2 newPos = rb.position + moveDirection * speed * Time.fixedDeltaTime;
            rb.MovePosition(newPos);
        }  
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
