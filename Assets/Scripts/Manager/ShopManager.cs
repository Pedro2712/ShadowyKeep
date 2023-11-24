using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

using static GlobalVariables;

public class ShopManager : MonoBehaviour
{

    public List<int> itensListIDs;
    public List<int> itensPriceIDs;
    public List<int> itensValue;

    public TextMeshProUGUI CoinsText;
    public int initCoins = 1000;
    
    void Start()
    {   
        CoinsText.text = "Coins : " + initCoins.ToString(); //GlobalVariables.instance.coins.ToString();

        // IDs
        itensListIDs.Add(1); // ID dos itens
        itensListIDs.Add(2);
        itensListIDs.Add(3);
        itensListIDs.Add(4);
        // shopItems[1,1] = 1;
        // shopItems[1,2] = 2;
        // shopItems[1,3] = 3;
        // shopItems[1,4] = 4;

        // Price
        itensPriceIDs.Add(10);    // Preco do item 1
        itensPriceIDs.Add(20);    // Preco do item 2
        itensPriceIDs.Add(30);  
        itensPriceIDs.Add(40);  

        // Quantidade de itens 
        itensValue.Add(0);    // Valor do item 1
        itensValue.Add(0);    // Valor do item 2
        itensValue.Add(0);  
        itensValue.Add(0); 

        // shopItems[3,1] = 0;         
        // shopItems[3,2] = 0;      
        // shopItems[3,3] = 0;       
        // shopItems[3,4] = 0;

        // Actual Value / Quantity:
        // shopItems[3,1] = player.entity.speed;         // Velocidade
        // shopItems[3,2] = player.entity.strength;      // Força
        // shopItems[3,3] = player.entity.defense;       // Defesa e Resistencia
        // shopItems[3,4] = player.entity.currentHealth; // Vida 
        //                                               // Mana 
        //                                               // MinDamage
        //                                               // Sorte
        //                                               // Inteligencia
    }

    
    public void Buy()
    {
        // Referencia ao botão
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        // Verificando se eu tenho moedas suficiente para comprar
        int itemId = ButtonRef.GetComponent<ShopButtonInfo>().ItemID;
        int itemPrice = itensPriceIDs[itemId];

        //if(GlobalVariables.instance.coins >= itemPrice){
        if(initCoins >= itemPrice){    
            //GlobalVariables.instance.coins -= itemPrice;
            initCoins -= itemPrice;

            // TODO : Update actualValue
            itensValue[itemId]++;

            // Atualiza moedas
            CoinsText.text = "Coins : " +  initCoins.ToString();//GlobalVariables.instance.coins.ToString();

            // TODO : Atualiza o actualValue no texto do Botão
            ButtonRef.GetComponent<ShopButtonInfo>().ActualValue.text = itensValue[itemId].ToString();
        }

        // TODO: Talvez adicionar um else com uma animação indicando que a compra nao pode ocorrer ?

    }
}
