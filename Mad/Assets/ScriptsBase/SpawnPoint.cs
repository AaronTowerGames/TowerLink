using System;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;
    private bool isClosedSpawn = false;

    public Vector3 GetPosition()
    {
        if (!isClosedSpawn)
            return spawnPoint.position;
        return Vector3.zero;
    }

    public void ClosePoint()
    {
        isClosedSpawn = true;
    }

    public void OpenPoint()
    {
        isClosedSpawn = false;
    }

    public bool IsClosed()
    {
        return isClosedSpawn;
    }

    private void Start()
    {
        EventBus.AddSpawnPoint.Invoke(this);
    }
}
