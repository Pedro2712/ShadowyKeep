using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{   
    public ManagerSpawner managerSpawner;

    private ManagerSFX managerSFX;
    public MenuMusic menuMusic;

    public List<Transform> possibleDestinations;
    public GameObject playerGO;
    private bool playerDetected;

    void Start()
    {
        playerDetected = false;
        GameObject temp = GameObject.FindGameObjectsWithTag("WaveSpawner")[0];
        managerSpawner = temp.GetComponent<ManagerSpawner>();

        // Começa a tocar a musica no Home
        GameObject temp2 = GameObject.FindGameObjectsWithTag("HomeMusic")[0];
        menuMusic = temp2.GetComponent<MenuMusic>();
        menuMusic.playHomeMusic();

        //Pega o Objeto responsavel pelas musicas de batalha
        managerSFX = FindObjectOfType<ManagerSFX>();
    }

    void Update()
    {
        if (playerDetected)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (possibleDestinations.Count > 0)
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
                    GlobalVariables.instance.roomsVisited++;

                    // Spawna inimigos na sala:
                    int qtd_enemies = 10;
                    int difficulty = managerSpawner.CalculateEnemyDifficulty(playerGO.GetComponent<Player>().entity.level, playerGO.GetComponent<Player>().entity.maxLevel);
                    
                    //Para musica do menu e começa uma musia Porrada!
                    menuMusic.stopHomeMusic();
                    managerSFX.backgroundSound(false); // False indica que não é a musica do boss.
                    managerSpawner.GenerateWave(qtd_enemies , difficulty , randomIndex+1);
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
