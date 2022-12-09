using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField]
    private Vector2Int _boardSize;

    [SerializeField]
    private GameBoard _board;

    private void Start()
    {
        _board.Initialize(_boardSize);

    }
}
