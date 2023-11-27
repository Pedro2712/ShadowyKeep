using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButtonInfo : MonoBehaviour
{
    public int ItemID;

    public TextMeshProUGUI PriceText;
    public TextMeshProUGUI ActualValue;

    private ShopManager manager;

    public void Start()
    {   
        manager = FindObjectOfType<ShopManager>();
        manager.createItem(ItemID);

        int itemPrice = manager.getPrice(ItemID);
        double itemActualValue = manager.getActualValue(ItemID);
        string typeOfItem = manager.getTypeOfItem(ItemID);

        if(typeOfItem == "attribute"){
            PriceText.text = "Price : " + itemPrice.ToString();
            ActualValue.text = itemActualValue.ToString();
        }else{
            PriceText.text = "Min Level : " + itemPrice.ToString();
            if(itemActualValue == 1){
                // Item desbloqueado
                ActualValue.text = " DISPONÍVEL ";
                ActualValue.color = Color.green;
            }else{
                // Item indisponível
                ActualValue.text = " INDISPONÍVEL ";
                ActualValue.color = Color.red;
            }
        }
    }

    // Ajeitar essa funçao para atualizar o valor do texto do valor atual
    void FixedUpdate() {        
        double itemActualValue = manager.getActualValue(ItemID);
        string typeOfItem = manager.getTypeOfItem(ItemID);
        
        if(typeOfItem == "attribute"){
            ActualValue.text = itemActualValue.ToString();
        }else{
            if(itemActualValue == 1){
                // Item desbloqueado
                ActualValue.text = " DISPONÍVEL ";
                ActualValue.color = Color.green;
            }
        }
    }

}
