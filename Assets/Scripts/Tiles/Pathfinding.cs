using System;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{

    [SerializeField] private Tile StartTile;
    [SerializeField] private Tile endTile;
    
    private Tile[][] grid;
    private IDictionary<Tile, List<Tile>> nodePaths;
    public void Initialize(Tile[][] tileGrid)
    {
        grid = tileGrid;
        List<Tile> path = BreadthFirstSearch();
        Debug.Log(path.Count);

        foreach (Tile tile in path)
        {
            tile.WALKEXAMPLE();
        }
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
    private List<Tile> BreadthFirstSearch()
    {
        Queue<TileNode> frontier = new Queue<TileNode>();
        List<Tile> exploredNodes = new List<Tile>();
        List<Tile> expandedNodes = new List<Tile>();

        TileNode currTile = new TileNode(null, null);
        frontier.Enqueue(new TileNode(StartTile, new List<Tile>()));
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
