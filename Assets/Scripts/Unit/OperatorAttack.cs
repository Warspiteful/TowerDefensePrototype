using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorAttack : MonoBehaviour
{

    private float _attackSpeed;
    private float _attackPower;

    private float attacksPerSecond;

    [SerializeField] private Damageable targetEnemy;
    private List<Damageable> inRangeEnemies;
    private bool initialized = false;

    private Projectile projectilePrefab;
    private Animator _animator;
    protected AnimatorOverrideController animatorOverrideController;
    
    
    public void Initialize(float attackPower, AnimationClip attackAnimation, Projectile projectile)
    {
        inRangeEnemies = new List<Damageable>();

        _animator = GetComponent<Animator>();
        animatorOverrideController =
            new AnimatorOverrideController(_animator.runtimeAnimatorController);
        _animator.runtimeAnimatorController = animatorOverrideController;
        animatorOverrideController["AttackPlaceholder"] = attackAnimation;
        

        _attackPower = attackPower;
        initialized = true;
        
        
        projectilePrefab = projectile;
        attacksPerSecond = 1 / attackAnimation.length;
    }
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider collision)
    {
        
        if(initialized && collision.gameObject.CompareTag("Enemy") && collision.gameObject.GetComponent<Damageable>() != null)
        {
            if(targetEnemy == null){
                targetEnemy = collision.gameObject.GetComponent<Damageable>();
                _animator.Play("Attack");
            }
            else
            {
                inRangeEnemies.Add(collision.gameObject.GetComponent<Damageable>());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (initialized && other.gameObject == targetEnemy.gameObject)
        {
            if(inRangeEnemies.Count == 0)
            {
                targetEnemy = null;
                _animator.Play("Idle");
            }
            else
            {
                targetEnemy = inRangeEnemies[0];
                inRangeEnemies.Remove(targetEnemy);
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
            Instantiate(projectilePrefab, transform).Initialize(targetEnemy.transform);
        }
    }
    
    

    private void RemoveEnemy()
    {
        
    }
}
