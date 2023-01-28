using System;
using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private RectTransform _transform;
    private int damage = 10;
    [SerializeField]
    private bool isAutoFire = false;
    private float attackSpeed = 2f;

    private void Start()
    {
        _transform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        EventBus.FireButtonClicked.Subscribe(FireFromButton);
        EventBus.AutoFireOn.Subscribe(AutoFireOn);
        EventBus.AutoFireOff.Subscribe(AutoFireOff);
    }

    private void OnDisable()
    {
        EventBus.FireButtonClicked.Unsubscribe(FireFromButton);
        EventBus.AutoFireOn.Unsubscribe(AutoFireOn);
        EventBus.AutoFireOff.Unsubscribe(AutoFireOff);
    }

    private void AutoFireOn()
    {
        isAutoFire = true;
        StartCoroutine(AutoFire());
    }

    private void AutoFireOff()
    {
        isAutoFire = false;
        StopAllCoroutines();
    }

    private void FireFromButton()
    {
        Debug.Log("TRY FIRE");
        if (!isAutoFire)
        {
            Fire();
        }
    }
    private void Fire()
    {
        Debug.Log("FIRE");
        var p = new Vector3(_transform.position.x, _transform.position.y, 0);
        RaycastHit2D ray2d = Physics2D.Raycast(new Vector3 (p.x, p.y, 0), Vector2.zero);
        if (ray2d.collider != null)
        {
            IDamageble damageble;
            if (ray2d.collider.TryGetComponent<IDamageble>(out damageble))
            {
                Debug.Log("DAMAGU");
                EventBus.Hit.Invoke(damageble, damage);
            }
        }
    }

    private IEnumerator AutoFire()
    {
        while (isAutoFire)
        {
            yield return new WaitForSeconds(attackSpeed);
            Fire();
        }
    }
}
