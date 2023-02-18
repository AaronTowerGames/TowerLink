using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour, IDamageble
{
    private BarrierData _data;

    public void SetData(BarrierData data)
    {
        _data = data;
    }

    private void OnEnable()
    {
        EventBus.Hit.Subscribe(GetDamage);
    }

    private void OnDisable()
    {
        EventBus.Hit.Subscribe(GetDamage);
    }

    private void GetDamage(GameObject barrier, int damage)
    {
        if (barrier == this.gameObject)
            Debug.Log("Pitun");
    }
}
