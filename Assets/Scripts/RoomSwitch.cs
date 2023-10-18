using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomSwitch : MonoBehaviour
{
    public Canvas canvas;
    private Animator animator;
    public Image icon1;
    public Image icon2;
    public Image icon3;

    public Sprite[] sprites;

    void Start()
    {
        canvas.gameObject.SetActive(false);
        animator = canvas.GetComponent<Animator>();

        if (sprites.Length >= 3)
        {
            icon1.sprite = sprites[0];
            icon2.sprite = sprites[1];
            icon3.sprite = sprites[2];
        }
        else
        {
            Debug.LogWarning("Insira pelo menos 3 sprites na matriz 'sprites'");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("Switch");
        }
    }
}
