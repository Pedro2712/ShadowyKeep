using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSFX : MonoBehaviour
{
    public AudioSource srcPlayer;
    public AudioSource srcMonster;
    public AudioSource srcBackground;

    public AudioClip walk, sword, cocadaemon, shadon, rats, levelUp, bossCleave, bossSmash, bossFireBreath, bossFireBall;
    
    private List<AudioClip> backgroundMusics;


    private void Start()
    {
        backgroundMusics = GlobalVariables.instance.backgroundMusics;

        backgroundSound(false);
    }
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

    public void shadonSound()
    {
        srcMonster.clip = shadon;
        srcMonster.Play();
    }

    public void ratsSound()
    {
        srcMonster.clip = rats;
        srcMonster.Play();
    }

    public void bossCleaveSound()
    {
        srcMonster.clip = bossCleave;
        srcMonster.Play();
    }

    public void bossSmashSound()
    {
        srcMonster.clip = bossSmash;
        srcMonster.Play();
    }

    public void bossFireBreathSound()
    {
        srcMonster.clip = bossFireBreath;
        srcMonster.Play();
    }

    public void bossFireBallSound()
    {
        srcMonster.clip = bossFireBall;
        srcMonster.Play();
    }

    public void stopMonsterSound(){
        srcMonster.clip = cocadaemon;
        srcMonster.Stop();
    }

    public void backgroundSound(bool bossBattle){
        
        int sound_idx = Random.Range(1, backgroundMusics.Count);
        if(bossBattle){
            Debug.Log("MUSICA DO BOSS");
            sound_idx = 0;
        }

        print(sound_idx);
        srcBackground.clip = backgroundMusics[sound_idx];
        srcBackground.Play();
    }
}
