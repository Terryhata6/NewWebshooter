using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;
using Facebook.Unity;

public class SDKController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string _ISIOSAppKey = null;
    [SerializeField] private string _ISAndroidAppKey = null;
    [SerializeField] private string _currentAppKey = null;
    [SerializeField] private float _interstitialDelay = 40f;
    private bool _isISInitialised = false;
    private float _lastInterstitialTime;
    public static IGetReward RewardInstance = null;

    private void Start()
    {
        GameAnalyticsInitialize();
        FacebookInitialize();

        SubscribeInterstitialEvents();
        SubscribeRewardedEvents();
    }



    #region GameAnalytics
    private void GameAnalyticsInitialize()
    {
        GameAnalytics.Initialize();
    }

    private void OnLevelStartEvent()
    {
        /*
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start,
            $"Lvl:{_saveData.LoadInt(SaveKeyManager.LevelNumber)}",
            $"Diff: {_saveData.LoadInt(SaveKeyManager.Difficulty)}",
            $"Overall: {_saveData.LoadInt(SaveKeyManager.ComplitedLevelValue)}");*/
    }

    private void OnLevelFailEvent()
    {/*
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail,
                $"Lvl:{_saveData.LoadInt(SaveKeyManager.LevelNumber)}",
                $"Diff: {_saveData.LoadInt(SaveKeyManager.Difficulty)}",
                $"Overall: {_saveData.LoadInt(SaveKeyManager.ComplitedLevelValue)}");*/
    }

    private void OnLevelCompleteEvent()
    {/*
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete,
                $"Lvl:{_saveData.LoadInt(SaveKeyManager.LevelNumber)}",
                $"Diff: {_saveData.LoadInt(SaveKeyManager.Difficulty)}",
                $"Overall: {_saveData.LoadInt(SaveKeyManager.ComplitedLevelValue)}");*/
    }
    #endregion
    #region FacebookSDK
    private void FacebookInitialize()
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }
    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.LogWarning("Failed to Initialize the Facebook SDK");
        }
    }
    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }
    #endregion
    #region InitializeIronSourseSDK
    private void InitializeIronSource()//подписать этот метод чтобы узнать работает реклама или нет сейчас
    {
        if (!_isISInitialised)
        {
#if UNITY_ANDROID
            _currentAppKey = _ISAndroidAppKey;
#elif UNITY_IPHONE
                _currentAppKey = _ISIOSAppKey;
#else
                _currentAppKey = "unexpected_platform";
#endif

            IronSource.Agent.validateIntegration();
            IronSource.Agent.init(_currentAppKey, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.OFFERWALL, IronSourceAdUnits.BANNER);
            _isISInitialised = true;
        }
    }
    #endregion
    #region Interstitial
    private void StartInterstitialOnLevelEnding()
    {
        if (Time.time - _lastInterstitialTime > _interstitialDelay)
        {
            ShowInterstitial();
        }
    }

    private void LoadInterstitial()
    {
        IronSource.Agent.loadInterstitial();
    }

    private void ShowInterstitial()
    {
        if (IronSource.Agent.isInterstitialReady())
        {
            IronSource.Agent.showInterstitial();
            GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.Interstitial, "IronSource", "EndLevel");
            _lastInterstitialTime = Time.time;
        }
        else
        {
            LoadInterstitial();
        }
    }

    private void SubscribeInterstitialEvents()
    {
        IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
        IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
        IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;

        GameEvents.Current.OnLevelComplete += StartInterstitialOnLevelEnding;
    }

    private void InterstitialAdReadyEvent()
    {
        Debug.Log("unity-script: I got InterstitialAdReadyEvent");
    }

    private void InterstitialAdLoadFailedEvent(IronSourceError error)
    {
        Debug.Log("unity-script: I got InterstitialAdLoadFailedEvent, code: " + error.getCode() + ", description : " + error.getDescription());
    }

    private void InterstitialAdShowSucceededEvent()
    {
        Debug.Log("unity-script: I got InterstitialAdShowSucceededEvent");
    }

    private void InterstitialAdShowFailedEvent(IronSourceError error)
    {
        Debug.Log("unity-script: I got InterstitialAdShowFailedEvent, code :  " + error.getCode() + ", description : " + error.getDescription());
    }

    private void InterstitialAdClickedEvent()
    {
        Debug.Log("unity-script: I got InterstitialAdClickedEvent");
    }

    private void InterstitialAdOpenedEvent()
    {
        Debug.Log("unity-script: I got InterstitialAdOpenedEvent");
    }

    private void InterstitialAdClosedEvent()
    {
        Debug.Log("unity-script: I got InterstitialAdClosedEvent");
    }
    #endregion
    #region Rewarded
    private void SubscribeRewardedEvents()
    {
        IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
        IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
        IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
        IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
        IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
        IronSourceEvents.onRewardedVideoAdClickedEvent += RewardedVideoAdClickedEvent;
        
        GameEvents.Current.OnAskingRewardedVideo += AskingShowReward;
    }

    private void ShowRewardedVideo()
    {
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            IronSource.Agent.showRewardedVideo();
        }
    }

    public void AskingShowReward(IGetReward instance)
    {
        SetRewardInstance(instance);
        ShowRewardedVideo();
    }

    private void SetRewardInstance(IGetReward instance)
    {
        RewardInstance = instance;
    }

    private void ClearRewardInstance()
    {
        RewardInstance = null;
    }

    private void RewardedVideoAdOpenedEvent()
    {

    }
    //Invoked when the RewardedVideo ad view is about to be closed.
    //Your activity will now regain its focus.
    private void RewardedVideoAdClosedEvent()
    {

    }
    //Invoked when there is a change in the ad availability status.
    //@param - available - value will change to true when rewarded videos are available.
    //You can then show the video by calling showRewardedVideo().
    //Value will change to false when no videos are available.
    private void RewardedVideoAvailabilityChangedEvent(bool available)
    {
        //Change the in-app 'Traffic Driver' state according to availability.
    }

    //Invoked when the user completed the video and should be rewarded. 
    //If using server-to-server callbacks you may ignore this events and wait for 
    // the callback from the  ironSource server.
    //@param - placement - placement object which contains the reward data
    private void RewardedVideoAdRewardedEvent(IronSourcePlacement placement)
    {
        if (RewardInstance != null)
        {
            RewardInstance.RewardPlayer();
            GameAnalytics.NewAdEvent(GAAdAction.RewardReceived, GAAdType.RewardedVideo, "IronSource", $"{RewardInstance}");
        }
        else
        {
            RewardInstance = null;
        }
        ClearRewardInstance();
    }
    //Invoked when the Rewarded Video failed to show
    //@param description - string - contains information about the failure.
    private void RewardedVideoAdShowFailedEvent(IronSourceError error)
    {
        ClearRewardInstance();
    }

    // ----------------------------------------------------------------------------------------
    // Note: the events below are not available for all supported rewarded video ad networks. 
    // Check which events are available per ad network you choose to include in your build. 
    // We recommend only using events which register to ALL ad networks you include in your build. 
    // ----------------------------------------------------------------------------------------

    //Invoked when the video ad starts playing. 
    private void RewardedVideoAdStartedEvent()
    {

    }
    //Invoked when the video ad finishes playing. 
    private void RewardedVideoAdEndedEvent()
    {

    }

    private void RewardedVideoAdClickedEvent(IronSourcePlacement placement)
    {
        GameAnalytics.NewAdEvent(GAAdAction.Clicked, GAAdType.RewardedVideo, "IronSource", $"{RewardInstance}");
    }
    #endregion
}
