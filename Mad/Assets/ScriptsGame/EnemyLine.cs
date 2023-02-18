using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLine : MonoBehaviour
{
    [SerializeField]
    private int _line;

    private void Awake()
    {
        if (_line == 1)
        {
            transform.position = new Vector3(0, DataSettings.LINE1_POS_Y, DataSettings.LINE1_POS_Z);
        }
        else if (_line == 2)
        {
            transform.position = new Vector3(0, DataSettings.LINE2_POS_Y, DataSettings.LINE2_POS_Z);
        }
        else if (_line == 3)
        {
            transform.position = new Vector3(0, DataSettings.LINE3_POS_Y, DataSettings.LINE3_POS_Z);
        }
    }
}
