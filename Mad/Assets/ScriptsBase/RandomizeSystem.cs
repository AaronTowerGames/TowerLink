using UnityEngine;
public static class RandomizeSystem
{
    public static int GetRandom(int value)
    {
        //Random.InitState((int)Time.realtimeSinceStartup);
        return Random.Range(1, value);
    }

    public static float GetRandom(float value)
    {
        //Random.InitState((int)Time.realtimeSinceStartup);
        return Random.Range(1, value);
    }

    public static int GetRandomRange(int start, int end)
    {
        //Random.InitState((int)Time.realtimeSinceStartup);
        return Random.Range(start, end);
    }

    public static float GetRandomRange(float start, float end)
    {
        //Random.InitState((int)Time.realtimeSinceStartup);
        return Random.Range(start, end);
    }

    public static int GetRandomAround(int value, int delta)
    {
        //Random.InitState((int)Time.realtimeSinceStartup);
        return Random.Range(value - delta, value + delta);
    }

    public static float GetRandomAround(float value, float delta)
    {
        //Random.InitState((int)Time.realtimeSinceStartup);
        return Random.Range(value - delta, value + delta);
    }

    public static bool Chance(int success, int total)
    {
        //Random.InitState((int)Time.realtimeSinceStartup);
        return Random.Range(1, total) <= success;
    }
}
