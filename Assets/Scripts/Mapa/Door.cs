using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public List<Transform> possibleDestinations;
    private bool playerDetected;
    public GameObject playerGO;

    void Start()
    {
        playerDetected = false;
    }

    void Update()
    {
        if (playerDetected)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (possibleDestinations.Count > 1)
                {
                    int randomIndex = Random.Range(0, possibleDestinations.Count);

                    // Garante que a sala escolhida não seja a mesma que a última visitada
                    while (randomIndex == GlobalVariables.instance.lastVisitedIndex)
                    {
                        randomIndex = Random.Range(0, possibleDestinations.Count);
                    }

                    Transform randomDestination = possibleDestinations[randomIndex];
                    playerGO.transform.position = randomDestination.position;

                    GlobalVariables.instance.lastVisitedIndex = randomIndex;
                }

                playerDetected = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = true;
            playerGO = collision.gameObject;
        }
    }
}
