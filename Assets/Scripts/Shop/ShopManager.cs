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

    private string buyMode = "coin";
    private int valueOwned;

    [Header("Store")]
    private Dictionary<int, Item> storeItens = new Dictionary<int, Item>();

    public List<string> attributesNames = new List<string>(
        new string[] {"Speed", "Strength", "Defense", "Lucky", "Stamina", "Mana", "Life"}
    );
    public List<string> attackNames = new List<string>(
        new string[] {"PoisonAttack", "ExplosaoArcana", "LifeOrDeath", "InvocacaoProfana"}
    );

    public List<int> itensPrice = new List<int>(
        new int[] {35, 40, 40, 30, 450, 400, 500, 15, 40, 50, 60}
    );
    
    [Header("Text Infos")]
    public TextMeshProUGUI CoinsText;
    
    void Start()
    {
        CoinsText.text = "Coins : " + GlobalVariables.instance.coins.ToString();
    }

    public void createItem(int id)
    {   
        Item newItem = new Item{name = attributesNames[id], 
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

    public void Buy()
    {
        int id = GlobalVariables.instance.selectedItemId;

        Item storeItem = storeItens[id];
        int itemPrice = storeItem.price;
        string itemName  = storeItem.name;

        if (attributesNames.Contains(itemName))
        {        
            buyMode = "coins";
            valueOwned = GlobalVariables.instance.coins;
        }else if (attackNames.Contains(itemName)){
            buyMode = "level";
            valueOwned = GlobalVariables.instance.lastPlayerLevel;
        }
       
        if(valueOwned >= itemPrice){    
            
            if(buyMode == "coins"){
                valueOwned -= itemPrice;
                storeItem.actualValue = storeItem.actualValue + 1;
                GlobalVariables.instance.coins -= itemPrice;
            }

            // Atualiza moedas
            CoinsText.text = "Coins : " +  GlobalVariables.instance.coins.ToString();

        }

    }

    public void PressBackToHome()
    {   
        // Vai para a sala de espera
        SceneManager.LoadScene(1); 
    }
}
