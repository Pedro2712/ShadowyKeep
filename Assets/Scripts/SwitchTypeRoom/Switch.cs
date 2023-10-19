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
    private int iconNumber;
    private List<Sprite> sortSprites = new List<Sprite>();

    void Start()
    {
        icon.sprite = sprites[position];
        animator = GetComponent <Animator>();
        iconNumber = Random.Range(2, sprites.Length + 1);
        SortSprites();
    }

    public void SwitchAnimation()
    {
        animator.SetTrigger("Switch");
    }

    public void SetButtonRight()
    {
        buttonRight = true;
    }

    public void switchIcon()
    {
        if (buttonRight)
        {
            position++;
            if (position >= sortSprites.Count)
            {
                position = 0;
            }
            icon.sprite = sortSprites[position];
        }
        else
        {
            position--;
            if (position < 0)
            {
                position = sortSprites.Count - 1;
            }
            icon.sprite = sortSprites[position];
        }
        buttonRight = false;
    }

    private void SortSprites()
    {
        for (int i = 0; i < iconNumber; i++)
        {
            int randomNumber;
            do
            {
                randomNumber = Random.Range(0, sprites.Length);
            } while (sortSprites.Contains(sprites[randomNumber]));

            sortSprites.Add(sprites[randomNumber]);
        }
        icon.sprite = sortSprites[0];
    }
}