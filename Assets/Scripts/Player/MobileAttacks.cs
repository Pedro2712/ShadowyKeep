using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileAttacks : MonoBehaviour
{   
    public Player player;

    public Animator animator;

    public ManagerSFX managerSFX;

    public void SimpleSwordAttack(){
        if (player.entity.currentStamina >= player.entity.staminaCost)
        {
            player.entity.currentStamina -= player.entity.staminaCost;
            animator.SetTrigger("attack");
            managerSFX.swordSound();
        }
    }
}
