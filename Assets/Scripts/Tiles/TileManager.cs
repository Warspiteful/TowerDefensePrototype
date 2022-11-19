using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class TileManager : MonoBehaviour
{

    [SerializeField] private List<Tile> _tiles;
    
    [SerializeField] private GameObject testObject;

    private GameObject testObjectParent;

    private PlayerControls _controls;

    private Tile[][] _tileGrid;
    
    private Tile renderedTile;

    private Pathfinding _pathfindingExample;

    

    // Start is called before the first frame update
    void Start()
    {
        _pathfindingExample = GetComponent<Pathfinding>();
        
        _tileGrid = new Tile[transform.childCount][];
        for(int i = 0; i < transform.childCount; i++)
        {
            Transform currTransform = transform.GetChild(i);
            
            
            _tileGrid[i] = new Tile[currTransform.childCount];

            Debug.Log(currTransform.childCount);
            for(int j = 0; j < currTransform.childCount; j++)
            {
                Tile currTile = currTransform.GetChild(j).GetChild(0).gameObject
                    .GetComponent<Tile>();
                if (currTile != null)
                {
                    _tileGrid[i][j] = currTransform.GetChild(j).GetChild(0).gameObject.GetComponent<Tile>();
                    _tileGrid[i][j].gameObject.name = "Tile " + i + ", " + j;
                    _tileGrid[i][j].SetCoordinates(new Tuple<int,int>(i,j));
                    _tileGrid[i][j].RegisterAttckCallback(RenderAttack);
                }
            }
        }
        
        _pathfindingExample.Initialize(_tileGrid);
    }

    private void RenderAttack(Tile _tile)
    {
        Debug.Log(_tile);
        ResetDisplay();
        if (renderedTile == _tile)
        {
            renderedTile = null;
        }
        else{
            HandleAttackRender(_tile);
            renderedTile = _tile;
        }
        
    }

    private void ResetDisplay()
    {
        foreach (Tile[] tileList in _tileGrid)
        {
            foreach (Tile tile in tileList)
            {
                tile.HideAttackDisplay();
            }
        }
    }

    private void HandleAttackRender(Tile _tile)
    {
        //  Tuple<int, int> tileLocation = _tile.GetCoordinates();

        Vector2[] attackRange = _tile.GetAttackRange();

        //Direction dir = _tile.GetDirection();

        //List<Vector2> attackGrid = new List<Vector2>();

        Tuple<int, int> tileCoordinate = _tile.GetCoordinates();

        //Tile currTile;
        Vector2 currPos;
        foreach (Vector2 attackTile in attackRange)
        {
            
            currPos = new Vector2(attackTile.x + tileCoordinate.Item1,
                attackTile.y + tileCoordinate.Item2);
            TryRenderAttack(currPos);
        }

    }
    
    private void TryRenderAttack(Vector2 tileCoordinate)
    {

        int x = Mathf.RoundToInt(tileCoordinate.x);
        int y = Mathf.RoundToInt(tileCoordinate.y);

        if(x < _tileGrid.Length && x >= 0 && y < _tileGrid[x].Length && y >= 0)
        {
        _tileGrid[x][y].RenderAttackDisplay();
        }
    }
}
