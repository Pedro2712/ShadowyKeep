using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Entity
{
    [Header("Name")]
    public string name;
    public int level = 1;
    public int experience = 0;
    public int experienceToNextLevel = 100;
    public int maxLevel = 50;
    public int coins = 0;

    [Header("Health")]
    public int currentHealth;
    public int maxHealth;

    [Header("Mana")]
    public int currentMana;
    public int maxMana;

    [Header("Stamina")]
    public int currentStamina;
    public int maxStamina;
    public int staminaCost = 1;

    [Header("Stats")]
    public int strength = 1;
    public int resistence = 1;
    public int inteligence = 1;
    public int damage = 1;
    public int defense = 1;
    public float speed = 2f;

    [Header("Combat")]
    public bool inCombat = false;
    public float cooldown = 1f;
    public GameObject target;
    public bool dead = false;

}