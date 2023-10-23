using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{   
    public GameObject pauseMenu;
    public void buttonPause(){
        Pause();
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
