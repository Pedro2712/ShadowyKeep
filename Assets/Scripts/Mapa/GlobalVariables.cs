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
    public int totalRooms = 2;

    [Header("Choose award")]
    public string finalChoose = "Coins";
    
    public List<SerializableDictionary> serializedSprites = new List<SerializableDictionary>();

    public Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

    [Header("Player")]
    public int lastPlayerLevel = 1;
    public int lastExperienceToNextLevel = 100;
    
    // ---- Store ---
    public int coins = 1000;   // valor inicial para teste da loja
    public double speed = 5.0;
    public double strength = 2.0;
    public double defense = 3.0;
    public double lucky = 0.0;

    public int life = 100;
    public int mana = 150;
    public int stamina = 100;
    
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

    void OnValidate()
    {
        // This method is called when changes are made in the Inspector
        // Update the 'sprites' dictionary from 'serializedSprites'
        sprites.Clear();
        foreach (var item in serializedSprites)
        {
            if (!sprites.ContainsKey(item.key))
            {
                sprites.Add(item.key, item.value);
            }
        }
    }
}
