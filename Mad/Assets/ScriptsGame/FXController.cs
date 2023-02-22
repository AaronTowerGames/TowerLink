using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXController : MonoBehaviour
{
    [SerializeField]
    private FXBlood _prefabFXBlood;

    private void OnEnable()
    {
        EventBus.FXRun.Subscribe(FXRun);
    }

    private void FXRun(FXTypes type, Vector3 vector)
    {
        switch (type)
        {
            case FXTypes.Blood:
                CreateFXBlood(vector);
                break;
            case FXTypes.BlockCrash:
                break;
            case FXTypes.FireShoot:
                break;
            default:
                break;
        }
    }

    private void CreateFXBlood(Vector3 vector)
    {
        //var obj = FactoryAbstractHandler.Instance.CreateEnemyPistolMan();
        var obj = Instantiate(_prefabFXBlood, vector, Quaternion.identity);
        Debug.Log("CREATE BLOOD");
        StartCoroutine(DesctroyBlood(obj));
    }

    private IEnumerator DesctroyBlood(FXBlood fXBlood)
    {
        yield return new WaitForSeconds(2f);
        Destroy(fXBlood.gameObject);
    }
}
