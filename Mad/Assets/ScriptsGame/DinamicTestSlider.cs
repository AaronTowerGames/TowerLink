using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DinamicTestSlider : MonoBehaviour
{
    [SerializeField]
    private Text text;

    [SerializeField]
    private DinamicTestFieldType fieldType;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();    
    }

    public void ChangeValue()
    {

        if (fieldType == DinamicTestFieldType.HERO_HP)
        {
            PlayerPrefs.SetFloat("HERO_HP", slider.value);
        }
        else if (fieldType == DinamicTestFieldType.HERO_ATTACKSPEED)
        {
            PlayerPrefs.SetFloat("HERO_ATTACKSPEED", slider.value);
        }
        else if (fieldType == DinamicTestFieldType.ENEMY_ATTACK_SPEED)
        {
            PlayerPrefs.SetFloat("ENEMY_ATTACK_SPEED", slider.value);
        }
        else if (fieldType == DinamicTestFieldType.HERO_MOVESPEED)
        {
            PlayerPrefs.SetFloat("HERO_MOVESPEED", slider.value);
        }
        else if (fieldType == DinamicTestFieldType.HERO_DAMAGE)
        {
            PlayerPrefs.SetFloat("HERO_DAMAGE", slider.value);
        }
        else if (fieldType == DinamicTestFieldType.ENEMY_DAMAGE)
        {
            PlayerPrefs.SetFloat("ENEMY_DAMAGE", slider.value);
        }
        else if (fieldType == DinamicTestFieldType.ENEMY_HP)
        {
            PlayerPrefs.SetFloat("ENEMY_HP", slider.value);
        }
        else if (fieldType == DinamicTestFieldType.CROSSHAIR_MOVESPEED)
        {
            PlayerPrefs.SetFloat("CROSSHAIR_MOVESPEED", slider.value);
        }
        else if (fieldType == DinamicTestFieldType.BARRIER_GARBAGE_HP)
        {
            PlayerPrefs.SetFloat("BARRIER_GARBAGE_HP", slider.value);
        }
        else if (fieldType == DinamicTestFieldType.BARRIER_STONE_HP)
        {
            PlayerPrefs.SetFloat("BARRIER_STONE_HP", slider.value);
        }

        PlayerPrefs.Save();

        text.text = slider.value.ToString();
    }
}
