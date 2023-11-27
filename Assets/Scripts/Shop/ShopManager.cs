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
    public double actualValue;
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
    
    void Start()
    {
        CoinsText.text = "Coins : " + GlobalVariables.instance.coins.ToString();
        LevelInfoText.text = "Level : " + GlobalVariables.instance.lastPlayerLevel.ToString();
    }

    public void createItem(int id)
    {   
        Item newItem = new Item{name = allNameItens[id], 
                              price = itensPrice[id], 
                              actualValue = GlobalVariables.instance.speed, 
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

    public double getActualValue(int itemId)
    {
        Item foundItem = storeItens[itemId];
        return foundItem.actualValue;
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

    public double calculateGain(int itemId)
    {
        Item foundItem = storeItens[itemId];
        string itemName = foundItem.name;

        if(itemName == "Stamina" || itemName == "Mana" || itemName == "Life"){
            return 10.0;
        }else{
            return 0.5;
        }
    }

    private void updateGlobalVariablevalue(string name, double newValue)
    {   
        if(name == "Speed"){
            GlobalVariables.instance.speed = newValue;
            Debug.LogError(" Update speed  : " + GlobalVariables.instance.speed);
        }else if(name == "Strength"){
            GlobalVariables.instance.strength = newValue;
            Debug.LogError(" Update strength  : " + GlobalVariables.instance.strength);
        }else if(name == "Defense"){
            GlobalVariables.instance.defense = newValue;
            Debug.LogError(" Update defense  : " + GlobalVariables.instance.defense);
        }else if(name == "Lucky"){
            GlobalVariables.instance.lucky = newValue;
            Debug.LogError(" Update lucky  : " + GlobalVariables.instance.lucky);
        }else if(name == "Stamina"){
            GlobalVariables.instance.stamina = newValue;
            Debug.LogError(" Update stamina  : " + GlobalVariables.instance.stamina);
        }else if(name == "Mana"){
            GlobalVariables.instance.mana = newValue;
            Debug.LogError(" Update mana  : " + GlobalVariables.instance.mana);
        }else if(name == "Life"){
            GlobalVariables.instance.life = newValue;
            Debug.LogError(" Update life  : " + GlobalVariables.instance.life);
        }else if(name == "PoisonAttack"){
            GlobalVariables.instance.poisonAttack = newValue;
            Debug.LogError(" Update poisonAttack  : " + GlobalVariables.instance.poisonAttack);
        }else if(name == "ExplosaoArcana"){
            GlobalVariables.instance.explosaoArcana = newValue;
            Debug.LogError(" Update explosaoArcana  : " + GlobalVariables.instance.explosaoArcana);
        }else if(name == "LifeOrDeath"){
            GlobalVariables.instance.lifeOrDeath = newValue;
            Debug.LogError(" Update lifeOrDeath  : " + GlobalVariables.instance.lifeOrDeath);
        }else if(name == "InvocacaoProfana"){
            GlobalVariables.instance.invocacaoProfana = newValue;
            Debug.LogError(" Update invocacaoProfana  : " + GlobalVariables.instance.invocacaoProfana);
        }
    }

    public void buyAttribute(int id)
    {
        Item storeItem = storeItens[id];
        int itemPrice = storeItem.price;
        string itemName  = storeItem.name;

        Debug.LogError(" > Item : " + itemName + " =====");

        if(GlobalVariables.instance.coins >= itemPrice){ 
            
            // Sempre aumentamos de 1 em 1 unidade?
            storeItem.actualValue = storeItem.actualValue + calculateGain(id); 
            updateGlobalVariablevalue(itemName, storeItem.actualValue);
            
            // Atualiza moedas
            GlobalVariables.instance.coins -= itemPrice;
            CoinsText.text = "Coins : " +  GlobalVariables.instance.coins.ToString();
        }else{
            // TODO : Animação de "compra bloqueada"  
            Debug.LogError(" [TODO] Não é possível adquirir o item : " + itemName);
        }
    }

    public void buyNewATtack(int id)
    {
        Item storeItem = storeItens[id];
        int itemPrice = storeItem.price;
        string itemName  = storeItem.name;

        Debug.LogError(" > Novo Ataque : " + itemName + " =====");

        if(GlobalVariables.instance.lastPlayerLevel >= itemPrice){ 
           Debug.LogError(" [TODO] Desbloquear novo ataque  " + itemName);

            // Em Global variables o número 1 significa que o ataque foi desbloqueado 
            // e o numero 0 significa que não foi desbloqueado
            storeItem.actualValue = 1.0;
            updateGlobalVariablevalue(itemName, storeItem.actualValue);
        }else{
            //TODO : Animação de "compra bloqueada"  
           Debug.LogError(" [TODO] Não é possível adquirir o ataque : " + itemName);
        }

    }

    public void PressBackToHome()
    {   
        // Vai para a sala de espera
        SceneManager.LoadScene(1); 
    }
}
