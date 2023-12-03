using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivateAds : MonoBehaviour
{
    [Header("Ads Screen")]
    public GameObject AdsScreen;

    private bool respawnInThisRoomBefore;

    [Header("Player")]
    public Player player;

    [Header("Init Ads")]
    public AdsInitializer initAds;

    void Start()
    {   
        // Toda vez que isso recarregar estou em outra sala entao poderia reviver novamente.
        respawnInThisRoomBefore = false;
    }

    public void loadAds()
    {
        // Não tenho a possibilidade de reviver após morrer uma segunda vez na mesma sala.
        if(!respawnInThisRoomBefore){
            // Enable Ads Screen apenas se o player não já tiver respawnado antes
            // Pause Game
            // Carrega Ads:       
            respawnInThisRoomBefore = true;
            StartCoroutine(DelayedAdsScreen());
            AdsScreen.SetActive(true); 

            initAds.startAds();

            //Time.timeScale = 0f;

        } else if (respawnInThisRoomBefore){
            //Carrega GameOver direto
            SceneManager.LoadScene("GameOver");
        }
    }

    private IEnumerator DelayedAdsScreen(){
        yield return new WaitForSeconds(3f);  
    }
}
