using UnityEngine;

public class EnemyPool : GSC<EnemyPool>
{
    [SerializeField]
    private EnemyType[] _pool = new EnemyType[20];

    public EnemyType[] Get()
    {
        if (_pool != null)
        {
            return _pool;
        }
        return null;
    }
}
