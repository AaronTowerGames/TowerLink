using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private int time = 0;
    private IEnumerator timer;

    private void OnEnable()
    {
        EventBus.StartLevel.Subscribe(StartLevel);

        EventBus.OnSetLevel.Subscribe(SetTime);
        EventBus.LevelWin.Subscribe(StopTimer);
        EventBus.LevelLose.Subscribe(StopTimer);
    }

    private void OnDisable()
    {
        EventBus.StartLevel.Unsubscribe(StartLevel);

        EventBus.OnSetLevel.Unsubscribe(SetTime);
        EventBus.LevelWin.Unsubscribe(StopTimer);
        EventBus.LevelLose.Unsubscribe(StopTimer);
    }

    private void StartLevel()
    {
        StartCoroutine(timer);
    }

    private void StopTimer()
    {
        StopCoroutine(timer);
    }

    private void SetTime(LevelData obj)
    {
        timer = Cooldown();
        time = obj.roundTimeSeconds;

        EventBus.OnSetTimerEnd.Invoke();
    }

    private IEnumerator Cooldown()
    {
        while (time > 0)
        {
            EventBus.OnChangeLevelTime.Invoke(time);
            time--;
            yield return new WaitForSeconds(1);
        }

        EventBus.EndLevelTime.Invoke();
    }
}
