using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusBar : MonoBehaviour
{
    [SerializeField]
    private ImageFiller _imageFiller;

    private void ChangeHP(int currentHp, int maxHp)
    {
        _imageFiller.SetFill(currentHp / (float)maxHp);
    }

    private void Start()
    {
        ChangeHP(13, 20);
    }
}
