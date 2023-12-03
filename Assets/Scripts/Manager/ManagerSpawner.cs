using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSpawner : MonoBehaviour
{
    [System.Serializable]
     public class Monsters
     {
          public GameObject enemyPrefab;
          public int force;
     }

     public List<Monsters> enemies = new List<Monsters>();
     
     public GameObject spawnLocationsGameObject;

     private int NUM_SPAWNS = 16;

     public List<Transform> selectedSpawnLocations;
     public List<GameObject> enemiesToSpawn = new List<GameObject>();

     private GameObject spawnedEnemy;

    public GameObject playerGO;

    private void Start()
    {

        // Spawna inimigos na sala:
        int qtd_enemies = 10;
        int playerLevel = GlobalVariables.instance.lastPlayerLevel;
        int maxLevel = playerGO.GetComponent<Player>().entity.maxLevel;
        int difficulty  = CalculateEnemyDifficulty(playerLevel, maxLevel);


        GenerateWave(qtd_enemies , difficulty);
    }

    public void GenerateWave(int qtd_enemies , int difficulty )
     {
          GenerateEnemies(qtd_enemies);
          Transform[] spawnLocations = spawnLocationsGameObject.GetComponentsInChildren<Transform>();
          int count_location = 0;
          int count_enemies = 0;

          List<int> locationSelectedId = new List<int>();

          while(count_location < qtd_enemies){
               int randLocationId = Random.Range(0, NUM_SPAWNS);
               
               if (!locationSelectedId.Contains(randLocationId))
               {
                    selectedSpawnLocations.Add(spawnLocations[randLocationId]);
                    locationSelectedId.Add(randLocationId);
                    count_location+=1;
               }
          }
          
          while(count_enemies < qtd_enemies ){
               spawnedEnemy = Instantiate(enemiesToSpawn[count_enemies], selectedSpawnLocations[count_enemies].position, Quaternion.identity);
               if (spawnedEnemy.GetComponent<Enemy>() != null){
                    spawnedEnemy.GetComponent<Enemy>().entity.level = Mathf.Min(difficulty, spawnedEnemy.GetComponent<Enemy>().entity.maxLevel);
               }
               else if (spawnedEnemy.GetComponent<EnemyRange>() != null){
                    spawnedEnemy.GetComponent<EnemyRange>().entity.level = Mathf.Min(difficulty, spawnedEnemy.GetComponent<EnemyRange>().entity.maxLevel);
               }
               count_enemies+=1;
          }
          enemiesToSpawn.Clear();
          selectedSpawnLocations.Clear();
     }

     public void GenerateEnemies(int qtd_enemies)
     {
          // Criar uma lita temporária de inimigos para a geração
          // 
          // Enquanto temos "pontos" para gastar (waveValues):
          //         pegamos um inimigos aleatório
          //         se o inimigo possui a força requerida para a fase : 
          //               add lista de inimigos e waveValues-inimigo.cost
          
          List<GameObject> generatedEnemies = new List<GameObject>();
          
          int n_enemies = 0;
          while(n_enemies < qtd_enemies){
               int randEnemyId = Random.Range(0, enemies.Count); 
               //int randEnemyForce = enemies[randEnemyId].force;

               generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
               n_enemies+=1;
               
          }
          
          // Lista de inimigos de custo aleatório escolhidos para Spawnar:
          enemiesToSpawn.Clear();
          enemiesToSpawn = generatedEnemies;
                            
     }

     public int CalculateEnemyDifficulty(int playerLevel, int maxLevel)
     {
          // Fórmula para calcular a dificuldade (level) do inimigo.

          float difficulty = (float) maxLevel / ( (GlobalVariables.instance.totalRooms - GlobalVariables.instance.roomsVisited) + ( (float) (maxLevel / 2) / (float) playerLevel ) ) ;
          return (int) difficulty;
     }
}
