using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
 
public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] Button _showAdButton;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    string _adUnitId = null; // This will remain null for unsupported platforms
    
    [Header("Player")]
    public Player player;
    public Transform respawnLocation;
    public Animator animator;

    [Header("AdsScreen")]
    public GameObject AdsScreen;

    void Awake()
    {   
        // Get the Ad Unit ID for the current platform:
        #if UNITY_IOS
                _adUnitId = _iOSAdUnitId;
        #elif UNITY_ANDROID
                _adUnitId = _androidAdUnitId;
        #endif

        // Disable the button until the ad is ready to show:
        _showAdButton.interactable = false;
    }
 
    // Call this public method when you want to get an ad ready to show.
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.LogWarning("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }
 
    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.LogWarning("Ad Loaded: " + adUnitId);
 
        if (adUnitId.Equals(_adUnitId))
        {
            // Configure the button to call the ShowAd() method when clicked:
            _showAdButton.onClick.AddListener(ShowAd);
            // Enable the button for users to click:
            _showAdButton.interactable = true;
        }
    }
 
    // Implement a method to execute when the user clicks the button:
    public void ShowAd()
    {
        // Disable the button:
        _showAdButton.interactable = false;
        // Then show the ad:
        Advertisement.Show(_adUnitId, this);
    }
 
    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.LogWarning("Unity Ads Rewarded Ad Completed");
            
            // Grant a reward.
            Debug.LogWarning("Back To Game");
            AdsScreen.SetActive(false);
            //Time.timeScale = 1f;
            
            player.entity.currentHealth = player.entity.maxHealth;
            player.entity.currentStamina = player.entity.maxStamina;
            
            // Atualizando Global Variables
            // GlobalVariables.instance.life = player.entity.maxHealth;    
            // GlobalVariables.instance.stamina = player.entity.maxStamina;

            // Manda o Player para a posição de respawn
            player.transform.position = respawnLocation.position;
            player.entity.dead = false;
            animator.SetBool("isDead", false);
            animator.SetTrigger("respawn");
            
        }
    }
 
    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.LogWarning($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }
 
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.LogWarning($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }
 
    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
 
    void OnDestroy()
    {
        // Clean up the button listeners:
        _showAdButton.onClick.RemoveAllListeners();
    }
}