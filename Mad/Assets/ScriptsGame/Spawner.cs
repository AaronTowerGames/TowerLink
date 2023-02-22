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

    private int maxEnemyInScene = 0;
    private int enemyInScene = 0;

    private void OnEnable()
    {
        EventBus.OnSetMaxEnemyInScene.Subscribe(SetMaxEnemyInScene);

        EventBus.StartLevel.Subscribe(StartLevel);

        EventBus.SpawnStartEnemy.Subscribe(SpawnStartEnemy);

        EventBus.AddSpawnPoint.Subscribe(AddSpawnPoint);
        EventBus.OnSpawnPointDestroy.Subscribe(DestroySpawnPoint);
        EventBus.OnSpawnPointOpen.Subscribe(SpawnPointOpen);
        EventBus.OnSpawnPointClose.Subscribe(SpawnPointClose);

        EventBus.OnAddEnemy.Subscribe(AddEnemy);
        EventBus.OnChangeCountEnemyInScene.Subscribe(SetCountEnemyInScene);
    }

    private void OnDisable()
    {
        EventBus.OnSetMaxEnemyInScene.Unsubscribe(SetMaxEnemyInScene);

        EventBus.StartLevel.Unsubscribe(StartLevel);
        
        EventBus.SpawnStartEnemy.Unsubscribe(SpawnStartEnemy);
        EventBus.AddSpawnPoint.Unsubscribe(AddSpawnPoint);
        EventBus.OnSpawnPointDestroy.Unsubscribe(DestroySpawnPoint);
        EventBus.OnSpawnPointOpen.Unsubscribe(SpawnPointOpen);
        EventBus.OnSpawnPointClose.Unsubscribe(SpawnPointClose);

        EventBus.OnAddEnemy.Unsubscribe(AddEnemy);
        EventBus.OnChangeCountEnemyInScene.Subscribe(SetCountEnemyInScene);
    }

    private void SetCountEnemyInScene(int obj)
    {
        enemyInScene = obj;
    }

    private void SetMaxEnemyInScene(LevelPhaseData obj)
    {
        maxEnemyInScene = obj.maxEnemyInScene;
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
            tryspawn = TrySpawn();
        }
        else
        {
            StopCoroutine(tryspawn);
        }
        StartCoroutine(TrySpawn());
    }

    private IEnumerator TrySpawn()
    {
        while (_enemiesToAdd.Count > 0)
        {
            if (enemyInScene < maxEnemyInScene)
            {
                TryAddEnemy();
            }
            yield return new WaitForSeconds(1);
        }
    }

    private void TryAddEnemy()
    {
        if (_enemiesToAdd.Count == 0)
        {
            return;
        }

        List<SpawnPoint> openSpawns = new List<SpawnPoint>();
        foreach (var spawn in _spawnPoints)
        {
            if (!spawn.IsClosed())
            {
                openSpawns.Add(spawn);
            }
        }

        if (openSpawns.Count > 0)
        {
            int randomNum = RandomizeSystem.GetRandom(openSpawns.Count);

            Spawn(openSpawns[randomNum]);
        }
    }

    private void Spawn(SpawnPoint spawn)
    {
        foreach (var enemy in _enemiesToAdd)
        {
            var obj = FactoryAbstractHandler.Instance.CreateEnemyPistolMan();
            Vector3 vector = new Vector3(0, 180, 0);
            var line = spawn.GetLine();
            obj.transform.position = spawn.GetPosition() - vector;
            obj.transform.SetParent(spawn.transform.parent);
            spawn.ClosePoint();
            obj.SetData(enemy, line);
            _enemiesToAdd.Remove(enemy);
            break;
        }
    }
}
