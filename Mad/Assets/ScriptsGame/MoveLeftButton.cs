using UnityEngine;

public class MoveLeftButton : MonoBehaviour
{
    public void Click()
    {
        EventBus.MoveLeftButtonClicked.Invoke();
    }
}
