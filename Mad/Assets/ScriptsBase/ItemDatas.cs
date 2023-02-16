using UnityEngine;

public class ItemDatas : GSC<ItemDatas>
{
    [SerializeField]
    private ItemData[] _data = null;

    private const string FILENAME = "ItemsData.json";

    //private readonly string JSON = "";

    public ItemData Get(int id)
    {
        foreach (var item in _data)
        {
            if (item.id == id)
            {
                return item;
            }
        }

        return null;
    }
    private void OnEnable()
    {
        EventBus.LoadItems.Subscribe(LoadData);
        EventBus.CreateItems.Subscribe(CreateItems);
    }

    private void OnDestroy()
    {
        EventBus.LoadItems.Unsubscribe(LoadData);
        EventBus.CreateItems.Unsubscribe(CreateItems);
    }

    private void LoadData()
    {
        _data = FileController2.LoadJsonsData<ItemData>(FILENAME);

        if (_data != null)
        {
            DatasLoaded(_data);
            return;
        }

        //_data = JsonHelper.FromJson<ItemData>(JSON);
        DatasLoaded(_data);
    }

    private void DatasLoaded(ItemData[] data)
    {
        _data = data;
        DataLoadChecker.Instance.Loaded(GetType());
        FileController2.SaveJsonsData(_data, FILENAME);
    }

    private void CreateItems() //Not used
    {
        for (int i = 0; i < _data.Length; i++)
        {
            EventBus.OnCreateItem.Invoke(_data[i]);
        }
    }
}
