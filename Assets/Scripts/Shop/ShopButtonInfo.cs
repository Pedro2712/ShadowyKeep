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

    public GameObject ShopManager;

    void Start()
    {
        PriceText.text = "Price : $" + ShopManager.GetComponent<ShopManager>().itensPriceIDs[ItemID].ToString();
        ActualValue.text = ShopManager.GetComponent<ShopManager>().itensValue[ItemID].ToString();
    }
}
