using UnityEngine;

public class LevelStartButton : MonoBehaviour
{
    public void Click()
    {
        EventBus.Close.Invoke("PauseCanvas");
        EventBus.StartLevel.Invoke();
    }
}
