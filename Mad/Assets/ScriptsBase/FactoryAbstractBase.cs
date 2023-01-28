using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FactoryAbstractBase<T> : MonoBehaviour
{
    public abstract T Create(T prefab);
}
