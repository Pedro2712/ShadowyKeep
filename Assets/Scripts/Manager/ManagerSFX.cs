using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSFX : MonoBehaviour
{
    public AudioSource srcPlayer;
    public AudioSource srcMonster;
    public AudioSource srcBackground;

    public AudioClip walk, sword, cocadaemon, levelUp;
    
    public List<AudioClip> backgroundMusics;

    public void walkSound(){
        srcPlayer.loop = true;
        srcPlayer.clip = walk;
        srcPlayer.Play();
    }

    public void stopWalkSound(){
        srcPlayer.clip = walk;
        srcPlayer.Stop();
    }

    public void swordSound(){
        srcPlayer.loop = false;
        srcPlayer.clip = sword;
        srcPlayer.Play();
    }

    public void LevelUp()
    {
        srcPlayer.loop = false;
        srcPlayer.clip = levelUp;
        srcPlayer.Play();
    }

    public void cocadaemonSound(){
        srcMonster.clip = cocadaemon;
        srcMonster.Play();
    }

    public void stopMonsterSound(){
        srcMonster.clip = cocadaemon;
        srcMonster.Stop();
    }

    public void backgroundSound(bool bossBattle){
        
        int sound_idx = Random.Range(1, backgroundMusics.Count);
        if(bossBattle){
            sound_idx = 0;
        }

        srcBackground.clip = backgroundMusics[sound_idx];
        srcBackground.Play();
    }
}
