using System;
using UnityEngine;

public static class EventBus
{
    //AccauntLogic
    public static readonly Evt<int> GetExp = new Evt<int>();
    public static readonly Evt OnGetExp = new Evt();
    public static readonly Evt<int> LevelUp = new Evt<int>();
    public static readonly Evt<int> OnEarnValue = new Evt<int>();
    public static readonly Evt<MoneyType, int> OnSpendValue = new Evt<MoneyType, int>();

    //LevelLogicHelper
    public static readonly Evt OnSetHeroEnd = new Evt();
    public static readonly Evt OnSetLevelEnd = new Evt();
    public static readonly Evt OnSetEnemyCountEnd = new Evt();
    public static readonly Evt OnSetTimerEnd = new Evt();
    public static readonly Evt OnSetHeroSpawnSetEnd = new Evt();
    

    public static readonly Evt CheckStage1LevelLoadingEnd = new Evt();

    public static readonly Evt OnSetEnemyInSpawnEnd = new Evt();
    public static readonly Evt OnCreateHeroEnd = new Evt();
    
    public static readonly Evt CheckStage2LevelLoadingEnd = new Evt();



    //LevelLogic
    public static readonly Evt<HeroEquipment> SetHeroEquipment = new Evt<HeroEquipment>();
    public static readonly Evt OnSetHeroEquipment = new Evt();
    
    public static readonly Evt<HeroSpawnPoint> SetHeroSpawnPoint = new Evt<HeroSpawnPoint>();
    
    public static readonly Evt StartLevelDataLoadChecker = new Evt();

    public static readonly Evt StartLevel = new Evt();

    public static readonly Evt<int> SetHero = new Evt<int>();
    
    public static readonly Evt LoadHub = new Evt();
    public static readonly Evt LoadHero = new Evt();
    public static readonly Evt<int> SetLevel = new Evt<int>();
    public static readonly Evt RestartLevel = new Evt();
    public static readonly Evt NextLevel = new Evt();
    public static readonly Evt<LevelData> OnSetLevel = new Evt<LevelData>();

    public static readonly Evt<EnemyPool> SetEnemyPool = new Evt<EnemyPool>();
    

    public static readonly Evt<int> SetHeroDatas = new Evt<int>();
    public static readonly Evt<HeroData> OnSetHero = new Evt<HeroData>();

    public static readonly Evt<int> SetEnemyData = new Evt<int>();
    public static readonly Evt<EnemyData> OnSetEnemy = new Evt<EnemyData>();

    public static readonly Evt<int> SetWeaponData = new Evt<int>();
    public static readonly Evt<WeaponData> OnSetWeapon = new Evt<WeaponData>();
    

    public static readonly Evt<int> CalcHeroPositions = new Evt<int>();
    public static readonly Evt<int> OnChangeLevelTime = new Evt<int>();
    public static readonly Evt EndLevelTime = new Evt();
    public static readonly Evt LevelWin = new Evt();
    public static readonly Evt LevelLose = new Evt();


    //LocationLogic
    public static readonly Evt<SpawnPoint> AddSpawnPoint = new Evt<SpawnPoint>();
    public static readonly Evt<SpawnPoint> OnSpawnPointDestroy = new Evt<SpawnPoint>();
    public static readonly Evt<SpawnPoint> OnSpawnPointOpen = new Evt<SpawnPoint>();
    public static readonly Evt<SpawnPoint> OnSpawnPointClose = new Evt<SpawnPoint>();

    public static readonly Evt<EnemyData> SpawnStartEnemy = new Evt<EnemyData>();
    public static readonly Evt OnNeedNextEnemy = new Evt();   
    
    public static readonly Evt<int> AddEnemy = new Evt<int>();
    public static readonly Evt<EnemyData> OnAddEnemy = new Evt<EnemyData>();

    public static readonly Evt<Enemy> OnAddEnemyInScene = new Evt<Enemy>();    
    public static readonly Evt<Enemy> EnemyDie = new Evt<Enemy>();


    //Equipment
    public static readonly Evt<string> GetGunLeftArm = new Evt<string>();
    public static readonly Evt<string> GetGunRightArm = new Evt<string>();
    

    //Raycast
    public static readonly Evt<GameObject, int> Hit = new Evt<GameObject, int>();

    //Controller
    public static readonly Evt MoveLeftButtonClicked = new Evt();
    public static readonly Evt MoveRightButtonClicked = new Evt();
    public static readonly Evt FireButtonClicked = new Evt();
    public static readonly Evt AutoFireOn = new Evt();
    public static readonly Evt AutoFireOff = new Evt();
    public static readonly Evt HeroUP = new Evt();
    public static readonly Evt HeroDOWN = new Evt();
    



    //Load
    public static readonly Evt<DataLoadCheckerType> StartDataLoadChecker = new Evt<DataLoadCheckerType>();
    public static readonly Evt LoadTranslates = new Evt();
    public static readonly Evt LoadAccauntData = new Evt();
    public static readonly Evt LoadHeroDatas = new Evt();
    public static readonly Evt LoadEnemyDatas = new Evt();
    public static readonly Evt LoadWeaponDatas = new Evt();
    public static readonly Evt LoadLevelDatas = new Evt();

    public static readonly Evt LoadItems = new Evt();
    

    //OnLoaded
    public static readonly Evt<DataLoadCheckerType> OnDatasLoaded = new Evt<DataLoadCheckerType>();
    //public static readonly Evt OnLoadedTranslates = new Evt();
    //public static readonly Evt OnLoadAccauntData = new Evt();

    //Set
    public static readonly Evt SetAccauntDatas = new Evt();
    

    //OnSet
    public static readonly Evt<AccauntData> OnSetAccauntDatas = new Evt<AccauntData>();

