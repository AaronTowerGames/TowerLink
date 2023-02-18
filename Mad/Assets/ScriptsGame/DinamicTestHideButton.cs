using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinamicTestHideButton : MonoBehaviour
{
    public void Click()
    {
        EventBus.Close.Invoke("DinamicTestCanvas");
    }
}
