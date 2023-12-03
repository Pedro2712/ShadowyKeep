using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{
    public void GotToGameOverScreen(){
        //Time.timeScale = 1f;
        SceneManager.LoadScene("GameOver");
    }
}
