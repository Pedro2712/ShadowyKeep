using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GlobalVariables;
public class InvocacaoProfana : MonoBehaviour
{
    public GameObject attack;
    // Start is called before the first frame update
    void Start()
    {
     if (GlobalVariables.instance.invocacaoProfana == 1)
        {
            attack.SetActive(true);
        }
        else
        {
            attack.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
