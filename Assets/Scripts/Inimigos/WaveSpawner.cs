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

    public List<GameObject> enemiesToSpawn = new List<GameObject>();
    void Start()
    {
        GenerateWave();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateWave()
    {
        waveValue = currWave * 10; // Valor que podemos gastar a depender do layer?
        GenerateEnemies();
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