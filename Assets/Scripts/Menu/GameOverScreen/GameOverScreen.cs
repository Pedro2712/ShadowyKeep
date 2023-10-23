using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public float volume = 1.0f;
    [SerializeField] private AudioClip _musicLose;
    [SerializeField] private AudioSource _source;

    // Start is called before the first frame update
    void Start()
    {
        // Defina a m�sica e configure o loop.
        _source.clip = _musicLose;
        _source.loop = true;
        _source.volume = volume;
        _source.Play();
    }

    public void backToHome () 
    {
        Debug.LogFormat("Voltar para o home");
        SceneManager.LoadScene(1);   
    }
}
