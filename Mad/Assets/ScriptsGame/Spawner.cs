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

    private IEnumerator tryspawn;
    private void OnEnable()
    {
        EventBus.StartLevel.Subscribe(StartLevel);

        EventBus.SpawnStartEnemy.Subscribe(SpawnStartEnemy);

        EventBus.AddSpawnPoint.Subscribe(AddSpawnPoint);
        EventBus.OnSpawnPointDestroy.Subscribe(DestroySpawnPoint);
        EventBus.OnSpawnPointOpen.Subscribe(SpawnPointOpen);
        EventBus.OnSpawnPointClose.Subscribe(SpawnPointClose);

        EventBus.OnAddEnemy.Subscribe(AddEnemy);
    }

    private void OnDisable()
    {
        EventBus.StartLevel.Unsubscribe(StartLevel);
        
        EventBus.SpawnStartEnemy.Unsubscribe(SpawnStartEnemy);
        EventBus.AddSpawnPoint.Unsubscribe(AddSpawnPoint);
        EventBus.OnSpawnPointDestroy.Unsubscribe(DestroySpawnPoint);
        EventBus.OnSpawnPointOpen.Unsubscribe(SpawnPointOpen);
        EventBus.OnSpawnPointClose.Unsubscribe(SpawnPointClose);
        EventBus.OnAddEnemy.Unsubscribe(AddEnemy);
    }

    private void StartLevel()
    {
        foreach (var spawn in _spawnPoints)
        {
            if (spawn.IsStartPoint())
            {
                Spawn(spawn);
            }
        }
    }

    private void SpawnStartEnemy(EnemyData data)
    {
        _enemiesToAdd.Add(data);
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
        if (tryspawn == null)
        {
            Debug.Log("CREATE TRY SPAWN");
            tryspawn = TrySpawn();
        }
        else
        {
            Debug.Log("STOP TRY SPAWN");
            StopCoroutine(tryspawn);
        }
        StartCoroutine(TrySpawn());
    }

    private IEnumerator TrySpawn()
    {
        Debug.Log("TrySpawn");
        while (_enemiesToAdd.Count > 0)
        {
            TryAddEnemy();
            yield return new WaitForSeconds(1);
        }
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
                Spawn(spawn);
            }

            if (_enemiesToAdd.Count == 0)
            {
                return;
            }
        }
    }

    private void Spawn(SpawnPoint spawn)
    {
        foreach (var enemy in _enemiesToAdd)
        {
            var obj = FactoryAbstractHandler.Instance.CreateEnemyPistolMan();
            obj.transform.position = spawn.GetPosition();
            obj.transform.SetParent(spawn.transform.parent);
            spawn.ClosePoint();
            obj.SetData(enemy, spawn.GetLine());
            _enemiesToAdd.Remove(enemy);
            break;
        }
    }
}
