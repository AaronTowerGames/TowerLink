using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoadChecker : GSC<DataLoadChecker>
{
    private Dictionary<Type, bool> dataLoadedChecker = new Dictionary<Type, bool>();

    internal void SetToCheck(DataLoadCheckerType type)
    {
        if (type == DataLoadCheckerType.LevelStart)
        {
            
        }
    }

    internal void AddToCheck(Type type)
    {
        dataLoadedChecker[type] = false;
    }

    internal void Loaded(Type type)
    {
        dataLoadedChecker[type] = true;
    }

    private void OnEnable()
    {
        EventBus.StartDataLoadChecker.Subscribe(StartDataLoadChecker);
    }

    private void OnDestroy()
    {
        EventBus.StartDataLoadChecker.Unsubscribe(StartDataLoadChecker);
    }

    private void StartDataLoadChecker(DataLoadCheckerType type)
    {
        StartCoroutine(DataLoadedChecker(type));
    }

    private IEnumerator DataLoadedChecker(DataLoadCheckerType type)
    {
        while (dataLoadedChecker == null)
        {
            yield return new WaitForSeconds(DataSettings.DELAY_CHECK_LOADED);
        }

        bool allTrue = false;
        while (!allTrue)
        {
            allTrue = true;
            var countLoaded = 0;
            foreach (var item in dataLoadedChecker)
            {
                if (item.Value == false)
                {
                    allTrue = false;
                    break;
                }
                else
                {
                    countLoaded++;
                }
            }
            //EventBus.SetValueSliderLoader.Invoke(countLoaded / (float)dataLoadedChecker.Count);

            if (!allTrue)
                yield return new WaitForSeconds(DataSettings.DELAY_CHECK_LOADED);
        }

        EventBus.OnDatasLoaded.Invoke(type);
    }
}
