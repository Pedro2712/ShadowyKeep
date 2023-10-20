using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private Collider2D hitbox;
    private Vector2 rightAttackOffset;
    private Vector2 leftAttackOffset;

    private void Start()
    {
        hitbox = GetComponent<Collider2D>();
        rightAttackOffset = transform.position;
        leftAttackOffset = new Vector2(-rightAttackOffset.x, rightAttackOffset.y);
    }
    public void SetAttackRight()
    {
        transform.position = rightAttackOffset;
        hitbox.enabled = true;
    }

    public void SetAttackLeft()
    {
        transform.position = leftAttackOffset;
        hitbox.enabled = true;
    }

    private void StopAttack()
    {
        hitbox.enabled = false;
    }
}