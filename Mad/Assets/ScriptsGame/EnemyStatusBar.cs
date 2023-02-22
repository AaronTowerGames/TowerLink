using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusBar : MonoBehaviour
{
    [SerializeField]
    private ImageFiller _imageFiller;

    [SerializeField]
    private Enemy _enemy;

    private int _maxHP;

    private bool isFirst = true;

    public void SetData(Enemy enemy)
    {
        _enemy = enemy;
        _maxHP = _enemy.GetHP();
        ChangeHP(_enemy);
        isFirst= true;
    }

    private void OnEnable()
    {
        EventBus.OnChangeEnemyHP.Subscribe(ChangeHP);
        EventBus.EnemyDie.Subscribe(EnemyDie);
    }

    private void OnDestroy()
    {
        EventBus.OnChangeEnemyHP.Unsubscribe(ChangeHP);
        EventBus.EnemyDie.Unsubscribe(EnemyDie);
    }

    private void EnemyDie(Enemy enemy)
    {
        if (_enemy == enemy)
        {
            Destroy(gameObject);
        }
    }

    private void ChangeHP(Enemy enemy)
    {
        if (isFirst)
        {
            _maxHP = enemy.GetHP();
            isFirst = false;
        }

        if (enemy == _enemy)
        {
            _imageFiller.SetFill(enemy.GetHP() / (float)_maxHP);
        }
        
    }
}
