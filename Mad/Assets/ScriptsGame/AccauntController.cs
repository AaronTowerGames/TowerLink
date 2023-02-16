using System;
using UnityEngine;

public class AccauntController : GSC<AccauntController>
{
    [SerializeField]
    private AccauntData _data = new AccauntData();

    private const string FILENAME = "AccauntData.json";

    public int GetLastLevel()
    {
        if (_data != null)
        {
            return _data.lastlevel;
        }

        return 1;
    }
    public bool ChechIsLevelToLoad()
    {
        if (_data != null)
        {
            return _data.isLevelToLoad;
        }

        return true;
    }

    private void OnEnable()
    {
        EventBus.LoadAccauntData.Subscribe(Load);
        EventBus.SetAccauntDatas.Subscribe(SendAccauntData);
        EventBus.LoadHero.Subscribe(SetHero);
        EventBus.CheckStage1LevelLoadingEnd.Subscribe(SetWeaponHero);
    }

    private void OnDestroy()
    {
        EventBus.LoadAccauntData.Unsubscribe(Load);
        EventBus.SetAccauntDatas.Unsubscribe(SendAccauntData);
        EventBus.LoadHero.Unsubscribe(SetHero);
        EventBus.OnSetHeroEquipment.Unsubscribe(SetWeaponHero);
    }

    private void SetWeaponHero()
    {
        EventBus.GetGunLeftArm.Invoke(WeaponDatas.Instance.Get(_data.idWeaponLeftArm).nameWeaponLeftHand);
        EventBus.GetGunRightArm.Invoke(WeaponDatas.Instance.Get(_data.idWeaponRightArm).nameWeaponRightHand);
    }

    private void SetHero()
    {
        EventBus.SetHeroDatas.Invoke(_data.idHero);
    }

    private void SendAccauntData()
    {
        EventBus.OnSetAccauntDatas.Invoke(_data);

        EventBus.GetGunLeftArm.Invoke(DataSettings.GUN_L_REVOLVER);
        EventBus.GetGunRightArm.Invoke(DataSettings.GUN_R_REVOLVER);
    }

    private void Load()
    {
        DataLoadChecker.Instance.AddToCheck(GetType());

        /*
        if (FileController2.IsFileExist(FILENAME))
        {
            _data = FileController2.LoadJsonData<AccauntData>(FILENAME);
        }
        /**/
        if (_data != null)
        {
            DatasLoaded(_data);
            return;
        }

        SetDefaultData();

    }

    private void SetDefaultData()
    {
        _data = new AccauntData();
        _data.exp = 0;
        _data.id = 1;
        _data.reputation = 0;
        _data.idHero = 1;
        _data.idWeaponLeftArm = 1;
        _data.idWeaponRightArm = 1;
        _data.isLevelToLoad = true;
        DatasLoaded(_data);
    }

    private void DatasLoaded(AccauntData data)
    {
        _data = data;
        DataLoadChecker.Instance.Loaded(GetType());
    }

    private void OnApplicationQuit()
    {
        FileController2.SaveJsonData(_data, FILENAME);
    }
}
