using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopDialogueManager : MonoBehaviour
{   
    [Header(" Attribute Dialogue ")]
    public TextMeshProUGUI attributeDialogueText;
    public GameObject attributeDialogueBox;

    [Header("Shop Manager")]
    private ShopManager manager;

    private void Start()
    {
        manager = FindObjectOfType<ShopManager>();
    }

    public void EnableDialogue()
    {
        attributeDialogueBox.SetActive(true);
    }

    public void DisableDialogue()
    {
        attributeDialogueBox.SetActive(false);
    }

    public void StartDialogue(SelectedItem item)
    {
        // Debug.LogWarning("--- Start conversation ---");

        string itemName = manager.getName(item.id); 
        string typeOfItem = manager.getTypeOfItem(item.id);
        int itemPrice = manager.getPrice(item.id);
        double gain = manager.calculateGain(item.id);

        if(typeOfItem == "attribute"){
            attributeDialogueText.text = "Você gostaria de adquirir +" + gain + " "+  itemName +  " por " + itemPrice + " modeas ?";
        }else{
            attributeDialogueText.text = "Desbloquear novo ataque " + itemName + " ?";
        }
    }

    public void BuyItem()
    {   
        int itemSelected = GlobalVariables.instance.selectedItemId;
        string typeOfItem = manager.getTypeOfItem(itemSelected);

        if(typeOfItem == "attribute"){
            manager.buyAttribute(itemSelected);
        }else{
            manager.buyNewAttack(itemSelected);
        }

        DisableDialogue();
    }
    
}
