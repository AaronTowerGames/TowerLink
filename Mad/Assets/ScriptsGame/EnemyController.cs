using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private List<EnemyData> _enemiesInRound = new List<EnemyData>();

    [SerializeField]
    private List<Enemy> _enemiesInScene = new List<Enemy>();

    private EnemyPool enemyPool = new EnemyPool();
    private int _startEnemyCount = 0;
    private bool readyToStage2Loading = false;

    public void Add(EnemyData data)
    {
        _enemiesInRound.Add(data);
    }

    private void OnEnable()
    {
        EventBus.StartLevel.Subscribe(StartLevel);

        EventBus.OnSetLevel.Subscribe(SetStartEnemyCount);
        EventBus.SetEnemyPool.Subscribe(StartWaitWhileDataLoading);
        EventBus.CheckStage1LevelLoadingEnd.Subscribe(SetReadyToStage2Loading);
        EventBus.OnAddEnemyInScene.Subscribe(AddEnemy);

        EventBus.AddEnemy.Subscribe(ReturnEnemy);
        EventBus.EnemyDie.Subscribe(EnemyDie);
    }

    private void OnDestroy()
    {
        EventBus.StartLevel.Unsubscribe(StartLevel);

        EventBus.OnSetLevel.Unsubscribe(SetStartEnemyCount);
        EventBus.SetEnemyPool.Unsubscribe(StartWaitWhileDataLoading);
        EventBus.CheckStage1LevelLoadingEnd.Unsubscribe(SetReadyToStage2Loading);
        EventBus.OnAddEnemyInScene.Unsubscribe(AddEnemy);

        EventBus.AddEnemy.Unsubscribe(ReturnEnemy);
        EventBus.EnemyDie.Unsubscribe(EnemyDie);
    }

    private void EnemyDie(Enemy obj)
    {
        _enemiesInScene.Remove(obj);
        Destroy(obj.gameObject);
    }

    private void SetReadyToStage2Loading()
    {
        readyToStage2Loading = true;
    }

    private void StartLevel()
    {
        StartCoroutine(EnemyInRoundChecker());

        StartCoroutine(TrySpawnEnemy());
    }

    private void AddEnemy(Enemy obj)
    {
        _enemiesInScene.Add(obj);
    }

    private void SetStartEnemyCount(LevelData obj)
    { 
        _startEnemyCount = obj.startEnemyCount;

        EventBus.OnSetEnemyCountEnd.Invoke();
    }

    private void StartWaitWhileDataLoading(EnemyPool obj)
    {
        enemyPool = obj;
        StartCoroutine(WaitWhileDataLoading());
    }

    private void SetEnemies(EnemyPool obj)
    {
        foreach (var item in obj.Get())
        {
            Add(EnemyDatas.Instance.Get(item));
        }

        StartCoroutine(TryStartSwapnEnemy());
    }

    private IEnumerator WaitWhileDataLoading()
    {
        while (!readyToStage2Loading)
        {
            yield return new WaitForSeconds(1);
        }

        SetEnemies(enemyPool);
    }

    private IEnumerator TryStartSwapnEnemy()
    {
        while (_startEnemyCount > _enemiesInRound.Count)
        {
            yield return new WaitForSeconds(1);
        }

        SpawnStartEnemy();
    }

    private void SpawnStartEnemy()
    {
        for (int i = 0; i < _startEnemyCount; i++)
        {
            EventBus.SpawnStartEnemy.Invoke(_enemiesInRound[i]);
            _enemiesInRound.RemoveAt(i);
        }

        EventBus.OnSetEnemyInSpawnEnd.Invoke();
    }

    private void ReturnEnemy(int id)
    {
        /*
        foreach (var item in _enemies)
        {
            if (item.GetId() == id)
            {
                EventBus.OnAddEnemy.Invoke(item.GetData());
                break;
            }
        }/**/
    }

    private IEnumerator EnemyInRoundChecker()
    {
        while (_enemiesInRound.Count > 0 || _enemiesInScene.Count > 0)
        {
            yield return new WaitForFixedUpdate();
        }

        EventBus.LevelWin.Invoke();
    }

    private IEnumerator TrySpawnEnemy()
    {
        while (_enemiesInRound.Count > 0)
        {
            yield return new WaitForSeconds(DataSettings.DELAY_SPAWN_ENEMY);
            EventBus.OnAddEnemy.Invoke(_enemiesInRound[0]);
            _enemiesInRound.RemoveAt(0);
        }
    }
}
