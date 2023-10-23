using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        strength.text = player.entity.strength.ToString();
        defense.text = player.entity.defense.ToString();
        speed.text = player.entity.speed.ToString("F1");
        
    }

    public void Close() {
        animator.SetBool("isOpen", false);
    }
}
