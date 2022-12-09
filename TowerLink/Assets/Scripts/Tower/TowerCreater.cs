using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCreater : MonoBehaviour
{
    [SerializeField]
    private List<Tower> _towerList = new List<Tower>();

    [SerializeField]
    private Transform _transform;
    public void CreateTower()
    {
        Instantiate(_towerList[0]._data.ModelTower, _transform);
    }

    public void SetPosition (Transform transform)
    {
        _transform = transform;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) )
        {
            CreateTower();
        }
    }
}
