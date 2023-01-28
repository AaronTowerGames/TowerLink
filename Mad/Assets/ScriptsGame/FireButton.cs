using UnityEngine;

public class FireButton : MonoBehaviour
{
    public void Click()
    {
        EventBus.FireButtonClicked.Invoke();
    }
}
