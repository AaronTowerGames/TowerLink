using Spine.Unity;
using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private Sprite _shootSprite;

    //[SerializeField]
    private SpriteRenderer _shootSprite2;

    [SerializeField]
    private Vector2 _shootOffsetL, _shootOffsetR;

    [SerializeField]
    private SkeletonAnimation _skeletonAnimation;

    [SerializeField]
    private HeroEquipment _heroEquipment;

    [SerializeField]
    private Transform _heroTransform, _cameraTransform;

    private RectTransform _transform;

    [SerializeField]
    private bool isAutoFire = false;
    private float attackSpeed = 1f;

    private void Start()
    {
        _transform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        EventBus.SetHeroEquipment.Subscribe(SetHeroEquipment);
        EventBus.FireButtonClicked.Subscribe(FireFromButton);
        EventBus.AutoFireOn.Subscribe(AutoFireOn);
        EventBus.AutoFireOff.Subscribe(AutoFireOff);
    }

    private void OnDisable()
    {
        EventBus.SetHeroEquipment.Unsubscribe(SetHeroEquipment);
        EventBus.FireButtonClicked.Unsubscribe(FireFromButton);
        EventBus.AutoFireOn.Unsubscribe(AutoFireOn);
        EventBus.AutoFireOff.Unsubscribe(AutoFireOff);
    }

    private void SetHeroEquipment(HeroEquipment obj)
    {
        _heroEquipment= obj;
        EventBus.OnSetHeroEquipment.Invoke();
    }

    private void AutoFireOn()
    {
        EventBus.HeroUP.Invoke();
        isAutoFire = true;
        if (_heroEquipment.LeftArm() != null)
        {
            if (_heroEquipment.LeftArm().id != 0)
                StartCoroutine(AutoFireLeft());
        }
        if (_heroEquipment.RightArm() != null)
        {
            if (_heroEquipment.RightArm().id != 0)
                StartCoroutine(AutoFireRight());
        }   
    }

    private void AutoFireOff()
    {
        EventBus.HeroDOWN.Invoke();
        isAutoFire = false;
        StopAllCoroutines();
    }

    private void FireFromButton()
    {
        EventBus.HeroUP.Invoke();
        if (!isAutoFire)
        {
            FireLeftArm();
            FireRightArm();
        }
        StartCoroutine(WaitUpAnimationEnd());
    }

    private IEnumerator WaitUpAnimationEnd()
    {
        yield return new WaitForSeconds(DataSettings.DELAY_DAMAGE_ENEMY_ANIMATION);
        EventBus.HeroDOWN.Invoke();
    }

    private void FireLeftArm()
    {
        if (_heroEquipment.LeftArm() == null)
        {
            return;
        }
        if (_heroEquipment.LeftArm().id == 0)
        {
            return;
        }

        var damage = 0;

        damage += _heroEquipment.LeftArm().damage;

        var p = new Vector3(transform.position.x, transform.position.y, 0);
        RaycastHit2D ray2d = Physics2D.Raycast(new Vector3(p.x, p.y, 0), Vector2.zero);
        if (ray2d.collider != null)
        {
            IDamageble damageble;
            if (ray2d.collider.TryGetComponent<IDamageble>(out damageble))
            {
                Enemy enemy;
                if (ray2d.collider.TryGetComponent<Enemy>(out enemy))
                {
                    //EventBus.Hit.Invoke(enemy.gameObject, damage);
                    EventBus.Hit.Invoke(enemy.gameObject, (int)(damage * DinamicTest.Instance.GetHeroDamage()));
                }

                Barrier barrier;
                if (ray2d.collider.TryGetComponent<Barrier>(out barrier))
                {
                    //EventBus.Hit.Invoke(barrier.gameObject, damage);
                    EventBus.Hit.Invoke(barrier.gameObject, (int)(damage * DinamicTest.Instance.GetHeroDamage()));
                }
            }
        }
        /*
        var p = new Vector3(_transform.position.x - _cameraTransform.localPosition.x, _transform.position.y + _heroTransform.position.y + _cameraTransform.localPosition.y + _cameraTransform.position.y, 0);
        Debug.Log($"p: {p} | a t: {_transform.position} | a l: {_transform.localPosition} | h: {_heroTransform.position} | c l: {_cameraTransform.localPosition} c t: {_cameraTransform.position}");
        RaycastHit2D ray2d = Physics2D.Raycast(new Vector3(p.x, p.y, 0), Vector2.zero);
        Debug.DrawRay(new Vector3(p.x, p.y, 0), Vector2.zero, Color.green);
        if (ray2d.collider != null)
        {
            IDamageble damageble;
            if (ray2d.collider.TryGetComponent<IDamageble>(out damageble))
            {
                Debug.Log("L DAMAGU NA: " + damage);
                EventBus.Hit.Invoke(damageble, damage);
            }
        }/**/
    }

    private void FireRightArm()
    {
        if (_heroEquipment.RightArm() == null)
        {
            return;
        }
        if (_heroEquipment.RightArm().id == 0)
        {
            return;
        }

        var damage = 0;

        damage += _heroEquipment.RightArm().damage;

        var p = new Vector3(_transform.position.x, _transform.position.y, 0);
        RaycastHit2D ray2d = Physics2D.Raycast(new Vector3 (p.x, p.y, 0), Vector2.zero, 10);
        if (ray2d.collider != null) //Не учитываем слой героя
        {
            IDamageble damageble;
            if (ray2d.collider.TryGetComponent<IDamageble>(out damageble))
            {
                Enemy enemy;
                if (ray2d.collider.TryGetComponent<Enemy>(out enemy))
                {
                    //EventBus.Hit.Invoke(enemy.gameObject, damage);
                    EventBus.Hit.Invoke(enemy.gameObject, (int)(1 * DinamicTest.Instance.GetHeroDamage()));
                }

                Barrier barrier;
                if (ray2d.collider.TryGetComponent<Barrier>(out barrier))
                {
                    //EventBus.Hit.Invoke(barrier.gameObject, damage);
                    EventBus.Hit.Invoke(barrier.gameObject, (int)(1 * DinamicTest.Instance.GetHeroDamage()));
                }
            }
        }
        else
        {

        }

        /*
        var x = _skeletonAnimation.separatorSlots[0].Data.BoneData.X;
        var y = _skeletonAnimation.separatorSlots[0].Data.BoneData.Y;
        _shootOffsetL = new Vector2(x, y);
        x = _skeletonAnimation.separatorSlots[1].Data.BoneData.X;
        y = _skeletonAnimation.separatorSlots[1].Data.BoneData.Y;
        _shootOffsetR = new Vector2(x, y);

        _shootSprite2.gameObject.SetActive(true);
        _shootSprite2.transform.position = _shootOffsetL;
        */
    }

    private IEnumerator AutoFireLeft()
    {
        while (isAutoFire)
        {
            Debug.Log("AutoFireLeft");
            //yield return new WaitForSeconds(_heroEquipment.LeftArm().attackSpeed);
            yield return new WaitForSeconds(_heroEquipment.LeftArm().attackSpeed * DinamicTest.Instance.GetHeroAttackSpeed());
            FireLeftArm();
        }
    }

    private IEnumerator AutoFireRight()
    {
        while (isAutoFire)
        {
            Debug.Log("AutoFireRight");
            //yield return new WaitForSeconds(_heroEquipment.RightArm().attackSpeed);
            yield return new WaitForSeconds(_heroEquipment.RightArm().attackSpeed * DinamicTest.Instance.GetHeroAttackSpeed());
            FireRightArm();
        }
    }
}
