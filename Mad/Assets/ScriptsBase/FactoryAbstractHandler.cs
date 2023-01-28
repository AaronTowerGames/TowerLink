using UnityEngine;

public class FactoryAbstractHandler : GSC<FactoryAbstractHandler>
{
    [SerializeField]
    private Enemy _enemyPrefab, _enemyPistolManPrefab;
    [SerializeField]
    private FactoryEnemy _factoryEnemy;

    public Enemy CreateEnemy()
    {
        return _factoryEnemy.Create(_enemyPrefab);
    }

    public Enemy CreateEnemyPistolMan()
    {
        return _factoryEnemy.Create(_enemyPistolManPrefab);
    }
}
