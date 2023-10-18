using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    public Image icon;
    public Sprite[] sprites;
    public Animator animator;

    private int position = 0;
    private bool buttonRight = false;

    void Start()
    {
        icon.sprite = sprites[position];
        animator = GetComponent<Animator>();
    }

    public void SwitchAnimation() {
        animator.SetTrigger("Switch");
    }

    public void SetButtonRight() {
        buttonRight = true;
    }

    public void switchIcon()
    {
        if (buttonRight) {
            position++;
            if (position >= sprites.Length)
            {
                position = 0;
            }
            icon.sprite = sprites[position];
        } else {
            position--;
            if (position < 0)
            {
                position = sprites.Length - 1;
            }
            icon.sprite = sprites[position];
        }
        buttonRight = false;
    }

    public void ResetPosition()
    {
        icon.rectTransform.localScale = Vector3.one; // Define a escala para o valor padrão (1,1,1)
        icon.rectTransform.localRotation = Quaternion.identity; // Define a rotação para a padrão (0,0,0)
    }
}
