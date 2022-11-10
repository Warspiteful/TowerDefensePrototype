using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    [SerializeField] private List<Tile> _tiles;
    
    [SerializeField] private GameObject testObject;

    private GameObject testObjectParent;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Tile tile in _tiles)
        {
            tile.RegisterAttckCallback(RenderAttack);
        }
    }

    private void RenderAttack(Tile _tile)
    {
        Vector2 tileLocation = _tile.GetCoordinates();

        Vector2 attackRange = _tile.GetAttackRange();

        Direction dir = _tile.GetDirection();
        
        List<Vector2> attackGrid = new List<Vector2>();
        
        //Replace with Reference to Tiles

        if(testObjectParent == null ){
            testObjectParent = Instantiate(new GameObject("Parent"), _tile.transform);
            testObjectParent.transform.localPosition = new Vector3(0.5f, 1, 0.5f);
        }
        
        for (int x = 1; x <= Mathf.RoundToInt(attackRange.x); x++)
        {
             Instantiate(testObject, testObjectParent.transform).transform.localPosition =
                    new Vector3(x, 0, 0);
            for (int y = 1; y < Mathf.RoundToInt(attackRange.y/2); y++)
            {
                
                Instantiate(testObject, testObjectParent.transform).transform.localPosition =
                    new Vector3(x, 0, y);
                Instantiate(testObject, testObjectParent.transform).transform.localPosition =
                    new Vector3(x, 0, -y);
            }
        }

      
  
  

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
