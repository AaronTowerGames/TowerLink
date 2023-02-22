using Spine.Unity;
using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageble
{
    [SerializeField]
    private EnemyData _data;

    [SerializeField]
    private SkeletonAnimation _skeletonAnimation;

    private int hpTest = -999;

    private float _attackSpeed = 0.05f;

    private bool _isHeroShow = false;
    private bool _isHeroLife = true;
    private bool _isLife = true;

    public int GetHP()
    {
        return _data.hp;
    }

    public void SetData(EnemyData data, int line)
    {
        _data = new EnemyData();
        _data.type = data.type;
        _data.id = data.id;
        _data.hp = data.hp;
        _data.aimTime = data.aimTime;
        _data.chance= data.chance;

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
        EventBus.HeroUP.Subscribe(DamageHero);
        EventBus.HeroDOWN.Subscribe(HeroHide);
        EventBus.HeroDie.Subscribe(HeroDie);
    }

    private void OnDestroy()
    {
        EventBus.Hit.Unsubscribe(GetDamage);
        EventBus.HeroUP.Unsubscribe(DamageHero);
        EventBus.HeroDOWN.Unsubscribe(HeroHide);
        EventBus.HeroDie.Unsubscribe(HeroDie);
    }

    private void HeroDie()
    {
        _skeletonAnimation.AnimationName = "stand_idle";
        _isHeroShow = false;
        _isHeroLife = false;
        StopAllCoroutines();
    }

    private void HeroHide()
    {
        _isHeroShow= false;
    }

    private void Start()
    {
        Teleportation();
        StartCoroutine(TryFindHero());
    }

    private IEnumerator TryFindHero()
    {
        if (_isLife)
        {
            if (!_isHeroLife)
            {
                yield break;
            }
            while (!_isHeroShow)
            {
                yield return new WaitForFixedUpdate();
            }

            StartCoroutine(WaitAimedEnd());
        }
    }


    private void Teleportation()
    {
        StartCoroutine(WaitTeleportationAnimationEnd());
    }

    private IEnumerator WaitTeleportationAnimationEnd()
    {
        _skeletonAnimation.AnimationName = "start";
        yield return new WaitForSeconds(DataSettings.DELAY_ENEMY_TELEPORTATION_ANIMATION);
        _skeletonAnimation.AnimationName = "stand_idle";
    }

    private void DamageHero()
    {
        _isHeroShow = true;
    }

    private IEnumerator WaitAimedEnd()
    {
        //yield return new WaitForSeconds(_data.aimTime);
        //Debug.Log("ENEMY AIM");
        yield return new WaitForSeconds((int)(_data.aimTime * DinamicTest.Instance.GetEnemyAttackSpeed() * _attackSpeed));
        //Debug.Log("ENEMY SHOOT");
        StartCoroutine(WaitShootAnimationEnd());
        if (RandomizeSystem.Chance(_data.chance, 100))
        {
            Debug.Log("ENEMY GOOD SHOOT");
            EventBus.HeroDamage.Invoke((int)DinamicTest.Instance.GetEnemyDamage());
        }

        StartCoroutine(TryFindHero());
    }

    private IEnumerator WaitShootAnimationEnd()
    {
        _skeletonAnimation.AnimationName = "stand_go_left";
        yield return new WaitForSeconds(DataSettings.DELAY_ENEMY_ATTACK_ANIMATION);
        _skeletonAnimation.AnimationName = "stand_idle";
    }

    private void GetDamage(GameObject enemy, int damage)
    {
        if (enemy == this.gameObject)
        {
            Damage(damage);
            EventBus.OnChangeEnemyHP.Invoke(this);
        }
    }

    private IEnumerator WaitDamageAnimationEnd()
    {
        yield return new WaitForSeconds(DataSettings.DELAY_DAMAGE_ENEMY_ANIMATION);
        _skeletonAnimation.AnimationName = "stand_idle";
    }

    private void Damage(int damage)
    {
        if (hpTest == -999)
        {
            hpTest = (int)(_data.hp * DinamicTest.Instance.GetEnemyHP());
        }
        hpTest -= damage;
        _data.hp = hpTest;

        //_data.hp -= damage;
        if (_data.hp <= 0)
        {
            StopAllCoroutines();
            StartCoroutine(WaitDieAnimationEnd());
        }
        else
        {
            _skeletonAnimation.AnimationName = "stand_damage";
            StartCoroutine(WaitDamageAnimationEnd());
        }
    }

    private IEnumerator WaitDieAnimationEnd()
    {
        _skeletonAnimation.AnimationName = "Death";
        yield return new WaitForSeconds(DataSettings.DELAY_DIE_ENEMY_ANIMATION);
        EventBus.EnemyDie.Invoke(this);
    }
}
