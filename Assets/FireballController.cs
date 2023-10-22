using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    public float speed = 5.0f;

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Destrua a fireball quando ela estiver fora da tela
        if (transform.position.y < -10.0f)
        {
            Destroy(gameObject);
        }
    }
}
