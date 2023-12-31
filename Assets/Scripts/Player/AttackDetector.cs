using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetector : MonoBehaviour
{
    public Player player;

    void OnTriggerEnter2D (Collider2D collider) {
        if (collider.gameObject.tag == "Enemy") {
            Enemy enemy = collider.GetComponentInParent<Enemy>();
            Boss boss = collider.GetComponentInParent<Boss>();

            if (enemy != null && enemy.entity.currentHealth <= 0)
            {
                player.entity.experience += enemy.entity.experience;
            }
        }

    }
}
