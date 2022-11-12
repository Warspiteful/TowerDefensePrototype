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

    private OperatorAttack attack;

   

    public void Initialize(OperatorData _operator)
    {
        _spriteRenderer.sprite = _operator.sprite;
        currentHealth = _operator.health;
        attackTiles = new List<GameObject>();
        _operatorData = _operator;

        attack = GetComponent<OperatorAttack>();
        attack.Initialize(_operator.atkSpeed, _operator.atkPower,_operator.attackAnimation, _operator.projectile );
        GenerateAttackTiles();

    }

    public Direction GetDirection()
    { 
        return _direction;
    }
    
    public Vector2 GetRange()
    { 
        return _operatorData.range;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("AAAH");
        }
    }



    private void GenerateAttackTiles()
    {
        
        TryRenderAttack(0, 0);
        
        for (int x = 1; x <= Mathf.RoundToInt(_operatorData.range.x); x++)
        {
            TryRenderAttack(x, 0);

            for (int y = 1; y < Mathf.RoundToInt(_operatorData.range.y / 2); y++)
            {

                TryRenderAttack(x,y);
                TryRenderAttack(x,-y);
            }
        }
  
        
    }

    private void TryRenderAttack(int x,int y)
    {
        GameObject obj = Instantiate(attackTilePrefab, transform);
        obj.transform.localPosition = new Vector3(x, 0, y-0.5f);
        obj.name = "Tile" + x + ", " + y;
        attackTiles.Add(obj);
    }
}
