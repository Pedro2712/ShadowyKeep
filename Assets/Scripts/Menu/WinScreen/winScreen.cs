using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winScreen : MonoBehaviour
{   

    public float volume = 1.0f;

    [SerializeField] private AudioClip _musicWin;
    [SerializeField] private AudioSource _source;

    // Start is called before the first frame update
    void Start()
    {
        // Defina a m�sica e configure o loop.
        _source.clip = _musicWin;
        _source.loop = true;
        _source.volume = volume;
        _source.Play();
    }

    public void backToMenu()
    {
        Debug.LogFormat("Voltar para o menu");
        SceneManager.LoadScene(0);
    }
}
