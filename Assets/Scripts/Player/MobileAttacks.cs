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

    public void InvocacaoProfana(){
        if (player.entity.currentStamina >= player.entity.staminaCost)
        {
            player.entity.currentStamina -= player.entity.staminaCost;
            animator.SetTrigger("invocacaoProfana");
            managerSFX.invocacaoProfanaSound(); // mudar para som de invocacao profana
        }
    }
}
