using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    void OnSimpleSwordAttack(){
        print("Attack Pressed");
        animator.SetTrigger("attack");
    }
}
