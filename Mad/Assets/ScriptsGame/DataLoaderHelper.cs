using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoaderHelper : MonoBehaviour
{
    private bool onSetHeroEnd = false;
    private bool onSetLevelEnd = false;
    private bool onSetEnemyCountEnd = false;
    private bool onSetTimerEnd = false;
    private bool onSetEnemyInSpawnEnd = false;
    private bool onCreateHeroEnd = false;
    private bool onSetHeroSpawnSetEnd = false;

    private void OnEnable()
    {
        EventBus.OnSetHeroEnd.Subscribe(OnSetHeroEnd);
        EventBus.OnSetLevelEnd.Subscribe(OnSetLevelEnd);

        EventBus.OnSetEnemyCountEnd.Subscribe(OnSetEnemyCountEnd);
        EventBus.OnSetTimerEnd.Subscribe(OnSetTimerEnd);
        EventBus.OnSetHeroSpawnSetEnd.Subscribe(OnSetHeroSpawnSetEnd);
        
        EventBus.OnSetEnemyInSpawnEnd.Subscribe(OnSetEnemyInSpawnEnd);
        EventBus.OnCreateHeroEnd.Subscribe(OnCreateHeroEnd);
    }

    private void OnSetHeroSpawnSetEnd()
    {
        onSetHeroSpawnSetEnd = true;
        CheckStage1LevelLoadingEnd();
    }

    private void OnSetEnemyCountEnd()
    {
        onSetEnemyCountEnd=true;
        CheckStage1LevelLoadingEnd();
    }

    private void OnSetTimerEnd()
    {
        onSetTimerEnd =true;
        CheckStage1LevelLoadingEnd();
    }

    private void OnSetLevelEnd()
    {
        onSetLevelEnd = true;
        CheckStage1LevelLoadingEnd();
    }

    private void OnSetHeroEnd()
    {
        onSetHeroEnd = true;
        CheckStage1LevelLoadingEnd();
    }

    private void CheckStage1LevelLoadingEnd()
    {
        if (onSetHeroEnd && onSetLevelEnd && onSetTimerEnd && onSetEnemyCountEnd && onSetHeroSpawnSetEnd)
        {
            EventBus.CheckStage1LevelLoadingEnd.Invoke();
        }
    }

    private void OnSetEnemyInSpawnEnd()
    {
        onSetEnemyInSpawnEnd = true;
        CheckStage2LevelLoadingEnd();
    }

    private void OnCreateHeroEnd()
    {
        onCreateHeroEnd = true;
        CheckStage2LevelLoadingEnd();
    }


    private void CheckStage2LevelLoadingEnd()
    {
        if (onSetEnemyInSpawnEnd && onCreateHeroEnd)
        {
            EventBus.CheckStage2LevelLoadingEnd.Invoke();
        }
    }
}
