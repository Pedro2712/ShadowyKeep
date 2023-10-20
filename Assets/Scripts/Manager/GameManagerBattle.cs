using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBattle : MonoBehaviour
{   
   // CalculateHealth 
   private int BASE_HEALTH_RESISTENCE = 10;
   private int BASE__HEALTH_LEVEL_RESISTENCE = 4;
   private int CONST_HEALTH = 10;

   //CalculateStamina
   private int BASE_STAMINA_LEVEL = 5;
   private int CONST_STAMINA = 5;

   //CalculateDamage
   private int BASE_DAMAGE = 2;
   private int BASE_DAMAGE_LEVEL = 3;

   //CalculateDefense
   private int BASE_DEFENSE_RESISTENCE = 2;
   private int BASE_DEFENSE_LEVEL = 3;

   public int CalculateHealth(Entity entity)
   {
        // Formula Proposta : (RESISTENCIA * BASE_HEALTH_RESISTENCE) + (LEVEL + BASE__HEALTH_LEVEL_RESISTENCE) + CONST_HEALTH
        int result = (entity.resistence * BASE_HEALTH_RESISTENCE) + (entity.level * BASE__HEALTH_LEVEL_RESISTENCE) + CONST_HEALTH;
        Debug.LogFormat("CalculateHealth : {0}", result);
        return result;
   }
   
   public int CalculateStamina(Entity entity)
   {
        // Formula Proposta : (LEVEL * BASE_STAMINA_LEVEL) + CONST_STAMINA;
        int result = (entity.level * BASE_STAMINA_LEVEL) + CONST_STAMINA;
        Debug.LogFormat("CalculateStamina : {0}", result);
        return result;
   }

   public int CalculateDamage(Entity entity, int weaponDamage){
        // Formula Proposta :  (STRENGTH*BASE_DAMAGE) + (weaponDamage*BASE_DAMAGE) + (LEVEL*BASE_LEVEL_DAMAGE) + random(1-20);
        System.Random rnd = new System.Random();
        int result = (entity.strength * BASE_DAMAGE) + (weaponDamage * BASE_DAMAGE) + (entity.level * BASE_DAMAGE_LEVEL) + rnd.Next(1,20);
        Debug.LogFormat("CalculateDamage : {0}", result);
        return result;
   }

   public int CalculateDefense(Entity entity, int armorDefense){
        // Formula Proposta :  (RESISTENCIA  * BASE_DEFENSE_RESISTENCE) + (LEVEL * BASE_DEFENSE_LEVEL) + ARMOR_DEFENSE;
        int result = (entity.resistence  * BASE_DEFENSE_RESISTENCE) + (entity.level * BASE_DEFENSE_LEVEL) + armorDefense;
        Debug.LogFormat("CalculateDefense : {0}", result);
        return result;
   }

}
