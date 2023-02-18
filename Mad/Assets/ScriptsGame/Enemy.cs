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
    }

    private void OnDestroy()
    {
        EventBus.Hit.Unsubscribe(GetDamage);
        EventBus.HeroUP.Unsubscribe(DamageHero);
    }

    private void DamageHero()
    {
        StartCoroutine(WaitAimedEnd());
    }

    private IEnumerator WaitAimedEnd()
    {
        //yield return new WaitForSeconds(_data.aimTime);
        yield return new WaitForSeconds((int)(_data.aimTime * DinamicTest.Instance.GetEnemyAttackSpeed()));
        if (RandomizeSystem.Chance(_data.chance, 100))
        {
            EventBus.HeroDamage.Invoke((int)DinamicTest.Instance.GetEnemyDamage());
        }
        
    }

    private void GetDamage(GameObject enemy, int damage)
    {
        if (enemy == this.gameObject)
        {
            Damage(damage);
            EventBus.OnChangeEnemyHP.Invoke(this);
            _skeletonAnimation.AnimationName = "stand_damage";
            StartCoroutine(WaitDamageAnimationEnd());
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
            _skeletonAnimation.AnimationName = "Death";
            _skeletonAnimation.loop= false;
            StartCoroutine(WaitDieAnimationEnd());
        }
    }

    private IEnumerator WaitDieAnimationEnd()
    {
        yield return new WaitForSeconds(DataSettings.DELAY_DIE_ENEMY_ANIMATION);
        EventBus.EnemyDie.Invoke(this);
    }
}
