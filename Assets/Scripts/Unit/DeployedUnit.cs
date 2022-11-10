using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployedUnit : MonoBehaviour
{
    [SerializeField] private Direction _direction;
    
    //private Direction _direction;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    private OperatorData _operatorData;
    
    [SerializeField] private GameObject attackTilePrefab;


    [SerializeField] private int currentHealth;

    private List<GameObject> attackTiles;

   

    public void Initialize(OperatorData _operator)
    {
        _spriteRenderer.sprite = _operator.sprite;
        currentHealth = _operator.health;
        attackTiles = new List<GameObject>();
        _operatorData = _operator;

    }

    public Direction GetDirection()
    { 
        return _direction;
    }
    
    public Vector2 GetRange()
    { 
        return _operatorData.range;
    }



    private void GenerateAttackTiles()
    {
        attackTiles.Add(Instantiate(attackTilePrefab,this.transform));
        for (int x = Mathf.RoundToInt(-_operatorData.range.x/2); x < Mathf.RoundToInt(_operatorData.range.x/2); x++)
        {
            for (int y = Mathf.RoundToInt(-_operatorData.range.y/2); y < Mathf.RoundToInt(_operatorData.range.y/2); y++)
            {
                GameObject obj = Instantiate(attackTilePrefab, transform);
                obj.transform.localPosition = new Vector3(x + 1, obj.transform.position.y, y);
                attackTiles.Add(obj);
            }
        }
        
    }
}
