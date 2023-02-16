using UnityEngine;

public class EnemyDatas : GSC<EnemyDatas>
{
    [SerializeField]
    private EnemyData[] _data = new EnemyData[10];

    private const string FILENAME = "EnemyDatas.json";

    public EnemyData Get(EnemyType type)
    {
        if (_data != null)
        {
            foreach (var data in _data)
            {
                if (data.type == type)
                {
                    return data;
                }
            }
        }

        return null;
    }

    public EnemyData Get(int id)
    {
        if (_data != null)
        {
            foreach (var data in _data)
            {
                if (data.id == id)
                {
                    return data;
                }
            }
        }

        return null;
    }
    private void OnEnable()
    {
        EventBus.LoadEnemyDatas.Subscribe(Load);
        EventBus.SetEnemyData.Subscribe(SetEnemy);
    }

    private void OnDestroy()
    {
        EventBus.LoadEnemyDatas.Unsubscribe(Load);
        EventBus.SetEnemyData.Unsubscribe(SetEnemy);
    }

    private void Load()
    {
        DataLoadChecker.Instance.AddToCheck(GetType());
        /*
        if (FileController2.IsFileExist(FILENAME))
        {
            _data = FileController2.LoadJsonsData<EnemyData>(FILENAME);
        }/**/

        DatasLoaded(_data);
    }
    private void DatasLoaded(EnemyData[] data)
    {
        _data = data;
        DataLoadChecker.Instance.Loaded(GetType());
    }

    private void SetEnemy(int idEnemy)
    {
        var currentEnemy = Get(idEnemy);

        EventBus.OnSetEnemy.Invoke(currentEnemy);
    }

    private void OnApplicationQuit()
    {
        FileController2.SaveJsonsData(_data, FILENAME);
    }
}
