using UnityEngine;

public class MoveRightButton : MonoBehaviour
{
    public void Click()
    {
        EventBus.MoveRightButtonClicked.Invoke();
    }
}
