using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    [SerializeField] private List<Tile> _tiles;
    
    [SerializeField] private GameObject testObject;

    private GameObject testObjectParent;

    private Tile[][] _tileGrid;

    // Start is called before the first frame update
    void Start()
    {
        _tileGrid = new Tile[transform.childCount][];
        for(int i = 0; i < transform.childCount; i++)
        {
            Transform currTransform = transform.GetChild(i);
            _tileGrid[i] = new Tile[currTransform.childCount];

            for(int j = 0; j < currTransform.childCount; j++){
                _tileGrid[i][j] = currTransform.GetChild(j).gameObject.GetComponent<Tile>();
                _tileGrid[i][j].SetCoordinates(new Tuple<int,int>(i,j));
                _tileGrid[i][j].RegisterAttckCallback(RenderAttack);
            }
        }
  
    }

    private void RenderAttack(Tile _tile)
    {
        Tuple<int, int> tileLocation = _tile.GetCoordinates();

        Vector2 attackRange = _tile.GetAttackRange();

        Direction dir = _tile.GetDirection();

        List<Vector2> attackGrid = new List<Vector2>();

        Tuple<int, int> tileCoordinate = _tile.GetCoordinates();

        Tile currTile;
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
        Debug.Log(xVal + ", " + yVal);
        if(xVal < _tileGrid.Length && xVal > 0 && yVal < _tileGrid[xVal].Length && yVal > 0)
        {
        _tileGrid[coordinate.Item1 + xOffset][coordinate.Item2 + yOffset].RenderAttackDisplay();
    
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
