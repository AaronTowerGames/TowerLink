using UnityEngine;

public class LevelSystem2 : GSC<LevelSystem2>
{
    private int _level;
    private int _newlevel;
    private int _exp;
    private int _expForLevelUp;
    private int _expThisLevel;
    private float _expDelta; //Процент для Fill image

    private void OnEnable()
    {
        EventBus.GetExp.Subscribe(AddExp);
        EventBus.OnSetAccauntDatas.Subscribe(SetData);
        //EventBus.OnGetReward.Subscribe(LevelUp);
        //EventBus.OnLevelUp.Subscribe(Blank); //Заглушка
    }

    private void OnDestroy()
    {
        EventBus.GetExp.Unsubscribe(AddExp);
        EventBus.OnSetAccauntDatas.Unsubscribe(SetData);
        //EventBus.OnGetReward.Unsubscribe(LevelUp);
        //EventBus.OnLevelUp.Unsubscribe(Blank); //Заглушка
    }

    private void SetData(AccauntData obj)
    {
        _exp = obj.exp;
        _level = CalcLevel();
    }

    private void AddExp(int value)
    {
        _exp += value;

        if (_exp >= _expForLevelUp)
        {
            _newlevel = CalcLevel();
            LevelUp();
        }

        //EventBus.OnChangeExpDelta.Invoke(_expDelta);
        EventBus.OnGetExp.Invoke();
    }

    private int CalcLevel()
    {
        var level = 2;
        _expThisLevel = 0;
        _expForLevelUp = GetExpForLevelUp(level);

        while (_exp >= _expForLevelUp)
        {
            _expThisLevel = _expForLevelUp;
            level++;
            _expForLevelUp = GetExpForLevelUp(level);
        }

        _expDelta = (_exp - _expThisLevel) / (float)(_expForLevelUp - _expThisLevel);

        return (level-1);
    }

    private int GetExpForLevelUp(int level)
    {
        return CoreMath.Instance.GetExpForLevelUp(level);
    }

    private void LevelUp()
    {
        if (_level < _newlevel)
        {
            _level++;
            EventBus.LevelUp.Invoke(_level);
        }
    }

    /*
    private void Blank(int obj) //Заглушка
    {
        EventBus.OnGetReward.Invoke();
    }
    */
}
