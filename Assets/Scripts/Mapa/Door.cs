using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public Canvas canvas;
    public Canvas KeyF;
    public Switch switchScript;
    public Button button;

    public LayerMask playerLayer;
    public float radius;
    private bool onRadios;

    private int NumberOfRooms = 5;
    private bool isClicked = false;
    
    void Start()
    {
        canvas.gameObject.SetActive(false);
    }

    public void ButtonClicado () {
        isClicked = true;
    }

    public void Update () {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

        onRadios = hit != null;

        canvas.gameObject.SetActive(onRadios);
        KeyF.gameObject.SetActive(onRadios);


        if (onRadios) {
            if(isClicked){

                if (GlobalVariables.instance.roomsVisited == GlobalVariables.instance.totalRooms) {
                    SceneManager.LoadScene("SalaBoss");

                } else {
                    
                    int randomIndex = Random.Range(0, NumberOfRooms);

                    // Garante que a sala escolhida não seja a mesma que a última visitada
                    while (randomIndex == GlobalVariables.instance.lastVisitedIndex) {
                        randomIndex = Random.Range(0, NumberOfRooms);

                    }
                    GlobalVariables.instance.lastVisitedIndex = randomIndex;
                    GlobalVariables.instance.roomsVisited = GlobalVariables.instance.roomsVisited + 1;
                    switchScript.ChooseIcon();
                    
                    SceneManager.LoadScene("Sala" + (randomIndex + 1));
                
            }
            }
        }else{
            isClicked = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
