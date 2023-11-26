using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopDialogueManager : MonoBehaviour
{   
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;
    private ShopManager manager;

    void Start()
    {
        manager = FindObjectOfType<ShopManager>();
    }

    public void enableDialogue()
    {
        dialogueBox.SetActive(true);
    }

    public void disableDialogue()
    {
        dialogueBox.SetActive(false);
    }

    public void StartDialogue(SelectedItem item)
    {
        Debug.LogWarning("Start conversation");

        string itemName = manager.getName(item.id); 
        int itemPrice = manager.getPrice(item.id);

        Debug.LogWarning("Item Name : " + itemName);
        Debug.LogWarning("Item Price : " + itemPrice);

        dialogueText.text = "Você gostaria de adquirir +" + itemName +  " por " + itemPrice + " modeas ?";
    }
    
}
