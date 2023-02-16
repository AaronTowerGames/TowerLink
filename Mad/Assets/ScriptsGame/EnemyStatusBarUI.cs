using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusBarUI : MonoBehaviour
{
    private void OnEnable()
    {
        EventBus.OnAddEnemyInScene.Subscribe(AddEnemySkull);
    }

    private void OnDestroy()
    {
        EventBus.OnAddEnemyInScene.Unsubscribe(AddEnemySkull);
    }

    private void AddEnemySkull(Enemy enemy)
    {
        var obj = FactoryAbstractHandler.Instance.CreateSkull();
        obj.transform.SetParent(transform, false);
        obj.SetData(enemy);
    }
}
