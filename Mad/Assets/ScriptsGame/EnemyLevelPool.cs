using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevelPool : GSC<EnemyLevelPool>
{
    [SerializeField]
    private EnemyPool[] _pool = new EnemyPool[10]; 

    public EnemyPool Get(int id) 
    { 
        if (_pool != null)
        {
            if (id < _pool.Length)
            {
                if (_pool[id] != null)
                {
                    return _pool[id];
                }
            }
        }
        return null;
    }

    private void OnEnable()
    {
        EventBus.OnSetLevel.Subscribe(Set);
    }

    private void OnDestroy()
    {
        EventBus.OnSetLevel.Unsubscribe(Set);
    }
    private void Set(LevelData obj)
    {
        EventBus.SetEnemyPool.Invoke(Get(obj.idEnemyLevelPool));
    }
}
