using System;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField]
    private HeroData data = new HeroData();

    private bool isStay = false;

    int hpTest = -999;
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
    }

    private void OnEnable()
    {
        EventBus.HeroDamage.Subscribe(GetDamage);
        EventBus.HeroDOWN.Subscribe(HeroDOWN);
        EventBus.HeroUP.Subscribe(HeroUP);
    }

    private void OnDestroy()
    {
        EventBus.HeroDamage.Unsubscribe(GetDamage);
        EventBus.HeroDOWN.Unsubscribe(HeroDOWN);
        EventBus.HeroUP.Unsubscribe(HeroUP);
    }

    private void HeroDOWN()
    {
        isStay = false;
    }

    private void HeroUP()
    {
        isStay = true;
    }

    private void GetDamage(int damage)
    {
        if (isStay)
        {
            if (hpTest == -999)
            {
                hpTest = (int)(data.hp * DinamicTest.Instance.GetHeroHP());
            }
            hpTest -= damage;
            data.hp = hpTest;
            //data.hp -= damage;

            EventBus.OnChangeHeroHP.Invoke(data.hp);
            if (data.hp < 0)
            {
                EventBus.HeroDie.Invoke();
            }
        }
    }
}
