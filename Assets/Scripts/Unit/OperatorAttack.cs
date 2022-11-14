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
    private Animator _animator;
    protected AnimatorOverrideController animatorOverrideController;

    [SerializeField] private GameObject attackTilePrefab;

    private List<GameObject> attackTiles;


    private OnValueChanged onAttack;
    private OnValueChanged onAttackEnd;

    
    public void Initialize(Vector2 range, float attackPower, Projectile projectile)
    {
        inRangeEnemies = new List<Damageable>();


        attackTiles = new List<GameObject>();
        _attackPower = attackPower;
        initialized = true;
        
        
        projectilePrefab = projectile;
        _range = range;
        
        GenerateAttackTiles();
    }
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider collision)
    {
        
        if(initialized && collision.gameObject.CompareTag("Enemy") && collision.gameObject.GetComponent<Damageable>() != null)
        {
            if(targetEnemy == null){
                targetEnemy = collision.gameObject.GetComponent<Damageable>();
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
        if(targetEnemy != null){
            if (initialized && other.gameObject == targetEnemy.gameObject)
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(targetEnemy!=null && initialized){
            Debug.DrawLine(transform.position, targetEnemy.transform.position,Color.red);
        }
        
        
    }

    private void Attack()
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


    private void GenerateAttackTiles()
    {
        
        TryRenderAttack(0, 0);
        
        for (int x = 1; x <= Mathf.RoundToInt(_range.x); x++)
        {
            TryRenderAttack(x, 0);

            for (int y = 1; y < Mathf.RoundToInt(_range.y / 2); y++)
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

    public void RegisterCallbacks(OnValueChanged onAttackFunction, OnValueChanged onAttackEndFunction)
    {
        onAttack += onAttackFunction;
        onAttackEnd += onAttackEndFunction;
    }
}
