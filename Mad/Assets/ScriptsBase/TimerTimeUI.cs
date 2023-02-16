using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerTimeUI : MonoBehaviour
{
    [SerializeField]
    private Text _timeTXT;

    private void OnEnable()
    {
        EventBus.OnChangeLevelTime.Subscribe(ChangeTime);
    }

    private void OnDisable()
    {
        EventBus.OnChangeLevelTime.Unsubscribe(ChangeTime);
    }

    private void ChangeTime(int time)
    { 
        var min = (time / 60).ToString("00.#");
        var sec = (time % 60).ToString("00.#");
        _timeTXT.text = $"{min}:{sec}";
    }
}
