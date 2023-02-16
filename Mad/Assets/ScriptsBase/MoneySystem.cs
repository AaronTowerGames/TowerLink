using System.Collections.Generic;

public class MoneySystem : GSC<MoneySystem>
{
    private Dictionary<MoneyType, int> wallet = new Dictionary<MoneyType, int>(3);

    public bool CanBuy(MoneyType type, int value)
    {
        if (wallet[type] >= value)
        {
            return true;
        }
        return false;
    }
    /*
    private void OnEnable()
    {
        EventBus.OnSetAccauntDatas.Subscribe(SetData);
        //EventBus.OnRefreshUserDatas.Subscribe(RefreshData);
        EventBus.OnEarnValue.Subscribe(Add);
        EventBus.OnSpendValue.Subscribe(Sub);
    }

    private void OnDestroy()
    {
        EventBus.OnSetAccauntDatas.Unsubscribe(SetData);
        //EventBus.OnRefreshUserDatas.Unsubscribe(RefreshData);
        EventBus.OnEarnValue.Unsubscribe(Add);
        EventBus.OnSpendValue.Unsubscribe(Sub);
    }

    private void Sub(MoneyType arg1, int arg2)
    {
        wallet[arg1] -= arg2;
        ChangeValue(arg1);
    }

    private void Add(MoneyType arg1, int arg2)
    {
        wallet[arg1] += arg2;
        ChangeValue(arg1);
    }

    /*
    private void RefreshData(AccauntData data)
    {
        wallet[MoneyType.Coin] = wallet[MoneyType.Coin] - startwallet[MoneyType.Coin]+data.Coin;
        wallet[MoneyType.Dollar] = wallet[MoneyType.Dollar] - startwallet[MoneyType.Dollar] + data.Dollar;
        wallet[MoneyType.Diamond] = wallet[MoneyType.Diamond] - startwallet[MoneyType.Diamond] + data.Diamond;
        startwallet[MoneyType.Coin] = data.Coin;
        startwallet[MoneyType.Dollar] = data.Dollar;
        startwallet[MoneyType.Diamond] = data.Diamond;

        ChangeValue(MoneyType.Coin);
        ChangeValue(MoneyType.Dollar);
        ChangeValue(MoneyType.Diamond);
    }/**/
    /*
    private void SetData(AccauntData data)
    {
        wallet[MoneyType.Baks] = data.;
        wallet[MoneyType.Amplifier] = data.Dollar;
        wallet[MoneyType.Implant] = data.Diamond;
        startwallet[MoneyType.Baks] = data.Coin;
        startwallet[MoneyType.Amplifier] = data.Dollar;
        startwallet[MoneyType.Implant] = data.Diamond;
        
        ChangeValue(MoneyType.Baks);
        ChangeValue(MoneyType.Amplifier);
        ChangeValue(MoneyType.Implant);
    }

    private void ChangeValue(MoneyType type)
    {
        EventBus.OnChangeValue.Invoke(type, wallet[type]);
    }/**/
}
