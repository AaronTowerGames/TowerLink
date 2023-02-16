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
        _levelData = obj;
        
        EventBus.OnSetLevelEnd.Invoke();
    }

    private void Win()
    {
        Debug.Log("WIN");
    }

    private void Lose()
    {
        Debug.Log("LOSE");
    }
}
