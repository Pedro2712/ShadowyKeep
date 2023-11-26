using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDialogueTrigger : MonoBehaviour
{
    public SelectedItem item;
    private ShopDialogueManager dialogueManager;
    private ShopManager manager;

    void Start(){
        dialogueManager = FindObjectOfType<ShopDialogueManager>();
        manager = FindObjectOfType<ShopManager>();
    }

    public void TriggerDialogue()
    {   
        dialogueManager.enableDialogue();
        dialogueManager.StartDialogue(item);
    }

    public void Cancel()
    {   
        dialogueManager.disableDialogue();
    }

    public void buyItem()
    {   
        manager.Buy(item);
        dialogueManager.disableDialogue();
    }
}
