using UnityEngine;

public class WeaponDatas : GSC<WeaponDatas>
{
    [SerializeField]
    private WeaponData[] _data = new WeaponData[10];

    private const string FILENAME = "WeaponDatas.json";

    public WeaponData Get(int id)
    {
        foreach (var gun in _data)
        {
            if (gun.id == id)
            {
                return gun;
            }
        }

        return null;
    }

    public WeaponData GetGunByName(string nameGun)
    {
        foreach (var gun in _data)
        {
            if (gun.nameWeaponLeftHand == nameGun)
            {
                return gun;
            }
            if (gun.nameWeaponRightHand == nameGun)
            {
                return gun;
            }
        }

        return null;
    }
    private void OnEnable()
    {
        EventBus.LoadWeaponDatas.Subscribe(Load);
        EventBus.SetWeaponData.Subscribe(Set);
    }

    private void OnDestroy()
    {
        EventBus.LoadEnemyDatas.Unsubscribe(Load);
        EventBus.SetWeaponData.Unsubscribe(Set);
    }

    private void Load()
    {
        DataLoadChecker.Instance.AddToCheck(GetType());
        /*
        if (FileController2.IsFileExist(FILENAME))
        {
            _data = FileController2.LoadJsonsData<WeaponData>(FILENAME);
        }/**/

        DatasLoaded(_data);
    }
    private void DatasLoaded(WeaponData[] data)
    {
        _data = data;
        DataLoadChecker.Instance.Loaded(GetType());
    }

    private void Set(int id)
    {
        var current = Get(id);

        EventBus.OnSetWeapon.Invoke(current);
    }

    private void OnApplicationQuit()
    {
        FileController2.SaveJsonsData(_data, FILENAME);
    }
}
