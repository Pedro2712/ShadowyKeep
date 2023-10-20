using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Monsters
{
    public GameObject enemyPrefab;
    public int cost;
}

public class WaveSpawner : MonoBehaviour
{   
    public List<Monsters> enemies = new List<Monsters>();
    public int currWave;
    public int waveValue; // Lawer, quanto maior o lawer maior a quantidade de monstros?
    // Start is called before the first frame update
    public int qtd_enemies; // Quantidade de inimigos que deveram ser gerados

    //Controlando a onda por tempo:
    public List<Transform> spawnLocations;

    public List<Transform> selectedSpawnLocations;
    // public int waveDuration;
    // private float waveTimer;
    // private float spawnInterval;
    // private float spawnTimer;

    public List<GameObject> enemiesToSpawn = new List<GameObject>();
    void Start()
    {
        GenerateWave();

        // Seleciona um número de posições de forma aletória.
        // Sempre tenho mais opções de Respawn do que inimigos para spawnar.
        Debug.LogFormat("Teste");
        int count_location = 0;
        List<int> locationSelectedId = new List<int>();

        while(count_location < enemiesToSpawn.Count){
            int randLocationId = Random.Range(0, spawnLocations.Count);
            
            if (!locationSelectedId.Contains(randLocationId))
            {
                selectedSpawnLocations.Add(spawnLocations[randLocationId]);
                locationSelectedId.Add(randLocationId);
                count_location+=1;
            }
        }
        
        int count_enemies = 0;
        while(enemiesToSpawn.Count > 0 ){
            Instantiate(enemiesToSpawn[count_enemies], selectedSpawnLocations[count_enemies].position, Quaternion.identity);
            // Remove o inimigo da lista de Spawn
            enemiesToSpawn.RemoveAt(0);
            count_enemies+=1;
        }
    }

    // Update is called once per frame
    // void FixedUpdate()
    // {
    //     if(spawnTimer <= 0 )
    //     {
    //         //Spawna um inimigo

    //         if(enemiesToSpawn.Count > 0 ){ // Enquanto tenho inimigos para Spawnar
    //             Instantiate(enemiesToSpawn[0], spawnLocation.position, Quaternion.identity);
    //             // Remove o inimigo da lista de Spawn
    //             enemiesToSpawn.RemoveAt(0);
    //             spawnTimer = spawnInterval;
    //         }
    //         else
    //         {
    //             waveTimer = 0;
    //         }
    //     }
    //     else
    //     {
    //         spawnTimer -= Time.fixedDeltaTime;
    //         waveTimer -= Time.fixedDeltaTime;
    //     }
    // }

    public void GenerateWave()
    {
        waveValue = currWave * 10; // Valor que podemos gastar a depender do layer?
        GenerateEnemies();
        
        // spawnInterval = waveDuration; // enemiesToSpawn.Count; // Tempo fixo entre cada spawn de inimigo.
        // waveTimer = waveDuration; // Duração de uma onda.
    }

    public void GenerateEnemies()
    {
        // Criar uma lita temporária de inimigos para a geração
        // 
        // Enquanto temos "pontos" para gastar (waveValues):
        //         pegamos um inimigos aleatório
        //         se conseguirmos "bancar o inimigo:
        //               add lista de inimigos e waveValues-inimigo.cost
        //         se não:
        //               continuamos no loop

        List<GameObject> generatedEnemies = new List<GameObject>();

        while(waveValue > 0){
            
            int randEnemyId = Random.Range(0, enemies.Count); // Qtd de inimigos que tem disponíveis.
            int randEnemyCost = enemies[randEnemyId].cost;

            if(waveValue - randEnemyCost >= 0){
                generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
                waveValue -= randEnemyCost;
            }
            else if (waveValue<=0){
                break;
            }
        }

        // Lista de inimigos de custo aleatório escolhidos para Spawnar:
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;

    }
}