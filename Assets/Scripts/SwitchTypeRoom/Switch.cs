using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    [System.Serializable]
    public class SpriteOption
    {
        public string name;
        public Sprite sprite;
    }
    public Image icon;
    public SpriteOption[] sprites;
    public Animator animator;
    public static int FinalChoose = 0;

    private int position = 0;
    private bool buttonRight = false;
    private int iconNumber = 3;
    private List<SpriteOption> sortSprites = new List<SpriteOption>();
    private List<int> numberPick = new List<int>();

    void Start()
    {
        icon.sprite = sprites[position].sprite;
        animator = GetComponent<Animator>();
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
            icon.sprite = sortSprites[position].sprite;
        }
        else
        {
            position--;
            if (position < 0)
            {
                position = sortSprites.Count - 1;
            }
            icon.sprite = sortSprites[position].sprite;
        }
        buttonRight = false;
    }

    public void ChooseIcon()
    {
        print(numberPick[position]);
        FinalChoose = numberPick[position];
        GlobalVariables.instance.finalChoose = numberPick[position];
    }

    private void SortSprites()
    {
        for (int i = 0; i < iconNumber; i++)
        {
            int randomNumber;
            do
            {
                randomNumber = Random.Range(0, sprites.Length);
            } while (numberPick.Contains(randomNumber));
            numberPick.Add(randomNumber);
            sortSprites.Add(sprites[randomNumber]);
        }
        icon.sprite = sortSprites[0].sprite;
    }
}
