using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField]
    private List<Enemy> _enemies = new List<Enemy>();
    private void OnEnable()
    {
        EventBus.AddEnemy.Subscribe(ReturnEnemy);
    }

    private void OnDestroy()
    {
        EventBus.AddEnemy.Unsubscribe(ReturnEnemy);
    }

    private void ReturnEnemy(int id)
    {
        foreach (var item in _enemies)
        {
            if (item.GetId() == id)
            {
                EventBus.OnAddEnemy.Invoke(item.GetData());
            }
        }
    }
}
