using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject _hero;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - _hero.transform.position;
    }

    void LateUpdate()
    {
        transform.position = _hero.transform.position + offset;
    }
}
