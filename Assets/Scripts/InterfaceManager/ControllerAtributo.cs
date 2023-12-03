using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using static GlobalVariables;
public class ControllerAtributo : MonoBehaviour
{
    public Player player;
    public Animator animator;
    public TextMeshProUGUI strength;
    public TextMeshProUGUI defense;
    public TextMeshProUGUI speed;
    void Start()
    {
        animator= GetComponent<Animator>();
    }

    public void Open() {
        animator.SetBool("isOpen", true);
        int playerStrength = player.entity.strength + GlobalVariables.instance.boughtStrength + GlobalVariables.instance.tempStrength;
        int playerDefense = player.entity.defense + GlobalVariables.instance.boughtDefense + GlobalVariables.instance.tempDefense;
        float playerSpeed = player.entity.speed + GlobalVariables.instance.boughtSpeed + GlobalVariables.instance.tempSpeed;
        strength.text = playerStrength.ToString();
        defense.text = playerDefense.ToString();
        speed.text = playerSpeed.ToString("F1");
        
    }

    public void Close() {
        animator.SetBool("isOpen", false);
    }
}
