using UnityEngine;

public class HeroDatas : GSC<HeroDatas>
{
    [SerializeField]
    private HeroData[] _data = new HeroData[10];

    private int _currentHero;

    private const string FILENAME = "HeroDatas.json";

    public HeroData Get(int id)
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
        EventBus.LoadHeroDatas.Subscribe(Load);
        EventBus.SetHeroDatas.Subscribe(SetHero);
    }

    private void OnDestroy()
    {
        EventBus.LoadHeroDatas.Unsubscribe(Load);
        EventBus.SetHeroDatas.Unsubscribe(SetHero);
    }

    private void Load()
    {
        DataLoadChecker.Instance.AddToCheck(GetType());
        /*
        if (FileController2.IsFileExist(FILENAME))
        {
            _data = FileController2.LoadJsonsData<HeroData>(FILENAME);
        }/**/

        DatasLoaded(_data);
    }
    private void DatasLoaded(HeroData[] data)
    {
        _data = data;
        DataLoadChecker.Instance.Loaded(GetType());
    }

    private void SetHero(int idHero)
    {
        _currentHero = idHero;
        var currentHero = Get(idHero);
        EventBus.OnSetHero.Invoke(currentHero);
    }

    private void OnApplicationQuit()
    {
        FileController2.SaveJsonsData(_data, FILENAME);
    }
}
