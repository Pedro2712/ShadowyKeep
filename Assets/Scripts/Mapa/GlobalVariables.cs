using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class SerializableDictionary
{
    public string key;
    public Sprite value;
}


public class GlobalVariables : MonoBehaviour
{
    public static GlobalVariables instance;

    [Header("Rooms")]
    public int lastVisitedIndex = -1;
    public int roomsVisited = 0;
    public int totalRooms = 6;

    [Header("Choose award")]
    public string finalChoose = "Coins";

    [Header("Player")]
    public int lastPlayerLevel = 1;
    public int lastExperienceToNextLevel = 100;
    
    // ---- Store ---
    public int selectedItemId;

    public int coins = 1000;   // valor inicial para teste da loja
    public float boughtSpeed = 0;
    public int boughtStrength = 0;
    public int boughtDefense = 0;
    public int boughtLucky = 0;

    public int boughtStamina = 0;
    public int boughtMana = 0;
    public int boughtHealth = 0;

    public float tempSpeed = 0;
    public int tempStrength = 0;
    public int tempDefense = 0;
    public int tempMaxHealth = 0;
    public int tempMaxStamina = 0;
    
    public int simpleAttack = 1;
    public int poisonAttack = 0;
    public int explosaoArcana = 0;
    public int lifeOrDeath = 0;
    public int invocacaoProfana = 0;
    

    [Header("BackGround Musics")]
    public List<AudioClip> backgroundMusics;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
