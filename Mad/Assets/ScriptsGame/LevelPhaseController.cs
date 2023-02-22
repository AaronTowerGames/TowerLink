using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPhaseController : MonoBehaviour
{
    [SerializeField]
    private LevelPhaseData _data;

    private void OnEnable()
    {
        EventBus.OnSetLevel.Subscribe(SetPhaseEnemyCount);
    }

    private void SetPhaseEnemyCount(LevelData obj)
    {
        _data = new LevelPhaseData
        {
            maxEnemyInScene = obj.startMaxEnemyCount
        };

        EventBus.OnSetMaxEnemyInScene.Invoke(_data);
    }
}
