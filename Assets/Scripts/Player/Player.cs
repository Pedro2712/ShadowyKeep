using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Entity entity;

    [Header("Player UI")]
    public Slider health;
    public Slider mana;
    public Slider stamina;

    //Para Slider de Level
    //public Slider exp;

    void Start()
    {
        entity.currentHealth = entity.maxHealth;
        entity.currentMana = entity.maxMana;
        entity.currentStamina = entity.maxStamina;

        health.maxValue = entity.maxHealth;
        health.value = health.maxValue;

        mana.maxValue = entity.maxMana;
        mana.value = mana.maxValue;

        stamina.maxValue = entity.maxStamina;
        stamina.value = stamina.maxValue;

        //exp.value = 0;
    }

    private void Update()
    {
        health.value =entity.currentHealth;
        mana.value =entity.currentMana;
        stamina.value =entity.currentStamina;
        
        // APENAS TESTE  - Atualização de Mana, Vida e Stamina.
        if(Input.GetKeyDown(KeyCode.Space))
            entity.currentHealth -=1;
            
        
    }
}
