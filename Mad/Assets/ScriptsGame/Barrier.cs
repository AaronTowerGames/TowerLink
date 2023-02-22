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
    public BoxCollider2D _boxCollider;

    internal int _maxHP;

    /*
    private void OnValidate()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _skining= GetComponent<Skining>();
        _skeletonAnimation = GetComponent<SkeletonAnimation>();
        _data.hp = 1;
    }/**/

    internal virtual void GetDamage(GameObject barrier, int damage)
    {
        
    }
}
