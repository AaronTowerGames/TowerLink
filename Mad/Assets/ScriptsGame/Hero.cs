using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField]
    private HeroData data = new HeroData();

    [SerializeField]
    private MeshRenderer meshRenderermeshRenderer = null;

    [SerializeField]
    private SkeletonAnimation _skeletonAnimation;

    private bool isStay = false;
    private bool isLife = true;

    int hpTest = -999;

    Skeleton skeleton;

    Spine.Animation walkAnimation;
    float animationTime = 0;
    internal void SetData(HeroData heroData)
    {
        data = new HeroData()
        {
            hp= heroData.hp,
            attackSpeed= heroData.attackSpeed,
            crosshireSpeed= heroData.crosshireSpeed,
            heroSkin= heroData.heroSkin,
            id= heroData.id,
            moveSpeed = heroData.moveSpeed
        };
        skeleton = _skeletonAnimation.skeleton;
        walkAnimation = skeleton.Data.FindAnimation("sit_idle");
    }

    private void OnEnable()
    {
        EventBus.HeroDamage.Subscribe(GetDamage);
        EventBus.HeroDOWN.Subscribe(HeroDOWN);
        EventBus.HeroUP.Subscribe(HeroUP);
        EventBus.StartLevel.Subscribe(Teleportation);
    }

    private void OnDestroy()
    {
        EventBus.HeroDamage.Unsubscribe(GetDamage);
        EventBus.HeroDOWN.Unsubscribe(HeroDOWN);
        EventBus.HeroUP.Unsubscribe(HeroUP);
        EventBus.StartLevel.Unsubscribe(Teleportation);
    }

    private void Teleportation()
    {
        StartCoroutine(WaitTeleportationAnimationEnd());
    }

    private IEnumerator WaitTeleportationAnimationEnd()
    {
        _skeletonAnimation.AnimationName = "start";
        meshRenderermeshRenderer.enabled = true;
        yield return new WaitForSeconds(DataSettings.DELAY_TELEPORTATION_ANIMATION);
        _skeletonAnimation.AnimationName = "sit_idle";
        /*
        AnimationStateData stateData = new AnimationStateData(_skeletonAnimation.skeleton.Data);
        stateData.SetMix("stand_jerk_left", "sit_idle", 0.2f);
        stateData.SetMix("sit_idle", "stand_jerk_left", 0.4f);
        Spine.AnimationState state = new Spine.AnimationState(stateData);
        state.SetAnimation(0, "stand_jerk_left", true);
        /**/
        //_skeletonAnimation.AnimationState.SetAnimation(0, "sit_idle", true);
        //_skeletonAnimation.AnimationState.AddAnimation(1, "sit_jerk_left", false, 0.4f);
        //_skeletonAnimation.AnimationState.SetAnimation(1, "sit_idle", true);
        //StartCoroutine(Render());
        //_skeletonAnimation.AnimationName = "sit_idle";
    }

    private IEnumerator Render()
    {
        while (isLife)
        {
            animationTime += Time.fixedDeltaTime;
            //walkAnimation.Apply(skeleton, animationTime, true, true);

            skeleton.UpdateWorldTransform();
            yield return new WaitForFixedUpdate();
        }

    }

    private void HeroDOWN()
    {
        isStay = false;
        _skeletonAnimation.AnimationName = "sit_idle";
    }

    private void HeroUP()
    {
        isStay = true;
        _skeletonAnimation.AnimationName = "stand_idle";
    }

    private void GetDamage(int damage)
    {
        if (isStay)
        {
            if (hpTest == -999)
            {
                hpTest = (int)(data.hp * DinamicTest.Instance.GetHeroHP());
                Debug.Log($"HP: {hpTest}");
            }
            hpTest -= damage;
            data.hp = hpTest;
            //data.hp -= damage;
            //Debug.Log($"HP2: {data.hp}");
            EventBus.OnChangeHeroHP.Invoke(data.hp);
            if (data.hp < 0)
            {
                isLife = false;
                Debug.Log("HERO DIE");
                EventBus.HeroDie.Invoke();
                StartCoroutine(WaitDieAnimationEnd());
            }
        }
    }

    private IEnumerator WaitDieAnimationEnd()
    {
        _skeletonAnimation.AnimationName = "end2";
        yield return new WaitForSeconds(DataSettings.DELAY_HERO_DIE_ANIMATION);
        meshRenderermeshRenderer.enabled = false;
        EventBus.LevelLose.Invoke();
    }
}
