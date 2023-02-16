using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpawnPoint : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;

    public Vector3 GetPosition()
    {
        return spawnPoint.position;
    }

    private void Start()
    {
        EventBus.SetHeroSpawnPoint.Invoke(this);
    }
}
