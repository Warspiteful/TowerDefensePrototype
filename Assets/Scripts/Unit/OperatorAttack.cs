using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorAttack : MonoBehaviour
{

    private float _attackSpeed;
    private float _attackPower;
    private Vector2 _range;

    private float attacksPerSecond;

    [SerializeField] private Damageable targetEnemy;
    private List<Damageable> inRangeEnemies;
    private bool initialized = false;

    private Projectile projectilePrefab;

    [SerializeField] private GameObject attackTilePrefab;



    private OnValueChanged onAttack;
    private OnValueChanged onAttackEnd;

    private GameObject attackTileParent;

    private List<Vector2> attackTiles;

    
    public void Initialize(Vector2 range, float attackPower, Projectile projectile, Direction dir)
    {
        inRangeEnemies = new List<Damageable>();


        _attackPower = attackPower;
        initialized = true;

        attackTiles = new List<Vector2>();
        projectilePrefab = projectile;
        _range = range;
        
        GenerateAttackTiles(dir);
    }
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider collision)
    {
        
        if(initialized && collision.gameObject.CompareTag("Enemy") && collision.gameObject.GetComponent<Damageable>() != null)
        {
            if(targetEnemy == null){
                targetEnemy = collision.gameObject.GetComponent<Damageable>();
               targetEnemy.RegisterOnDeathCallback(GetNextEnemy);
                onAttack?.Invoke();
            }
            else
            {
                inRangeEnemies.Add(collision.gameObject.GetComponent<Damageable>());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Damageable exitingEnemy = other.GetComponent<Damageable>();

        if(targetEnemy != null && exitingEnemy != null){
            if (initialized && exitingEnemy == targetEnemy)
            {
                GetNextEnemy();
            }
            else
            {
                inRangeEnemies.Remove(exitingEnemy);
            }
        }
    }

    private void GetNextEnemy()
    {
        if(inRangeEnemies.Count == 0)
        {
            targetEnemy = null;
            onAttackEnd?.Invoke();
        }
        else
        {
            targetEnemy = inRangeEnemies[0];
            inRangeEnemies.Remove(targetEnemy);
        }
    }
    
    private void Attack()
    {
        if (targetEnemy != null)
        {
        

        if (projectilePrefab == null)
        {
            targetEnemy.TakeDamage(_attackPower);
        }
        else
        {
            Instantiate(projectilePrefab, transform).Initialize(targetEnemy.transform, _attackPower);
        }
        }
    }


    private void GenerateAttackTiles(Direction dir)
    {


        attackTileParent = gameObject;
        
        TryRenderAttack(0, 0);

        
        int x = Mathf.RoundToInt(_range.x);
        int y = Mathf.RoundToInt(_range.y);

        Debug.Log(x + ", " + y);
        int startX, endX, startY, endY;
        
        switch (dir)
        {
            case Direction.DOWN:
                startY = -x+1;
                endY = 0;
                startX = 1;
                endX = y;
                VerticalAttack(startX, endX, startY, endY);

                break;
            case Direction.LEFT:
                startX = -x;
                endX = -1;
                startY = 1;
                endY = y;
                HorizontalAttack(startX, endX, startY, endY);

                break;
            case Direction.UP :
                startY = 1;
                endY = x;
                startX = 1;
                endX = y;
                VerticalAttack(startX, endX, startY, endY);

                break;
            default:
                startX = 1;
                endX = x;
                startY = 1;
                endY = y;
                HorizontalAttack(startX, endX, startY, endY);
                break;
        }
        



    }
    
    private void VerticalAttack(int startX, int endX, int startY,int endY)
    {
        Debug.Log(startX +"," + endX + ", " + startY + ", " + endY);
        for (int y = startY; y <= endY; y++)
        {
            TryRenderAttack(0, y);
            Debug.Log(startX + ", " + endX / 2);
            
            for (int x = startX; x <=  Mathf.RoundToInt(endX / 2); x++)
            {
                TryRenderAttack(x,y);
                TryRenderAttack(-x,y);
            }
        }
    }

    private void HorizontalAttack(int startX, int endX, int startY,int endY)
    {
        Debug.Log(startX +"," + endX + ", " + startY + ", " + endY);

        for (int x = startX; x <= endX; x++)
        {
            TryRenderAttack(x, 0);

            for (int y = startY; y <= Mathf.RoundToInt(endY / 2); y++)
            {

                TryRenderAttack(x,y);
                TryRenderAttack(x,-y);
            }
        }
    }

    private void TryRenderAttack(int x,int y)
    {
        GameObject obj = Instantiate(attackTilePrefab, attackTileParent.transform);
        obj.transform.localPosition = new Vector3(x, 0, y-0.5f);
        obj.name = "Tile" + x + ", " + y;
        attackTiles.Add(new Vector2(x,y));
    }

    public void RegisterCallbacks(OnValueChanged onAttackFunction, OnValueChanged onAttackEndFunction)
    {
        onAttack += onAttackFunction;
        onAttackEnd += onAttackEndFunction;
    }

    public Vector2[] GetRange()
    {
        return attackTiles.ToArray();
    }
}
