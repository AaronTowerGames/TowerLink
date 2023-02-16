using Spine.Unity;
using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageble
{
    [SerializeField]
    private EnemyData _data;

    [SerializeField]
    private SkeletonAnimation _skeletonAnimation;

    public void SetData(EnemyData data, int line)
    {
        _data = new EnemyData();
        _data.type = data.type;
        _data.id = data.id;
        _data.hp = data.hp;
        _data.aimTime = data.aimTime;

        SetScale(line);
        SetLayer(line);
        EventBus.OnAddEnemyInScene.Invoke(this);
    }

    private void SetScale(int line)
    {
        if (line == 1)
        {
            transform.localScale = new Vector3(DataSettings.LINE1_SCALE, DataSettings.LINE1_SCALE, 1);
        }
        else if (line == 2)
        {
            transform.localScale = new Vector3(DataSettings.LINE2_SCALE, DataSettings.LINE2_SCALE, 1);
        }
        else if (line == 3)
        {
            transform.localScale = new Vector3(DataSettings.LINE3_SCALE, DataSettings.LINE3_SCALE, 1);
        }
    }

    private void SetLayer(int line)
    {
        if (line == 1)
        {
            _skeletonAnimation.GetComponent<MeshRenderer>().sortingOrder = DataSettings.LINE1_LAYER+5;
        }
        else if (line == 2)
        {
            _skeletonAnimation.GetComponent<MeshRenderer>().sortingOrder = DataSettings.LINE2_LAYER+5;
        }
        else if (line == 3)
        {
            _skeletonAnimation.GetComponent<MeshRenderer>().sortingOrder = DataSettings.LINE3_LAYER+5;
        }
    }
    /*
    public EnemyData GetData()
    {
        return _data;
    }

    public int GetId()
    {
        return _data.id;
    }/**/

    private void OnEnable()
    {
        EventBus.Hit.Subscribe(GetDamage);
    }

    private void OnDestroy()
    {
        EventBus.Hit.Unsubscribe(GetDamage);
    }

    private void GetDamage(GameObject enemy, int damage)
    {
        if (enemy == this.gameObject)
        {
            Debug.Log("SUDA URON" + damage);
            Damage(damage);
        }
            
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
