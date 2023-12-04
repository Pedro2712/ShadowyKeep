using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject HitBoxAttackTop;
    public GameObject HitBoxAttackRight;
    public GameObject HitBoxAttackLeft;
    public GameObject HitBoxAttackDown;
    public GameObject projectilePrefab;
    public float projectileSpeed = 5f;

    public void EnableHitBoxAttackTop()
    {
        if (HitBoxAttackTop != null)
        {
            HitBoxAttackTop.SetActive(true);
        }
        else
        {
            Debug.LogWarning("HitBoxAttackTop n�o foi atribu�do no Inspector.");
        }
    }
    
    public void EnableHitBoxAttackRight()
    {
        if (HitBoxAttackRight != null)
        {
            HitBoxAttackRight.SetActive(true);
        }
        else
        {
            Debug.LogWarning("HitBoxAttackRight n�o foi atribu�do no Inspector.");
        }
    }
    
    public void EnableHitBoxAttackLeft()
    {
        if (HitBoxAttackLeft != null)
        {
            HitBoxAttackLeft.SetActive(true);
        }
        else
        {
            Debug.LogWarning("HitBoxAttackLeft n�o foi atribu�do no Inspector.");
        }
    }
    public void EnableHitBoxAttackDown()
    {
        if (HitBoxAttackDown != null)
        {
            HitBoxAttackDown.SetActive(true);
        }
        else
        {
            Debug.LogWarning("HitBoxAttackDown n�o foi atribu�do no Inspector.");
        }
    }

    public void DisableAll() {
        HitBoxAttackTop.SetActive(false);
        HitBoxAttackRight.SetActive(false);
        HitBoxAttackLeft.SetActive(false);
        HitBoxAttackDown.SetActive(false);
    }

    private void LaunchProjectile(string direction)
    {
        Vector2 spawnPosition = transform.position;
        Vector2 directionVector = Vector2.zero;

        switch (direction)
        {
            case "top":
                directionVector = Vector2.up;
                break;
            case "right":
                directionVector = Vector2.right;
                break;
            case "down":
                directionVector = Vector2.down;
                break;
            case "left":
                directionVector = Vector2.left;
                break;
        }

        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        projectile.transform.SetParent(transform);
        projectile.SetActive(true);
        StartCoroutine(MoveProjectile(projectile, directionVector));
    }

    private IEnumerator MoveProjectile(GameObject projectile, Vector2 direction)
    {
        float elapsedTime = 0f;

        while (elapsedTime < 3f && projectile != null) // Tempo de vida do projétil (ajuste conforme necessário)
        {
            float step = projectileSpeed * Time.deltaTime;
            projectile.transform.Translate(direction * step);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        Destroy(projectile);
    }
}
