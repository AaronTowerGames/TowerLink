using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinamicTestShowButton : MonoBehaviour
{
    public void Click()
    {
        EventBus.Show.Invoke("DinamicTestCanvas");
    }
}
