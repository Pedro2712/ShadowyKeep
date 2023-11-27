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
    public int selectedItemId;

    public int coins = 1000;   // valor inicial para teste da loja
    public double speed = 5.0;
    public double strength = 2.0;
    public double defense = 3.0;
    public double lucky = 1.0;

    public double life = 100.0;
    public double mana = 150.0;
    public double stamina = 100.0;
    
    public double simpleAttack = 1;
    public double poisonAttack = 0;
    public double explosaoArcana = 0;
    public double lifeOrDeath = 0;
    public double invocacaoProfana = 0;
    

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
