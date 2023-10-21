using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;
    public int attackDamage = 1;

    public Vector3 attackOffset;
    public float attackRange = 4f;
    public LayerMask attackMask;
    public int health = 100;
    public Animator animator;

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
    
    public void Attack(){
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
    }

   public void Update(){
        if (health > 0){
            if (Input.GetKeyDown(KeyCode.M)){
                health = health - 10;
            }            
        } 
        else if (health == 0) {
            Debug.Log("entrei aq");
            animator.SetTrigger("Enraged");
            health = 100;
        }
   }  
}
