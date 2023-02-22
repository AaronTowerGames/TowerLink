using System;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [SerializeField]
    private HeroData _heroData = new HeroData();

    [SerializeField]
    private HeroSpawnPoint _spawnPoint = new HeroSpawnPoint();

    private void OnEnable()
    {
        EventBus.SetHeroSpawnPoint.Subscribe(SetHeroSpawn);
        EventBus.OnSetHero.Subscribe(Set);
        EventBus.CheckStage1LevelLoadingEnd.Subscribe(SetReadyToStage2Loading);
    }

    private void OnDestroy()
    {
        EventBus.SetHeroSpawnPoint.Unsubscribe(SetHeroSpawn);
        EventBus.OnSetHero.Unsubscribe(Set);
        EventBus.CheckStage1LevelLoadingEnd.Unsubscribe(SetReadyToStage2Loading);
    }

    private void SetHeroSpawn(HeroSpawnPoint obj)
    {
        _spawnPoint = obj;
        EventBus.OnSetHeroSpawnSetEnd.Invoke();
    }

    private void Set(HeroData obj)
    {
        _heroData = obj;
        EventBus.OnSetHeroEnd.Invoke();
    }

    private void SetReadyToStage2Loading()
    {
        var obj = FactoryAbstractHandler.Instance.CreateHero();
        obj.transform.position = _spawnPoint.GetPosition();
        obj.transform.SetParent(_spawnPoint.transform.parent);
        obj.SetData(_heroData);

        EventBus.OnCreateHeroEnd.Invoke();
    }
}
