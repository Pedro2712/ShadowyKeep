using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManagerMusic : MonoBehaviour
{
    public AudioSource srcSFX;
    public AudioSource srcBackground;

    public AudioClip buy, denied, background;

    private void Start()
    {
        srcBackground.clip = background;
        srcBackground.Play();
    }
    public void buySound(){
        srcSFX.clip = buy;
        srcSFX.Play();
    }

    public void deniedSound(){
        srcSFX.clip = denied;
        srcSFX.Play();
    }
}
