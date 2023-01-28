using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : MonoBehaviour
{
    [SerializeField]
    private float _maxBar = 1f, _currentBar = 0f;

    [SerializeField]
    private ProgressBarScript _progressBarScript;

    private int _countEnemy = 1;
    private int _currentEnemy = 0;

    [SerializeField]
    private List<Enemy> _enemyList = new List<Enemy>();

    [SerializeField]
    private float _secToAddEnemy = 3f;

    private Queue<Enemy> _enemyQueue = new Queue<Enemy>();

    private void Start()
    {
        _progressBarScript.SetMax(_maxBar);
        _countEnemy = _enemyList.Count;
    }

    private void OnEnable()
    {
        EventBus.OnNeedNextEnemy.Subscribe(ProduceEnemy);
    }

    private void OnDisable()
    {
        EventBus.OnNeedNextEnemy.Unsubscribe(ProduceEnemy);
    }

    private void ProduceEnemy()
    {
        Debug.Log("AS");
        if (_currentEnemy > _enemyList.Count - 1)
            return;

        if (_enemyQueue.Count > 0)
        {
            AddToQueue();
            return;
        }
        AddToQueue();
        StartCoroutine(AddEnemy());
    }

    private void AddToQueue()
    {
        _enemyQueue.Enqueue(_enemyList[_currentEnemy]);
        _currentEnemy++;
    }

    private IEnumerator AddEnemy()
    {
        var delta = 0f;

        while (delta < _secToAddEnemy)
        {
            yield return new WaitForSeconds(DataSettings.MIN_DELTA_IENUMERATOR);
            delta = AddProgress(delta);
        }

        AddProgress(delta);

        EventBus.AddEnemy.Invoke(1); //_enemyQueue.Peek().GetId()

        _enemyQueue.Dequeue();

        if (_enemyQueue.Count > 0)
        {
            StartCoroutine(AddEnemy());
        }
    }

    private float AddProgress(float delta)
    {
        delta += DataSettings.MIN_DELTA_IENUMERATOR;
        _progressBarScript.SetCurrent(delta / _secToAddEnemy);
        return delta;
    }
}
