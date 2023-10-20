using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAtributo : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        animator.SetBool("isOpen", true);
    }

    public void Close()
    {
        animator.SetBool("isOpen", false);
    }
}
