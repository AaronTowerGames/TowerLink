using UnityEngine;

public class Enemy : MonoBehaviour, IDamageble
{
    [SerializeField]
    private EnemyData _data;

    private void Start()
    {
        EnemyData data = new EnemyData
        {
            id = 1,
            hp = 100
        };
        SetData(data);
    }

    public EnemyData GetData()
    {
        return _data;
    }

    public void SetData(EnemyData enemyData)
    {
        _data = enemyData;
    }
    public int GetId()
    {
        return _data.id;
    }

    private void OnEnable()
    {
        EventBus.Hit.Subscribe(GetDamage);
    }

    private void OnDisable()
    {
        EventBus.Hit.Subscribe(GetDamage);
    }

    private void GetDamage(IDamageble damageble, int damage)
    {
        if (damageble == this)
            Damage(damage);
    }

    private void Damage(int damage)
    {
        _data.hp -= damage;
        if (_data.hp <= 0)
        {
            EventBus.EnemyDie.Invoke(this);
        }
    }
}
