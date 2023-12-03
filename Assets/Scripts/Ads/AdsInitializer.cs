using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
 
public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    [SerializeField] RewardedAdsButton rewardedAdsButton;
 
    public void startAds()
    {
        InitializeAds();
    }
 
    public void InitializeAds()
    {
        #if UNITY_IOS
                _gameId = _iOSGameId;
        #elif UNITY_ANDROID
                _gameId = _androidGameId;
        #elif UNITY_EDITOR
                _gameId = _androidGameId; //Only for testing the functionality in the Editor
        #endif

        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }

        if (Advertisement.isInitialized){
            Debug.LogWarning("Unity Ads initialization complete.");
            rewardedAdsButton.LoadAd();
        }else{
            Debug.LogWarning($"Unity Ads Initialization Failed.");
        }
    }

 
    public void OnInitializationComplete()
    {
        Debug.LogWarning("Unity Ads initialization complete.");
        rewardedAdsButton.LoadAd();
    }
 
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogWarning($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}