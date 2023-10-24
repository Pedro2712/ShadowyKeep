using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogueObj;
    public Image profile;
    public TextMeshProUGUI speechText;
    public TextMeshProUGUI actorNameText;

    [Header("Settings")]
    public float typingSpeed;
    private string[] setences;
    private int index;

    public bool isRunning;

    private void Start()
    {
        index= 0;
        speechText.text = "";
        isRunning = false;

        typingSpeed = 0.01f;
    }

    public void Speech(Sprite p, string[] txt, string actorName) {
        if (!isRunning) {
            isRunning = true;

            dialogueObj.SetActive(true);
            profile.sprite = p;
            setences = txt;
            actorNameText.text = actorName;
            StartCoroutine(TypeSetence());
        }
    }

    IEnumerator TypeSetence() {
        foreach (char letter in setences[index].ToCharArray()) {

            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSetence() {
        if (speechText.text == setences[index]) {
            // Ainda há textos
            if (index < setences.Length - 1) { 
                index++;
                speechText.text = "";
                StartCoroutine(TypeSetence());
            }
            else {
                speechText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                isRunning = false;
            }

        }
    }

    public bool getisRunning() {
        return isRunning;
    }
}
