using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();

    [SerializeField]
    private List<EnemyData> _enemiesToAdd = new List<EnemyData>();

    private void OnEnable()
    {
        EventBus.AddSpawnPoint.Subscribe(AddSpawnPoint);
        EventBus.OnSpawnPointDestroy.Subscribe(DestroySpawnPoint);
        EventBus.OnSpawnPointOpen.Subscribe(SpawnPointOpen);
        EventBus.OnSpawnPointClose.Subscribe(SpawnPointClose);
        EventBus.OnAddEnemy.Subscribe(AddEnemy);
    }

    private void OnDisable()
    {
        EventBus.AddSpawnPoint.Unsubscribe(AddSpawnPoint);
        EventBus.OnSpawnPointDestroy.Unsubscribe(DestroySpawnPoint);
        EventBus.OnSpawnPointOpen.Unsubscribe(SpawnPointOpen);
        EventBus.OnSpawnPointClose.Unsubscribe(SpawnPointClose);
        EventBus.OnAddEnemy.Unsubscribe(AddEnemy);
    }

    private void SpawnPointOpen(SpawnPoint spawn)
    {
        foreach (var item in _spawnPoints)
        {
            if (item == spawn)
            {
                item.OpenPoint();
                break;
            }
        }
    }

    private void SpawnPointClose(SpawnPoint spawn)
    {
        foreach (var item in _spawnPoints)
        {
            if (item == spawn)
            {
                item.ClosePoint();
                break;
            }
        }
    }

    private void DestroySpawnPoint(SpawnPoint spawn)
    {
        _spawnPoints.Remove(spawn);
    }

    private void AddSpawnPoint(SpawnPoint spawn)
    {
        _spawnPoints.Add(spawn);
    }

    private void AddEnemy(EnemyData data)
    {
        _enemiesToAdd.Add(data);
        TryAddEnemy();
    }

    private void TryAddEnemy()
    {
        if (_enemiesToAdd.Count == 0)
        {
            return;
        }

        foreach (var spawn in _spawnPoints)
        {
            if (!spawn.IsClosed())
            {
                foreach (var enemy in _enemiesToAdd)
                {
                    var obj = FactoryAbstractHandler.Instance.CreateEnemyPistolMan();
                    obj.transform.position = spawn.GetPosition() + new Vector3(70, 0, 0);
                    obj.SetData(enemy);
                    _enemiesToAdd.Remove(enemy);
                    break;
                }
            }

            if (_enemiesToAdd.Count == 0)
            {
                return;
            }
        }
    }
}
