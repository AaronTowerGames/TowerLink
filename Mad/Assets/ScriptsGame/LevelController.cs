using UnityEngine;

public class LevelController : MonoBehaviour
{
    private int _currentLevel = 1;

    private void OnEnable()
    {
        EventBus.OnSetLevel.Subscribe(Set);
        EventBus.EndLevelTime.Subscribe(Lose);
        EventBus.LevelWin.Subscribe(Win);
        EventBus.LevelLose.Subscribe(Lose);
    }

    private void OnDestroy()
    {
        EventBus.OnSetLevel.Unsubscribe(Set);
        EventBus.EndLevelTime.Unsubscribe(Lose);
        EventBus.LevelWin.Unsubscribe(Win);
        EventBus.LevelLose.Unsubscribe(Lose);
    }

    private void Set(LevelData obj)
    {
        _currentLevel = obj.level;
        EventBus.OnSetLevelEnd.Invoke();
    }

    private void Win()
    {
        Debug.Log("WIN");
    }

    private void Lose()
    {
        Debug.Log("LOSE");
    }
}
