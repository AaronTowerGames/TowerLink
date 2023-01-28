using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour, IDamageble
{
    private void OnEnable()
    {
        EventBus.Hit.Subscribe(GetDamage);
    }

    private void OnDisable()
    {
        EventBus.Hit.Subscribe(GetDamage);
    }

    private void GetDamage(IDamageble damageble, int damage)
    {
        if (damageble == this)
            Debug.Log("Pitun");
    }
}
