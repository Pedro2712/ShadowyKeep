using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{   
    public GameObject pauseMenu;

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
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void backToMenu(){
        SceneManager.LoadScene(0);
    }
}
