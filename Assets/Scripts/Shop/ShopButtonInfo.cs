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

        PriceText.text = "Price : " + itemPrice.ToString();
        ActualValue.text = itemActualValue.ToString();
    }

}
