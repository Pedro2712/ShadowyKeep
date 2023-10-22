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
     
     public List<Transform> spawnLocations1;
     public List<Transform> spawnLocations2;
     public List<Transform> spawnLocations3;
     public List<Transform> spawnLocations4;
     public List<Transform> spawnLocations5;

     private int NUM_SPAWNS = 16;

     public List<Transform> selectedSpawnLocations;
     public List<GameObject> enemiesToSpawn = new List<GameObject>();

     private GameObject spawnedEnemy;
     
     public void GenerateWave(int qtd_enemies , int difficulty  , int id_room)
     {
          GenerateEnemies(qtd_enemies);

          int count_location = 0;
          int count_enemies = 0;

          List<int> locationSelectedId = new List<int>();
          List<Transform> spawnLocations = new List<Transform>();
          
          // Seleciona spawn points a depender da sala:
          if(id_room == 1){
               spawnLocations = spawnLocations1;
          }else if (id_room == 2){
               spawnLocations = spawnLocations2;
          }else if (id_room == 3){
               spawnLocations = spawnLocations3;
          }else if (id_room == 4){
               spawnLocations = spawnLocations4;
          }else if (id_room == 5){
               spawnLocations = spawnLocations5;
          }

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
               // Fórmula para calcular a dificuldade do inimigo. Precisa de balanceamento.
               spawnedEnemy.GetComponent<Enemy>().entity.level = difficulty;
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
          int difficulty = (int) maxLevel / ( (GlobalVariables.instance.totalRooms - GlobalVariables.instance.roomsVisited) + (int) ( (maxLevel / 2) / playerLevel ) ) ;
          return difficulty;
     }
}
