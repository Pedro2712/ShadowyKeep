using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    private Dictionary<string, Sprite> sortSprites = new Dictionary<string, Sprite>();

    public Image icon;
    public Animator animator;

    private int position = 0;
    private bool buttonRight = false;
    private int iconNumber = 3;
    List<string> keys = new List<string>();

    void Start()
    {
        animator = GetComponent<Animator>();
        SortSprites();
        icon.sprite = sortSprites.ElementAt(0).Value;
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
            if (position >= keys.Count)
            {
                position = 0;
            }
            icon.sprite = sortSprites[keys[position]];
        }
        else
        {
            position--;
            if (position < 0)
            {
                position = keys.Count - 1;
            }
            icon.sprite = sortSprites[keys[position]];
        }
        buttonRight = false;
    }

    public void ChooseIcon()
    {
        GlobalVariables.instance.finalChoose = keys[position];
    }

    private void SortSprites()
    {
        for (int i = 0; i < iconNumber; i++)
        {
            var keysList = new List<string>(GlobalVariables.instance.sprites.Keys);
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, keysList.Count);
            } while (keys.Contains(keysList[randomIndex]));
            keys.Add(keysList[randomIndex]);
            sortSprites.Add(keysList[randomIndex], GlobalVariables.instance.sprites[keysList[randomIndex]]);
        }
    }
}
