using UnityEngine;

public class GameBoardTile : MonoBehaviour
{
    [SerializeField]
    private Transform _arrow;

    private GameBoardTile _north, _east, _south, _west, _nextOnPath;

    private int _distance;

    private bool HasPath => _distance != int.MinValue;

    public bool IsAlternative { get; set; }

    private Quaternion _northRotation = Quaternion.Euler(90f,0f,0f);
    private Quaternion _eastRotation = Quaternion.Euler(90f, 90f, 0f);
    private Quaternion _southRotation = Quaternion.Euler(90f, 180f, 0f);
    private Quaternion _westRotation = Quaternion.Euler(90f, 270f, 0f);

    public static void MakeEastWestNeighbors(GameBoardTile east, GameBoardTile west) 
    {
        west._east = east;
        east._west = west;
    }

    public static void MakeNorthSouthNeighbors(GameBoardTile north, GameBoardTile  south)
    {
        south._north = north;
        north._south = south;
    }

    public void ClearPath()
    {
        _distance = int.MaxValue;
        _nextOnPath = null;
    }

    public void BecomeDestination ()
    {
        _distance = 0;
        _nextOnPath = null;
    }

    private GameBoardTile GrowPathTo(GameBoardTile neighbor)
    {
        if (!HasPath || neighbor == null || neighbor.HasPath)
        {
            return null;
        }

        neighbor._distance = _distance + 1;
        neighbor._nextOnPath = this;

        return neighbor;
    }

    public GameBoardTile GrowPathNorth() => GrowPathTo(_north);
    public GameBoardTile GrowPathEast() => GrowPathTo(_east);
    public GameBoardTile GrowPathWest() => GrowPathTo(_west);
    public GameBoardTile GrowPathSouth() => GrowPathTo(_south);


    public void ShowPath()
    {
        if (_distance == 0)
        {
            _arrow.gameObject.SetActive(false);
            return;
        }

        _arrow.gameObject.SetActive(true);
        _arrow.localRotation =
            _nextOnPath == _north ? _northRotation :
            _nextOnPath == _east ? _eastRotation :
            _nextOnPath == _south ? _southRotation :
            _westRotation;
    }
}
