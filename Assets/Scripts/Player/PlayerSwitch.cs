using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSwitch : MonoBehaviour
{
    [Header("Player 1")]
    public PlayerMovement playerController;
    public PlayerInput playerInput;
    public CinemachineVirtualCamera player1Camera;


    [Header("Player2")]
    public PlayerMovement player2Controller;
    public PlayerInput player2Input;
    public CinemachineVirtualCamera player2Camera;

    public bool player1Active = true;

    private void Start()
    {
        // Certifique-se de que apenas um dos players esteja ativo no início
        if (player1Active)
        {
            EnablePlayer1();
        }
        else
        {
            EnablePlayer2();
        }
    }

    private void Update()
    {
        // Verifique o input do novo sistema de Input
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            SwitchPlayer();
        }
    }

    public void SwitchPlayer()
    {
        if (player1Active)
        {
            EnablePlayer2();
        }
        else
        {
            EnablePlayer1();
        }
    }

    private void EnablePlayer1()
    {
        player1Active = true;

        player2Controller.enabled = false;
        player2Input.enabled = false;
        player2Camera.gameObject.SetActive(false);

        playerController.enabled = true;
        playerInput.enabled = true;
        player1Camera.gameObject.SetActive(true);

    }

    private void EnablePlayer2()
    {
        player1Active = false;

        playerController.enabled = false;
        playerInput.enabled = false;
        player1Camera.gameObject.SetActive(false);

        player2Controller.enabled = true;
        player2Input.enabled = true;
        player2Camera.gameObject.SetActive(true);
    }
}
