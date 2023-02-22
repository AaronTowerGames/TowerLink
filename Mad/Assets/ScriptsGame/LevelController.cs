using System;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private LevelData _levelData = new LevelData();

    private void OnEnable()
    {
        EventBus.OnSetLevel.Subscribe(Set);
        EventBus.EndLevelTime.Subscribe(Lose);
        EventBus.LevelWin.Subscribe(Win);
        EventBus.LevelLose.Subscribe(Lose);

        EventBus.OnSetHeroEquipment.Subscribe(SetPositionHero);
    }

    private void OnDestroy()
    {
        EventBus.OnSetLevel.Unsubscribe(Set);
        EventBus.EndLevelTime.Unsubscribe(Lose);
        EventBus.LevelWin.Unsubscribe(Win);
        EventBus.LevelLose.Unsubscribe(Lose);
        
        EventBus.OnSetHeroEquipment.Unsubscribe(SetPositionHero);
    }

    private void SetPositionHero()
    {
        EventBus.CalcHeroPositions.Invoke(_levelData._countPoss);
    }

    private void Set(LevelData obj)
    {
        _levelData = new LevelData
        {
            idEnemyLevelPool= obj.idEnemyLevelPool,
            idLocationData=obj.idLocationData,
            level=obj.level,
            roundTimeSeconds=obj.roundTimeSeconds,
            startEnemyCount=obj.startEnemyCount,
            startMaxEnemyCount=obj.startMaxEnemyCount,
            _countLocation = obj._countLocation,
            _countPoss = obj._countPoss
        };
        
        EventBus.OnSetLevelEnd.Invoke();
    }

    private void Win()
    {
        Debug.Log("WIN");
        EventBus.Show.Invoke("PauseCanvas");
    }

    private void Lose()
    {
        Debug.Log("LOSE");
        EventBus.Show.Invoke("PauseCanvas");
    }
}
