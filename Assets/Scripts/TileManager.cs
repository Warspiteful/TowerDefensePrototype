using System;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    [SerializeField] private List<Tile> _tiles;
    
    [SerializeField] private GameObject testObject;

    private GameObject testObjectParent;

    private PlayerControls _controls;

    private Tile[][] _tileGrid;
    
    private Tile renderedTile;

    
    private delegate void tileOperation(Tuple<int, int> coordinate, int xOffset, int yOffset);

    // Start is called before the first frame update
    void Start()
    {
        _tileGrid = new Tile[transform.childCount][];
        for(int i = 0; i < transform.childCount; i++)
        {
            Transform currTransform = transform.GetChild(i);
            _tileGrid[i] = new Tile[currTransform.childCount];

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
    }

    private void RenderAttack(Tile _tile)
    {

        HandleAttackRender(_tile);
        if (renderedTile != null)
        {
            if(renderedTile == _tile)
            {
                renderedTile = null;
            }
            else
            {
                HandleAttackRender(renderedTile);
            }
        }
        
        renderedTile = _tile;
    }

    private void HandleAttackRender(Tile _tile)
    {
        //  Tuple<int, int> tileLocation = _tile.GetCoordinates();

        Vector2 attackRange = _tile.GetAttackRange();

        //Direction dir = _tile.GetDirection();

        //List<Vector2> attackGrid = new List<Vector2>();

        Tuple<int, int> tileCoordinate = _tile.GetCoordinates();

        //Tile currTile;
        for (int x = 1; x <= Mathf.RoundToInt(attackRange.x); x++)
        {
            TryRenderAttack(tileCoordinate,x,0);


            for (int y = 1; y < Mathf.RoundToInt(attackRange.y / 2); y++)
            {

                TryRenderAttack(tileCoordinate,x,y);
                TryRenderAttack(tileCoordinate,x,-y);
            }
        }
    }
    
    private void TryRenderAttack(Tuple<int,int> coordinate, int xOffset,int yOffset)
    {
        int xVal = coordinate.Item1 + xOffset;
        int yVal = coordinate.Item2 + yOffset;
        if(xVal < _tileGrid.Length && xVal >= 0 && yVal < _tileGrid[xVal].Length && yVal >= 0)
        {
        _tileGrid[coordinate.Item1 + xOffset][coordinate.Item2 + yOffset].RenderAttackDisplay();
    
        }
    }
}
