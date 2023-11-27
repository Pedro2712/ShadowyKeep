using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{   
    public GameObject pauseMenu;
    public GameObject playerInfos;
    public GameObject playerControllers;

    public GameObject pausedButton;


    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!isPaused)
            {
                isPaused = true;
                Pause();
            }
            else {
                isPaused = false;
                Resume();
            }
        }
    }

    public void Pause(){
        disableControlsAndInfos();

        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume(){
        enableControlsAndInfos();

        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void backToMenu(){
        SceneManager.LoadScene(0);
    }

    private void disableControlsAndInfos()
    {
        playerInfos.SetActive(false);
        playerControllers.SetActive(false);
        pausedButton.SetActive(false);
    }

    private void enableControlsAndInfos()
    {
        playerInfos.SetActive(true);
        playerControllers.SetActive(true);
        pausedButton.SetActive(true);
    }
}
