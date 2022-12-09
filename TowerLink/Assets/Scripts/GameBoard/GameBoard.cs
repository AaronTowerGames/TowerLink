using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField]
    private Transform _ground;

    [SerializeField] GameBoardTile _tilePrefab;

    private Vector2Int _size;

    private GameBoardTile[] _tiles;

    private Queue<GameBoardTile> _searchFrontier = new Queue<GameBoardTile>();

    public void Initialize(Vector2Int size)
    {
        _size = size;
        _ground.localScale = new Vector3(size.x, size.y, 1f);

        Vector2 offset = new Vector2((size.x - 1) * 0.5f, (size.y - 1) * 0.5f);

        _tiles = new GameBoardTile[size.x * size.y];

        for (int i = 0, y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++, i++)
            {
                GameBoardTile tile = _tiles[i] = Instantiate(_tilePrefab);
                tile.transform.SetParent(transform, false);
                tile.transform.localPosition = new Vector3(x - offset.x, 0f, y - offset.y);

                if (x > 0)
                {
                    GameBoardTile.MakeEastWestNeighbors(tile, _tiles[i - 1]);
                }

                if (y > 0)
                {
                    GameBoardTile.MakeNorthSouthNeighbors(tile, _tiles[i - size.x]);
                }
                /*
                tile.IsAlternative = (x & 1) == 0;
                if ((y & 1) == 0)
                {
                    tile.IsAlternative = !tile.IsAlternative;
                }*/
            }
        }

        FindPath();
    }

    public void FindPath()
    {
        foreach (GameBoardTile tile in _tiles)
        {
            tile.ClearPath();
        }

        int destinationIndex = _tiles.Length / 2;
        _tiles[destinationIndex].BecomeDestination();
        _searchFrontier.Enqueue(_tiles[destinationIndex]);

        while (_searchFrontier.Count > 0)
        {
            GameBoardTile tile = _searchFrontier.Dequeue();
            if (tile != null)
            {
                if (tile.IsAlternative)
                {
                    _searchFrontier.Enqueue(tile.GrowPathNorth());
                    _searchFrontier.Enqueue(tile.GrowPathSouth());
                    _searchFrontier.Enqueue(tile.GrowPathEast());
                    _searchFrontier.Enqueue(tile.GrowPathWest());
                }
                else
                {
                    _searchFrontier.Enqueue(tile.GrowPathWest());
                    _searchFrontier.Enqueue(tile.GrowPathEast());
                    _searchFrontier.Enqueue(tile.GrowPathSouth());
                    _searchFrontier.Enqueue(tile.GrowPathNorth());
                }
                
            }
        }

        foreach (GameBoardTile tile in _tiles)
        {
            tile.ShowPath();
        }
    }

}