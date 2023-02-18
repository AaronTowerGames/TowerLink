using System;
using UnityEngine;
using UnityEngine.UI;

public class DinamicTest : GSC<DinamicTest>
{
    [SerializeField]
    private InputField nameSetup;

    [SerializeField]
    private Slider sliderEnemyDamage, sliderEnemyHP, sliderEnemyAttackSpeed, sliderHeroDamage, sliderHepoAttackSpeed, sliderCrosshireSpeed, sliderHeroMoveSpeed, sliderHeroHP;

    internal float GetEnemyDamage()
    {
        return sliderEnemyDamage.value;
    }
    internal float GetEnemyHP()
    {
        return sliderEnemyHP.value;
    }
    internal float GetEnemyAttackSpeed()
    {
        return sliderEnemyAttackSpeed.value;
    }

    internal float GetHeroDamage()
    {
        return sliderHeroDamage.value;
    }

    internal float GetHeroAttackSpeed()
    {
        return sliderHepoAttackSpeed.value;
    }
    internal float GetHeroHP()
    {
        return sliderHeroHP.value;
    }
    internal float GetHeroMoveSpeed()
    {
        return sliderHeroMoveSpeed.value;
    }
    internal float GetCrosshairSpeed()
    {
        return sliderCrosshireSpeed.value;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("ENEMY_DAMAGE"))
        {
            sliderEnemyDamage.value = PlayerPrefs.GetFloat("ENEMY_DAMAGE");
        }
        else
        {
            sliderEnemyDamage.value = DataSettings.ENEMY_ATTACK_DAMAGE;
        }

        if (PlayerPrefs.HasKey("ENEMY_ATTACK_SPEED"))
        {
            Debug.Log(PlayerPrefs.GetFloat("ENEMY_ATTACK_SPEED"));
            sliderEnemyAttackSpeed.value = PlayerPrefs.GetFloat("ENEMY_ATTACK_SPEED");
        }
        else
        {
            sliderEnemyAttackSpeed.value = DataSettings.ENEMY_ATTACK_SPEED;

        }

        if (PlayerPrefs.HasKey("ENEMY_HP"))
        {
            sliderEnemyHP.value = PlayerPrefs.GetFloat("ENEMY_HP");
        }
        else
        {
            sliderEnemyHP.value = DataSettings.ENEMY_HP;

        }

        if (PlayerPrefs.HasKey("HERO_DAMAGE"))
        {
            sliderHeroDamage.value = PlayerPrefs.GetFloat("HERO_DAMAGE");
        }
        else
        {
            sliderHeroDamage.value = DataSettings.HERO_DAMAGE;

        }

        if (PlayerPrefs.HasKey("HERO_ATTACKSPEED"))
        {
            sliderHepoAttackSpeed.value = PlayerPrefs.GetFloat("HERO_ATTACKSPEED");
        }
        else
        {
            sliderHepoAttackSpeed.value = DataSettings.HERO_ATTACKSPEED;

        }

        if (PlayerPrefs.HasKey("HERO_MOVESPEED"))
        {
            sliderHeroMoveSpeed.value = PlayerPrefs.GetFloat("HERO_MOVESPEED");
        }
        else
        {
            sliderHeroMoveSpeed.value = DataSettings.HERO_MOVESPEED;

        }

        if (PlayerPrefs.HasKey("HERO_HP"))
        {
            sliderHeroHP.value = PlayerPrefs.GetFloat("HERO_HP");
        }
        else
        {
            sliderHeroHP.value = DataSettings.HERO_HP;

        }

        if (PlayerPrefs.HasKey("CROSSHAIR_MOVESPEED"))
        {
            sliderCrosshireSpeed.value = PlayerPrefs.GetFloat("CROSSHAIR_MOVESPEED");
        }
        else
        {
            sliderCrosshireSpeed.value = DataSettings.CROSSHAIR_SPEED;

        }
    }
}
