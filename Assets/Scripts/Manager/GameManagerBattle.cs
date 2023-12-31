using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBattle : MonoBehaviour
{   
     // CalculateHealth 
     private readonly int BASE_HEALTH_RESISTENCE = 10;
     private readonly int BASE_HEALTH_LEVEL_RESISTENCE = 6;
     private readonly int CONST_HEALTH = 100;
     private int BOUGHT_HEALTH = 0;
     private int TEMP_HEALTH = 0;

     // CalculateStamina
     private readonly int BASE_STAMINA_LEVEL = 5;
     private readonly int CONST_STAMINA = 5;
     private int BOUGHT_STAMINA = 0;
     private int TEMP_STAMINA = 0;

     // CalculateDamage
     private readonly int BASE_DAMAGE = 4;
     private readonly int BASE_DAMAGE_LEVEL = 3;
     private int BOUGHT_STRENGTH = 0;
     private int TEMP_STRENGTH = 0;

     // CalculateDefense
     private readonly int BASE_DEFENSE_RESISTENCE = 1;
     private readonly int BASE_DEFENSE_LEVEL = 2;
     private int BOUGHT_DEFENSE = 0;
     private int TEMP_DEFENSE = 0;

     // CalculateEnemyExperience
     private readonly int BASE_EXPERIENCE = 5;
     private readonly int CONST_EXPERIENCE = 10;

     // Speed
     private float BOUGHT_SPEED = 0;
     private float TEMP_SPEED = 0;

   public int CalculateHealth(Entity entity)
   {
      // Formula Proposta : (RESISTENCIA * BASE_HEALTH_RESISTENCE) + (LEVEL * BASE_HEALTH_LEVEL_RESISTENCE) + BOUGHT_HEALTH + TEMP_HEALTH + CONST_HEALTH
      int result = (entity.resistence * BASE_HEALTH_RESISTENCE) + (entity.level * BASE_HEALTH_LEVEL_RESISTENCE) + BOUGHT_HEALTH + TEMP_HEALTH + CONST_HEALTH;
      return result;
   }
   
   public int CalculateStamina(Entity entity)
   {
      // Formula Proposta : (LEVEL * BASE_STAMINA_LEVEL) + BOUGHT_STAMINA + TEMP_STAMINA + CONST_STAMINA;
      int result = (entity.level * BASE_STAMINA_LEVEL) + BOUGHT_STAMINA + TEMP_STAMINA + CONST_STAMINA;
      return result;
   }

   public int CalculateDamage(Entity entity, int weaponDamage)
   {
      // Formula Proposta :  (STRENGTH*BASE_DAMAGE) + BOUGHT_STRENGTH + TEMP_STRENGTH + (weaponDamage*BASE_DAMAGE) + (LEVEL*BASE_LEVEL_DAMAGE) + random(1-20);
      System.Random rnd = new System.Random();
      int result = (entity.strength * BASE_DAMAGE) + BOUGHT_STRENGTH + TEMP_STRENGTH + (weaponDamage * BASE_DAMAGE) + (entity.level * BASE_DAMAGE_LEVEL) + rnd.Next(1,20);
      return result;
   }

   public int CalculateDefense(Entity entity, int armorDefense)
   {
      // Formula Proposta :  (RESISTENCIA  * BASE_DEFENSE_RESISTENCE) + (LEVEL * BASE_DEFENSE_LEVEL) + BOUGHT_DEFENSE + TEMP_DEFENSE + ARMOR_DEFENSE;
      int result = (entity.resistence  * BASE_DEFENSE_RESISTENCE) + (entity.level * BASE_DEFENSE_LEVEL) + BOUGHT_DEFENSE + TEMP_DEFENSE + armorDefense;
      return result;
   }

   public float CalculateSpeed(Entity entity)
   {
      // Formula Proposta :  VELOCIDADE + BOUGHT_SPEED + TEMP_SPEED;
      float result = entity.speed + BOUGHT_SPEED + TEMP_SPEED;
      return result;
   }

   public int CalculateEnemyExperience(Entity entity)
   {
      // Formula Proposta :  (LEVEL * BASE_EXPERIENCE);
      int result = (entity.level * BASE_EXPERIENCE) + CONST_EXPERIENCE;
      return result;
   }

   public int CalculateBossDamage(Entity entity)
   {
      // Formula Proposta :  (STRENGTH*BASE_DAMAGE) + (LEVEL*BASE_LEVEL_DAMAGE) + random(1-20);
      System.Random rnd = new System.Random();
      int result = (entity.strength * BASE_DAMAGE) + (entity.level * BASE_DAMAGE_LEVEL) + rnd.Next(1,20);
      return result;
   }

   public int CalculateFireballDamage(Entity entity)
   {
      // Formula Proposta :  (STRENGTH*BASE_DAMAGE) + (weaponDamage*BASE_DAMAGE) + (LEVEL*BASE_LEVEL_DAMAGE) + random(1-20);
      System.Random rnd = new System.Random();
      int result = (entity.inteligence * BASE_DAMAGE) + (entity.level * BASE_DAMAGE_LEVEL) + rnd.Next(1,20);
      return result;
   }

   public void UpdateHealth(int boughtHealth, int tempHealth)
   {
      BOUGHT_HEALTH = boughtHealth;
      TEMP_HEALTH = tempHealth;
   }

   public void UpdateStamina(int boughtStamina, int tempStamina)
   {
      BOUGHT_STAMINA = boughtStamina;
      TEMP_STAMINA = tempStamina;
   }
   
   public void UpdateStrength(int boughtStrength, int tempStrength)
   {
      BOUGHT_STRENGTH = boughtStrength;
      TEMP_STRENGTH = tempStrength;
   }

   public void UpdateDefense(int boughtDefense, int tempDefense)
   {
      BOUGHT_DEFENSE = boughtDefense;
      TEMP_DEFENSE = tempDefense;
   }

   public void UpdateSpeed(float boughtSpeed, float tempSpeed)
   {
      BOUGHT_SPEED = boughtSpeed;
      TEMP_SPEED = tempSpeed;
   }

   public void UpdateCoins(Entity entity, int coins)
   {
      entity.coins = coins;
   }

}
