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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("STOLKNULSYA Colli");
        Enemy enemy;
        if (collision.gameObject.TryGetComponent<Enemy>(out enemy))
        {
            ClosePoint();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("STOLKNULSYA");
        Enemy enemy;
        if (other.TryGetComponent<Enemy>(out enemy))
        {
            Debug.Log("AUCH2");
            ClosePoint();
        }

        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("AUCH");
            ClosePoint();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("VYTOLKNULSYA");
        Enemy enemy;
        if (other.TryGetComponent<Enemy>(out enemy))
        {
            Debug.Log("WOOSH2");
            OpenPoint();
        }

        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("WOOSH");
            OpenPoint();
        }
    }
}
