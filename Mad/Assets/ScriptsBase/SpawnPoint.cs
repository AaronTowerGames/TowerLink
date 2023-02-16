using System;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private bool isStartSpawn = false;

    [SerializeField]
    private int line;

    [SerializeField]
    private bool isClosedSpawn = false;

    internal bool IsStartPoint()
    {
        return isStartSpawn;
    }

    public int GetLine()
    {
        return line;
    }

    public Vector3 GetPosition()
    {
        if (!IsClosed())
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy;
        if (other.TryGetComponent<Enemy>(out enemy))
        {
            ClosePoint();
        }

        if (other.gameObject.tag == "Enemy")
        {
            ClosePoint();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Enemy enemy;
        if (other.TryGetComponent<Enemy>(out enemy))
        {
            OpenPoint();
        }

        if (other.gameObject.tag == "Enemy")
        {
            OpenPoint();
        }
    }
}
