using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public Canvas canvas;
    public Canvas KeyF;
    public Switch switchScript;

    public LayerMask playerLayer;
    public float radius;
    private bool onRadios;

    private int NumberOfRooms = 5;
    
    void Start()
    {
        canvas.gameObject.SetActive(false);
    }

    void Update() {

        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

        onRadios = hit != null;

        canvas.gameObject.SetActive(onRadios);
        KeyF.gameObject.SetActive(onRadios);


        if (onRadios) {

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (GlobalVariables.instance.roomsVisited == GlobalVariables.instance.totalRooms) {
                    SceneManager.LoadScene("SalaBoss");

                } else {
                    
                    int randomIndex = 1;//Random.Range(0, NumberOfRooms);

                    // Garante que a sala escolhida não seja a mesma que a última visitada
                    while (randomIndex == GlobalVariables.instance.lastVisitedIndex) {
                        randomIndex = Random.Range(0, NumberOfRooms);

                    }

                    GlobalVariables.instance.lastVisitedIndex = randomIndex;
                    GlobalVariables.instance.roomsVisited++;
                    switchScript.ChooseIcon();

                    SceneManager.LoadScene("Sala" + (randomIndex + 1));
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