    //Create
    public static readonly Evt CreateItems = new Evt();

    //OnCreate
    public static readonly Evt<ItemData> OnCreateItem = new Evt<ItemData>();
    

    //ELSE
    public static readonly Evt<string> ShowNotice = new Evt<string>();
    public static readonly Evt<string> GetAudio = new Evt<string>();

    //Joystick
    public static readonly Evt<Vector2> JoystickMove = new Evt<Vector2>();
    public static readonly Evt JoystickMoveActivated = new Evt();
    public static readonly Evt JoystickMoveDeactivated = new Evt();
    //Joystick

    //Translater
    public static readonly Evt OnChangeLanguage = new Evt();
    public static readonly Evt OnSetNewLanguage = new Evt();
    //Translater

    //FileManager
    public static readonly Evt LoadAllResFromData = new Evt();    
    public static readonly Evt<string> OnFileDownload = new Evt<string>();
    public static readonly Evt<string> UpdateTexture = new Evt<string>();
    public static readonly Evt<string> UpdateAudio = new Evt<string>();
    //FileManager

    //SoundManager
    public static readonly Evt<string> PlaySound = new Evt<string>();
    //SoundManager

    //CanvasManager
    public static readonly Evt<Canvas> AddCanvas = new Evt<Canvas>();
    public static readonly Evt<string> Close = new Evt<string>();
    public static readonly Evt<string> Show = new Evt<string>();
    //CanvasManager

    //SceneManager
    public static readonly Evt<int> LoadScene = new Evt<int>();
    public static readonly Evt<int> OnLoadedScene = new Evt<int>();
    //SceneManager
}

public class Evt
{
    private string _name { get; set; }

    public Evt() { }

    public Evt(string Name) 
    { 
        _name = Name;
    }

    private event Action Act = delegate { };

    public void Invoke() 
    { 
        Act.Invoke();
    }

    public void Subscribe(Action subscriber)
    {
        Unsubscribe(subscriber);
        Act += subscriber;
    }

    public void Unsubscribe(Action subscriber)
    {
        Act -= subscriber;
    }
}

public class Evt<T>
{
    private string _name { get; set; }
    public T _data;

    public Evt() { }

    public Evt(string Name, T data)
    {
        _name = Name;
        _data = data;
    }

    private event Action<T> Act = delegate { };

    public void Invoke()
    {
        Act.Invoke(_data);
    }

    public void Invoke(T param)
    {
        Act.Invoke(param);
    }

    public void Subscribe(Action<T> subscriber)
    {
        Unsubscribe(subscriber);
        Act += subscriber;
    }

    public void Unsubscribe(Action<T> subscriber)
    {
        Act -= subscriber;
    }
}


public class Evt<T,Q>
{
    private event Action<T,Q> Act = delegate { };

    public void Invoke(T param, Q param2)
    {
        Act.Invoke(param, param2);
    }

    public void Subscribe(Action<T, Q> subscriber)
    {
        Unsubscribe(subscriber);
        Act += subscriber;
    }

    public void Unsubscribe(Action<T, Q> subscriber)
    {
        Act -= subscriber;
    }

    internal void Invoke(IDamageble damageble, object damage)
    {
        throw new NotImplementedException();
    }

    internal void Subscribe(object setOffSize)
    {
        throw new NotImplementedException();
    }
}

public class Evt<T, Q, W>
{
    private event Action<T, Q, W> Act = delegate { };

    public void Invoke(T param, Q param2, W param3)
    {
        Act.Invoke(param, param2, param3);
    }

    public void Subscribe(Action<T, Q, W> subscriber)
    {
        Unsubscribe(subscriber);
        Act += subscriber;
    }

    public void Unsubscribe(Action<T, Q, W> subscriber)
    {
        Act -= subscriber;
    }
}

/*
static class EventProxy
{
    //void delegates with no parameters
    static public Delegate Create(EventInfo evt, Action d)
    {
        var handlerType = evt.EventHandlerType;
        var eventParams = handlerType.GetMethod("Invoke").GetParameters();

        //lambda: (object x0, EventArgs x1) => d()
        var parameters = eventParams.Select(p => Expression.Parameter(p.ParameterType, "x"));
        var body = Expression.Call(Expression.Constant(d), d.GetType().GetMethod("Invoke"));
        var lambda = Expression.Lambda(body, parameters.ToArray());
        return Delegate.CreateDelegate(handlerType, lambda.Compile(), "Invoke", false);
    }

    //void delegate with one parameter
    static public Delegate Create<T>(EventInfo evt, Action<T> d)
    {
        var handlerType = evt.EventHandlerType;
        var eventParams = handlerType.GetMethod("Invoke").GetParameters();

        //lambda: (object x0, ExampleEventArgs x1) => d(x1.IntArg)
        var parameters = eventParams.Select(p => Expression.Parameter(p.ParameterType, "x")).ToArray();
        var arg = getArgExpression(parameters[1], typeof(T));
        var body = Expression.Call(Expression.Constant(d), d.GetType().GetMethod("Invoke"), arg);
        var lambda = Expression.Lambda(body, parameters);
        return Delegate.CreateDelegate(handlerType, lambda.Compile(), "Invoke", false);
    }

    //returns an expression that represents an argument to be passed to the delegate
    static Expression getArgExpression(ParameterExpression eventArgs, Type handlerArgType)
    {
        if (eventArgs.Type == typeof(ExampleEventArgs) && handlerArgType == typeof(int))
        {
            //"x1.IntArg"
            var memberInfo = eventArgs.Type.GetMember("IntArg")[0];
            return Expression.MakeMemberAccess(eventArgs, memberInfo);
        }

        throw new NotSupportedException(eventArgs + "->" + handlerArgType);
    }
}
*/