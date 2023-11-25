using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

using static GlobalVariables;

public class ShopManager : MonoBehaviour
{

    // --- IDs ---
    // 0 a 6  - atributos (Price em moedas)
    // 7 a 10 - ataques   (Price em level)

    List<int> itensListIDs = new List<int>(new int[] {0,1,2,3,4,5,6,7,8,9,10});
    List<int> itensPriceIDs = new List<int>(new int[] {100,150,150,80,450,400,350,15,30,50,70});
    public List<double> itensValue;

    private int limiar = 6;
    private bool buyMode = "coin";
    private int valueOwned;

    public TextMeshProUGUI CoinsText;
    
    void Start()
    {      
        
        CoinsText.text = "Coins : " + GlobalVariables.instance.coins.ToString();

        // Atributos
        itensValue.Add( GlobalVariables.instance.speed)     // Velocidade
        itensValue.Add( GlobalVariables.instance.strength)  // Força
        itensValue.Add( GlobalVariables.instance.defense)   // Defesa
        itensValue.Add( GlobalVariables.instance.lucky)     // Sorte

        itensValue.Add( GlobalVariables.instance.stamina)   //Stamina
        itensValue.Add( GlobalVariables.instance.mana)      //Mana
        itensValue.Add( GlobalVariables.instance.life)      // Vida

        // Ataques
        itensValue.Add( GlobalVariables.instance.poison)           // poison
        itensValue.Add( GlobalVariables.instance.explosaoArcana)   // explosao arcana
        itensValue.Add( GlobalVariables.instance.lifeOrDeath)      // Life or death
        itensValue.Add( GlobalVariables.instance.invocacaoProfana) // Invocação profana
        
    }

    
    public void Buy()
    {
        // Referencia ao botão
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        // Verificando se eu tenho moedas suficiente para comprar
        int itemId = ButtonRef.GetComponent<ShopButtonInfo>().ItemID;
        int itemPrice = itensPriceIDs[itemId];

        // Se o item é comprado com moeda ou com level
        if(itemId > limiar){
            // Ataques
            buyMode = "attack";
            valueOwned = GlobalVariables.instance.lastPlayerLevel;
        }else{
            //Moedas
            buyMode = "attribute";
            valueOwned = initCoins;
        }

        if(valueOwned >= itemPrice){    
            GlobalVariables.instance.coins -= itemPrice;
            
            if(buyMode == "attribute"){
                initCoins -= itemPrice;
                itensValue[itemId]++;
            }

            // Atualiza moedas
            CoinsText.text = "Coins : " +  GlobalVariables.instance.coins.ToString();

            // TODO : Atualiza o actualValue no texto do Botão
            ButtonRef.GetComponent<ShopButtonInfo>().ActualValue.text = itensValue[itemId].ToString();
        }

        // TODO: Talvez adicionar um else com uma animação indicando que a compra nao pode ocorrer ?

    }

    public void PressBackToHome()
    {   
        // Vai para a sala de espera
        SceneManager.LoadScene(1); 
    }
}
