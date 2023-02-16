using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationController : MonoBehaviour
{
    private void A()
    {
        EventBus.OnNeedNextEnemy.Invoke();
    }
}
