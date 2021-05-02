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
    public Action<int> OnLevelStart;
    public void LevelStart(int levelNumber) //момент клика на кнопку старта
    {
        OnLevelStart?.Invoke(levelNumber);
    }

    public Action OnLevelEnd; //конец уровня без учета победы/поражения
    public void LevelEnd()
    {
        OnLevelEnd?.Invoke();
    }

    public Action OnLevelComplete; //момент включения победного ui
    public void LevelComplete()
    {
        OnLevelComplete?.Invoke();
    }

    public Action OnLevelFailed; //момент включения фейлового ui
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
