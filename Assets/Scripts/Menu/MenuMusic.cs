using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    public float volume = 1.0f;

    [SerializeField] private AudioClip _musicMenu;
    [SerializeField] private AudioSource _source;

    // Start is called before the first frame update
    void Start()
    {
        // Defina a música e configure o loop.
        _source.clip = _musicMenu;
        _source.loop = true;
        _source.volume = volume;

        // Inicie a reprodução.
        _source.Play();
    }
}
