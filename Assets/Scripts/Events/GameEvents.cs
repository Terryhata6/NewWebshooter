using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Current;

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Current = this;        
    }

    #region GeneralEvents



    #endregion

    #region LevelEvents
    public Action OnLevelStart;
    public void LevelStart()
    {
        OnLevelStart?.Invoke();
    }

    public Action OnLevelEnd;
    public void LevelEnd()
    {
        OnLevelEnd?.Invoke();
    }

    public Action OnLevelComplete;
    public void LevelComplete()
    {
        OnLevelComplete?.Invoke();
    }

    public Action OnLevelFailed;
    public void LevelFailed()
    {
        OnLevelFailed?.Invoke();
    }

    #endregion
    #region SkinShop
    public Action<IGetReward> OnAskingRewardedVideo;
    public void AskingRewardedVideo(IGetReward reward)
    {
        OnAskingRewardedVideo?.Invoke(reward);
    }
    #endregion
}
