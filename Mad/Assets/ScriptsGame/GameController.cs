using UnityEngine;

public class GameController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            EventBus.OnNeedNextEnemy.Invoke();
        }   
    }
}
