using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private void Start()
    {
        EventBus.LoadAccauntData.Invoke();
        EventBus.LoadHeroDatas.Invoke();
        EventBus.LoadWeaponDatas.Invoke();
        EventBus.LoadLevelDatas.Invoke();
        EventBus.LoadEnemyDatas.Invoke();

        EventBus.StartDataLoadChecker.Invoke(DataLoadCheckerType.GameStart);
    }

    private void OnEnable()
    {
        EventBus.OnDatasLoaded.Subscribe(CheckToLoad);
        EventBus.CheckStage1LevelLoadingEnd.Subscribe(LoadStage2LevelLoading);
        EventBus.CheckStage2LevelLoadingEnd.Subscribe(LoadStage3LevelLoading);
    }

    private void OnDestroy()
    {
        EventBus.OnDatasLoaded.Unsubscribe(CheckToLoad);
        EventBus.CheckStage1LevelLoadingEnd.Unsubscribe(LoadStage2LevelLoading);
        EventBus.CheckStage2LevelLoadingEnd.Unsubscribe(LoadStage3LevelLoading);
    }

    private void LoadStage3LevelLoading()
    {
        EventBus.Close.Invoke("LoadingCanvas");
        EventBus.Show.Invoke("PauseCanvas");
    }

    private void LoadStage2LevelLoading()
    {
        
    }

    private void CheckToLoad(DataLoadCheckerType type)
    {
        if (type == DataLoadCheckerType.GameStart)
        {
            if (AccauntController.Instance.ChechIsLevelToLoad())
            {
                LoadLevel(AccauntController.Instance.GetLastLevel());
            }
            else
            {
                LoadHub();
            }
        }
    }

    private void LoadHub()
    {
        EventBus.LoadHub.Invoke();
    }

    private void LoadLevel(int level)
    {
        EventBus.SetLevel.Invoke(level);
        EventBus.LoadHero.Invoke();
        EventBus.Show.Invoke("LoadingCanvas");
    }
}
