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
    
    [Header("Text Infos")]
    public TextMeshProUGUI CoinsText;
    
    
    void Start()
    {      
        CoinsText.text = "Coins : " + GlobalVariables.instance.coins.ToString();

        // Create Attributes Itens:
        Item speed = new Item{name = attributesNames[0], 
                              price = 35, 
                              actualValue = GlobalVariables.instance.speed, 
                              id = 0
                              };
        Item strength = new Item{name = attributesNames[1], 
                                 price = 40, 
                                 actualValue = GlobalVariables.instance.strength,  
                                 id = 1
                                 };
        Item defense = new Item{name = attributesNames[2], 
                                price = 40, 
                                actualValue = GlobalVariables.instance.defense, 
                                id = 2};
        Item lucky = new Item{name = attributesNames[3], 
                              price = 30, 
                              actualValue = GlobalVariables.instance.lucky,  
                              id = 3
                              };

        Item stamina = new Item{name = attributesNames[4], 
                                price = 450, 
                                actualValue = GlobalVariables.instance.stamina, 
                                id = 4
                                };
        Item mana = new Item{name = attributesNames[5], 
                             price = 400, 
                             actualValue = GlobalVariables.instance.mana, 
                             id = 5
                             };
        Item life = new Item{name = attributesNames[6], 
                             price = 500, 
                             actualValue = GlobalVariables.instance.life, 
                             id = 6
                            };

        // Create Attack Itens:
        Item poisonAttack = new Item{name = attackNames[0], 
                                     price = 15, 
                                     actualValue = GlobalVariables.instance.poisonAttack,  
                                     id = 7
                                     };
        Item explosaoArcana = new Item{name = attackNames[1], 
                                       price = 40, 
                                       actualValue = GlobalVariables.instance.explosaoArcana,  
                                       id = 8
                                       };
        Item lifeOrDeath = new Item{name = attackNames[2], 
                                    price = 50, 
                                    actualValue = GlobalVariables.instance.lifeOrDeath, 
                                    id = 9
                                    };
        Item invocacao = new Item{name = attackNames[3], 
                                  price = 60, 
                                  actualValue = GlobalVariables.instance.invocacaoProfana, 
                                  id = 10
                                  };

        // Adicionando itens na loja
        storeItens.Add(speed.id, speed);
        storeItens.Add(strength.id, strength);
        storeItens.Add(defense.id, defense);
        storeItens.Add(lucky.id, lucky);
        storeItens.Add(stamina.id, stamina);
        storeItens.Add(mana.id, mana);
        storeItens.Add(life.id, life);
        storeItens.Add(poisonAttack.id, poisonAttack);
        storeItens.Add(explosaoArcana.id, explosaoArcana);
        storeItens.Add(lifeOrDeath.id, lifeOrDeath);
        storeItens.Add(invocacao.id, invocacao);
        
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

    public void Buy(SelectedItem item)
    {
        
        int id = item.id;

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
