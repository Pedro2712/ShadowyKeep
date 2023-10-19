using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{   
    public Entity entity;

    [Header("Player Regen System")]
    public bool regenHPEnabled = true;
    public float regenHPTime = 1f; // Segundos 
    public int regenHPValue = 1;
    public bool regenMPEnabled = true;
    public float regenMPTime = 1f; // Segundos 
    public int regenMPValue = 1;

    [Header("Game Manager")]
    public GameManagerBattle manager;

    [Header("Player UI")]
    public Slider health;
    public Slider mana;
    public Slider stamina;

    //Para Slider de Level
    //public Slider exp;

    // Start is called before the first frame update
    void Start()
    {   
        if(manager == null){
            Debug.LogError("Você precisa anexar o Game Manager aqui no player.");
            return;
        }

        // Setando o valor máximo de Vida e Stamina a depender dos atributos do Player:
        entity.maxHealth = manager.CalculateHealth(this);
        entity.maxStamina = manager.CalculateStamina(this);
        
        // Exemplo de uso do calculateDamage e do calculateDefense
        // int dmg = manager.CalculateDamage(this, 10);   PLAYER ATACA
        // int def = manager.CalculateDefense(this, 10);  INIMIGO POSSUI UMA DEFESA
        // delta_dano = dmg - def;   ESSE É O DANO REAL QUE O INIMIGO SOFRE.

        entity.currentHealth = entity.maxHealth;
        entity.currentStamina = entity.maxStamina;
        entity.currentMana = entity.maxMana;         // Por enquanto sem implementação.
        

        health.maxValue = entity.maxHealth;
        health.value = health.maxValue;

        mana.maxValue = entity.maxMana;
        mana.value = mana.maxValue;

        stamina.maxValue = entity.maxStamina;
        stamina.value = stamina.maxValue;

        //exp.value = 0;

        //Iniciar a regeneração de vida:
        StartCoroutine(RegenHealth());
        StartCoroutine(RegenStamina());
    }

    // Update is called once per frame
    void Update()
    {
        health.value =entity.currentHealth;
        mana.value =entity.currentMana;
        stamina.value =entity.currentStamina;
        
        // APENAS TESTE  - Atualização de Mana, Vida e Stamina.
        if(Input.GetKeyDown(KeyCode.Space))
        {
            entity.currentHealth -= 1;
            entity.currentStamina -= 2;
        }   

    }

    // IEnumerator é um Loop Infinito
    // Regerear Vida
    IEnumerator RegenHealth(){
        while(true){ // Loop

            if(regenHPEnabled){
                if(entity.currentHealth < entity.maxHealth){
                    Debug.LogFormat("Recuperando HP do jogador");
                    entity.currentHealth += regenHPValue;
                    yield return new WaitForSeconds(regenHPTime);
                }
                else
                {
                    yield return null;
                }
            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator RegenStamina(){
        while(true){ // Loop

            if(regenMPEnabled){
                if(entity.currentStamina < entity.maxStamina){
                    Debug.LogFormat("Recuperando Mana do jogador");
                    entity.currentStamina += regenMPValue;
                    yield return new WaitForSeconds(regenMPTime);
                }
                else
                {
                    yield return null;
                }
            }
            else
            {
                yield return null;
            }
        }
    }

}
