using System;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{

    [SerializeField] private Tile endTile;
    
    private Tile[][] grid;
    private IDictionary<Tile, List<Tile>> nodePaths;
    private List<Tile> path;

    public void Initialize(Tile[][] tileGrid)
    {
        grid = tileGrid;


    }

    public Vector3[] GetPath(Tile _startTile)
    {
        path = BreadthFirstSearch(_startTile);
        
        Vector3[] VectorPath = new Vector3[path.Count+1];

        VectorPath[0] = _startTile.gameObject.transform.parent.position;
        for (int i = 1; i < path.Count+1; i++)
        {
            VectorPath[i] = path[i - 1].transform.parent.position;

        }

        return VectorPath;
    }

    private struct TileNode
    {
        public Tile tile;
        public List<Tile> path;

        public TileNode(Tile _tile, List<Tile> _path)
        {
            tile = _tile;
            path = _path;
        }
    }
    private List<Tile> BreadthFirstSearch(Tile _startTile)
    {
        Queue<TileNode> frontier = new Queue<TileNode>();
        List<Tile> exploredNodes = new List<Tile>();
        List<Tile> expandedNodes = new List<Tile>();

        TileNode currTile = new TileNode(null, null);
        frontier.Enqueue(new TileNode(_startTile, new List<Tile>()));
        while (frontier.Count != 0)
        {
            currTile = frontier.Dequeue();

            if(currTile.tile != null && currTile.tile == endTile)
            {
                return currTile.path;
            }

            exploredNodes.Add(currTile.tile);
            foreach (Tile _tile in GetSuccessors(currTile.tile))
            {
                if (!exploredNodes.Contains(_tile) && !expandedNodes.Contains(_tile))
                {
                    List<Tile> newPath = currTile.path;
                    newPath.Add(_tile);
                    expandedNodes.Add(_tile);
                    frontier.Enqueue(new TileNode(_tile, newPath));
                }
            }
        }

        return null;
    }

    private List<Tile> GetSuccessors(Tile tile)
    {
        List<Tile> successors = new List<Tile>();
        Tuple<int, int> coordinate = tile.GetCoordinates();

        Tuple<int, int>[] testCoords = 
        {
            new Tuple<int, int>(coordinate.Item1 + 1,coordinate.Item2),
            new Tuple<int, int>(coordinate.Item1 - 1,coordinate.Item2),
            new Tuple<int, int>(coordinate.Item1,coordinate.Item2+1),
            new Tuple<int, int>(coordinate.Item1,coordinate.Item2-1),
        };
        
        foreach(Tuple<int,int> test in testCoords)
        {
        if (isWithinBounds(test.Item1, grid.Length) &&
            isWithinBounds(test.Item2, grid[test.Item1].Length) && grid[test.Item1][test.Item2].isWalkable())
            {
                successors.Add(grid[test.Item1][test.Item2]);
            }
        }
        return successors;
    }

    private bool isWithinBounds(int index, int length)
    {
        return index >= 0 && index < length;
    }
}
