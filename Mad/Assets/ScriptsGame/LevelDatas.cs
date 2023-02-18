using System;
using UnityEngine;

public class LevelDatas : GSC<LevelDatas>
{
    [SerializeField]
    private LevelData[] _data = new LevelData[10];

    private const string FILENAME = "LevelDatas.json";

    private int _currentLevel = 1;

    public LevelData Get(int level)
    {
        foreach (var item in _data)
        {
            if (item.level == level)
            {
                return item;
            }
        }

        return null;
    }

    private void OnEnable()
    {
        EventBus.LoadLevelDatas.Subscribe(Load);
        EventBus.SetLevel.Subscribe(LoadLevel);
        EventBus.RestartLevel.Subscribe(RestartLevel);
        EventBus.NextLevel.Subscribe(NextLevel);
    }

    private void OnDestroy()
    {
        EventBus.LoadLevelDatas.Unsubscribe(Load);
        EventBus.SetLevel.Unsubscribe(LoadLevel);
        EventBus.RestartLevel.Unsubscribe(RestartLevel);
        EventBus.NextLevel.Unsubscribe(NextLevel);
    }

    private void NextLevel()
    {
        LoadLevel(_currentLevel + 1);
    }

    private void RestartLevel()
    {
        LoadLevel(_currentLevel);
    }

    private void Load()
    {
        DataLoadChecker.Instance.AddToCheck(GetType());
        /*
        if (FileController2.IsFileExist(FILENAME))
        {
            _data = FileController2.LoadJsonsData<LevelData>(FILENAME);
        }/**/

        DatasLoaded(_data);
    }
    private void DatasLoaded(LevelData[] data)
    {
        _data = data;
        DataLoadChecker.Instance.Loaded(GetType());
    }

    private void LoadLevel(int level)
    {
        _currentLevel = level;
        var currentLevel = Get(_currentLevel);

        EventBus.OnSetLevel.Invoke(currentLevel);
        //EventBus.CalcHeroPositions.Invoke(currentLevel._countPoss);
    }

    private void OnApplicationQuit()
    {
        FileController2.SaveJsonsData(_data, FILENAME);
    }
}
