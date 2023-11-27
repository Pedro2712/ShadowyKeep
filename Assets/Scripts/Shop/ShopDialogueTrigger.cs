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

        // Varivel que controla qual item foi selecionado
        GlobalVariables.instance.selectedItemId = item.id;
    }

    public void EndDialogue()
    {   
        dialogueManager.disableDialogue();
    }

}
