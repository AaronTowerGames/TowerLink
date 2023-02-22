using Spine.Unity;
using UnityEngine;

public abstract class Barrier : MonoBehaviour, IDamageble
{
    [SerializeField]
    internal SkeletonAnimation _skeletonAnimation;

    [SerializeField]
    internal Skining _skining;

    [SerializeField]
    internal BarrierData _data;

    [SerializeField]
    internal BoxCollider2D _boxCollider;


    internal int _maxHP;

    public void SetData(BarrierData data)
    {
        _data = new BarrierData
        {
            hp= data.hp,
            id= data.id
        };
        _maxHP = data.hp;
    }

    private void OnEnable()
    {
        EventBus.Hit.Subscribe(GetDamage);
    }

    private void OnDisable()
    {
        EventBus.Hit.Subscribe(GetDamage);
    }
    
    private void OnValidate()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _skining= GetComponent<Skining>();
        _skeletonAnimation = GetComponent<SkeletonAnimation>();
        _data.hp = 1;
    }/**/

    internal virtual void GetDamage(GameObject barrier, int damage)
    {
        if (barrier == this.gameObject)
        { 
            _data.hp -= damage;
            float percent = _data.hp / (float)_maxHP;
            if (percent <= DataSettings.BARRIER_SET_DAMAGED_1 && percent > DataSettings.BARRIER_SET_DAMAGED_2) 
            {
                Debug.Log("SKIN 75"+percent);
                _skining.SetSkin(_skeletonAnimation.skeleton, "75");
                _boxCollider.offset.Set(_boxCollider.offset.x, DataSettings.BARRIER_SET_DAMAGED_COLLIDER_1_Y_OFFSET);
                _boxCollider.size.Set(_boxCollider.size.x, DataSettings.BARRIER_SET_DAMAGED_COLLIDER_1_Y_HEIGHT);
            }
            else if (percent <= DataSettings.BARRIER_SET_DAMAGED_2)
            {
                Debug.Log("SKIN 50" + percent);
                _skining.SetSkin(_skeletonAnimation.skeleton, "50");
                _boxCollider.offset.Set(_boxCollider.offset.x, DataSettings.BARRIER_SET_DAMAGED_COLLIDER_2_Y_OFFSET);
                _boxCollider.size.Set(_boxCollider.size.x, DataSettings.BARRIER_SET_DAMAGED_COLLIDER_2_Y_HEIGHT);
            }
            else
            {
                Debug.Log("SKIN 100" + percent);
                _skining.SetSkin(_skeletonAnimation.skeleton, "100");
            }
        }
    }
}
