using System.Collections.Generic;

public class Inventory : GSC<Inventory>
{
    /*
    private Dictionary<ItemType, Dictionary<int, int>> _items = new Dictionary<ItemType, Dictionary<int, int>>(100);

    public bool Check(ItemType itemType, int id_item, int count)
    {
        if (!_items.ContainsKey(itemType))
        {
            return false;
        }

        if (!_items[itemType].ContainsKey(id_item))
        {
            return false;
        }

        if (_items[itemType][id_item] < count)
        {
            return false;
        }

        return true;
    }

    private void Add(ItemType itemType, int id_item, int count)
    {
        if (!_items.ContainsKey(itemType))
        {
            _items[itemType] = new Dictionary<int, int>(1);
        }
        if (!_items[itemType].ContainsKey(id_item))
        {
            _items[itemType][id_item] = new int();
        }
        _items[itemType][id_item] += count;

        EventBus.OnAddItemToInventory.Invoke(itemType, id_item, _items[itemType][id_item]);
    }


    private void Del(ItemType itemType, int id_item, int count)
    {
        if (!_items.ContainsKey(itemType))
        {
            return;
        }
        if (!_items[itemType].ContainsKey(id_item))
        {
            return;
        }
        _items[itemType][id_item] -= count;

        EventBus.OnSubItemToInventory.Invoke(itemType, id_item, _items[itemType][id_item]);
    }

    private void OnEnable()
    {
        EventBus.AddItem.Subscribe(Add);
        EventBus.SubItem.Subscribe(Del);
    }

    private void OnDestroy()
    {
        EventBus.AddItem.Unsubscribe(Add);
        EventBus.SubItem.Unsubscribe(Del);
    }

    public UserItemData[] ReturnItems()
    {
        List<UserItemData> usersItemData = new List<UserItemData>();

        foreach (var item in _items)
        {
            foreach (var _item in item.Value)
            {
                var userItemData = new UserItemData();
                userItemData.Id_item = _item.Key;
                userItemData.Count = _item.Value;

                userItemData.Id_item_type = ItemTypes.Instance.itemtypeids[item.Key];
                usersItemData.Add(userItemData);
            }
        }
        return usersItemData.ToArray();
    }/**/
}
