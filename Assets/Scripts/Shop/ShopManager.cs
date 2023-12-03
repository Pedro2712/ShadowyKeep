using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

using static GlobalVariables;

public class Item
{
    public string name;
    public int price;
    public float value;
    public int id;
}

public class ShopManager : MonoBehaviour
{   
    private string buyMode;
    private int valueOwned;

    [Header("Store")]
    private Dictionary<int, Item> storeItens = new Dictionary<int, Item>();

    public List<string> attributesNames = new List<string>(
        new string[] {"Speed", "Strength", "Defense", "Lucky", "Stamina", "Mana", "Life"}
    );
    public List<string> attackNames = new List<string>(
        new string[] {"PoisonAttack", "ExplosaoArcana", "LifeOrDeath", "InvocacaoProfana"}
    );

    public List<string> allNameItens = new List<string>(
        new string[] {"Speed", "Strength", "Defense", "Lucky", "Stamina", "Mana", "Life",
                      "PoisonAttack", "ExplosaoArcana", "LifeOrDeath", "InvocacaoProfana"}
    );
    public List<int> itensPrice = new List<int>(
        new int[] {35, 40, 40, 30, 450, 400, 500, 15, 40, 50, 60}
    );
    
    [Header("Text Infos")]
    public TextMeshProUGUI CoinsText;
    public TextMeshProUGUI LevelInfoText;
    
    private void Start()
    {
        CoinsText.text = "Coins : " + GlobalVariables.instance.coins.ToString();
        LevelInfoText.text = "Level : " + GlobalVariables.instance.lastPlayerLevel.ToString();
    }

    public void CreateItem(int id)
    {
        string itemName = allNameItens[id];
        int itemPrice = itensPrice[id];
        float itemValue = 0.0f;

        switch (itemName) {
            case "Speed":
                itemValue = (float) GlobalVariables.instance.speed;
                break;
            case "Strength":
                itemValue = (float) GlobalVariables.instance.strength;
                break;
            case "Defense":
                itemValue = (float) GlobalVariables.instance.defense;
                break;
            case "Lucky":
                itemValue = (float) GlobalVariables.instance.lucky;
                break;
            case "Stamina":
                itemValue = (float) GlobalVariables.instance.stamina;
                break;
            case "Mana":
                itemValue = (float) GlobalVariables.instance.mana;
                break;
            case "Life":
                itemValue = (float) GlobalVariables.instance.life;
                break;
            case "PoisonAttack":
                itemValue = (float) GlobalVariables.instance.poisonAttack;
                break;
            case "ExplosaoArcana":
                itemValue = (float) GlobalVariables.instance.explosaoArcana;
                break;
            case "LifeOrDeath":
                itemValue = (float) GlobalVariables.instance.lifeOrDeath;
                break;
            case "InvocacaoProfana":
                itemValue = (float) GlobalVariables.instance.invocacaoProfana;
                break;
        } 
        Item newItem = new Item{    
                                    name = itemName, 
                                    price = itemPrice, 
                                    value = itemValue, 
                                    id = id
                                };
    
        storeItens.Add(newItem.id, newItem);
    }
   
    public string getName(int itemId)
    {
        Item foundItem = storeItens[itemId];
        return foundItem.name;
    }

    public int getPrice(int itemId)
    {
        Item foundItem = storeItens[itemId];
        return foundItem.price;
    }

    public float getValue(int itemId)
    {
        Item foundItem = storeItens[itemId];
        return foundItem.value;
    }

    public string getTypeOfItem(int itemId)
    {
        Item foundItem = storeItens[itemId];
        string itemName = foundItem.name;

        if (attributesNames.Contains(itemName)){
            return "attribute";
        }else{
            return "attack";
        }
    }

    public float calculateGain(int itemId)
    {
        Item foundItem = storeItens[itemId];
        string itemName = foundItem.name;

        if(itemName == "Stamina" || itemName == "Mana" || itemName == "Life"){
            return 10.0f;
        }else{
            return 0.5f;
        }
    }

    private void updateGlobalVariablevalue(string name, float newValue)
    {   
        switch (name) {
            case "Speed":
                GlobalVariables.instance.speed = newValue;
                break;
            case "Strength":
                GlobalVariables.instance.strength = (int) newValue;
                break;
            case "Defense":
                GlobalVariables.instance.defense = (int) newValue;
                break;
            case "Lucky":
                GlobalVariables.instance.lucky = (int) newValue;
                break;
            case "Stamina":
                GlobalVariables.instance.stamina = (int) newValue;
                break;
            case "Mana":
                GlobalVariables.instance.mana = (int) newValue;
                break;
            case "Life":
                GlobalVariables.instance.life = (int) newValue;
                break;
            case "PoisonAttack":
                GlobalVariables.instance.poisonAttack = (int) newValue;
                break;
            case "ExplosaoArcana":
                GlobalVariables.instance.explosaoArcana = (int) newValue;
                break;
            case "LifeOrDeath":
                GlobalVariables.instance.lifeOrDeath = (int) newValue;
                break;
            case "InvocacaoProfana":
                GlobalVariables.instance.invocacaoProfana = (int) newValue;
                break;
        }
    }

    public void buyAttribute(int id)
    {
        Item storeItem = storeItens[id];
        int itemPrice = storeItem.price;
        string itemName  = storeItem.name;

        if(GlobalVariables.instance.coins >= itemPrice){ 
            
            // Sempre aumentamos de 1 em 1 unidade?
            storeItem.value += calculateGain(id);
            updateGlobalVariablevalue(itemName, storeItem.value);
            
            // Atualiza moedas
            GlobalVariables.instance.coins -= itemPrice;
            CoinsText.text = "Coins : " +  GlobalVariables.instance.coins.ToString();
        }else{
            // TODO : Animação de "compra bloqueada"  
            Debug.LogWarning(" [TODO] Não é possível adquirir o item : " + itemName);
        }
    }

    public void buyNewATtack(int id)
    {
        Item storeItem = storeItens[id];
        int itemPrice = storeItem.price;
        string itemName  = storeItem.name;

        if(GlobalVariables.instance.lastPlayerLevel >= itemPrice){
            // Em Global variables o número 1 significa que o ataque foi desbloqueado 
            // e o numero 0 significa que não foi desbloqueado
            storeItem.value = 1.0f;
            updateGlobalVariablevalue(itemName, storeItem.value);
        }else{
            //TODO : Animação de "compra bloqueada"  
           Debug.LogWarning(" [TODO] Não é possível adquirir o ataque : " + itemName);
        }

    }

    public void PressBackToHome()
    {   
        // Vai para a sala de espera
        SceneManager.LoadScene(1); 
    }
}
