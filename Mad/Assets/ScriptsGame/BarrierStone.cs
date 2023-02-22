using System;
using UnityEngine;

public class BarrierStone : Barrier
{
    [SerializeField]
    internal BarrierType _type = BarrierType.Stone;

    private void Start()
    {
        SetHp();
    }

    private void OnEnable()
    {
        EventBus.StartLevel.Subscribe(SetHp);
        EventBus.Hit.Subscribe(GetDamage);
    }

    private void OnDestroy()
    {
        EventBus.StartLevel.Unsubscribe(SetHp);
        EventBus.Hit.Subscribe(GetDamage);
    }

    private void SetHp()
    {
        _data.hp = DinamicTest.Instance.GetStoneHP();
        _maxHP = DinamicTest.Instance.GetStoneHP();
    }

    internal override void GetDamage(GameObject barrier, int damage)
    {
        if (barrier == this.gameObject)
        {
            _data.hp -= damage;
            float percent = _data.hp / (float)DinamicTest.Instance.GetStoneHP();
            if (percent <= DataSettings.BARRIER_SET_DAMAGED_1 && percent > DataSettings.BARRIER_SET_DAMAGED_2)
            {
                _skining.SetSkin(_skeletonAnimation.skeleton, "75");
                _boxCollider.offset.Set(_boxCollider.offset.x, DataSettings.BARRIER_SET_DAMAGED_COLLIDER_1_Y_OFFSET);
                _boxCollider.size.Set(_boxCollider.size.x, DataSettings.BARRIER_SET_DAMAGED_COLLIDER_1_Y_HEIGHT);
            }
            else if (percent <= DataSettings.BARRIER_SET_DAMAGED_2)
            {
                _skining.SetSkin(_skeletonAnimation.skeleton, "50");
                _boxCollider.offset.Set(_boxCollider.offset.x, DataSettings.BARRIER_SET_DAMAGED_COLLIDER_2_Y_OFFSET);
                _boxCollider.size.Set(_boxCollider.size.x, DataSettings.BARRIER_SET_DAMAGED_COLLIDER_2_Y_HEIGHT);
            }
            else
            {
                _skining.SetSkin(_skeletonAnimation.skeleton, "100");
            }
        }
    }
}
