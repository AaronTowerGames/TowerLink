using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField]
    private HeroData data = new HeroData();
    internal void SetData(HeroData heroData)
    {
        data = heroData;
    }
}
