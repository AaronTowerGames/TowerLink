using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreMath : GSC<CoreMath>
{
    [SerializeField]
    private int multiplier = 10;

    [SerializeField]
    private float scale = 1.4f;

    public int GetExpForLevelUp(int level)
    {
        return (int)Mathf.Pow(level * multiplier, scale);
    }
}
