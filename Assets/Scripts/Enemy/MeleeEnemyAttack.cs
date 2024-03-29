using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeEnemyAttack : MonoBehaviour
{
    

    private OnValueChanged onAttack;
    private OnValueChanged onAttackEnd;

    private VoidBoolCallback isBlocked;
    private bool initialized = false;
    
    private int _attackPower;


    private OperatorAttack attack;
    
    [SerializeField] private Damageable targetEnemy;
    
    private List<Damageable> inRangeEnemies;


    private void OnDestroy()
    {
        if (attack != null)
        {
            attack.StopGuard();
        }
    }

    public void Initialize(int attackPower)
    {
        _attackPower = attackPower;
        initialized = true;
    }

    private void OnTriggerStay(Collider collision)
    {
        if(initialized && collision.gameObject.CompareTag("Operator") && collision.gameObject.GetComponentInParent<Damageable>() != null)
        {
            attack = collision.gameObject.GetComponentInParent<OperatorAttack>();
            if(attack != null && attack.CanGuard()){
                if(targetEnemy == null || !targetEnemy.isActiveAndEnabled){
                    targetEnemy = collision.gameObject.GetComponent<Damageable>();
                    onAttack?.Invoke();
                    isBlocked?.Invoke(true);
                    attack.RegisterAttacker(RemoveAttacker);
                    
                    
                }
            }
        }
    }

    private void RemoveAttacker()
    {
        targetEnemy = null;
    }



    private void OnTriggerExit(Collider other)
    {
        if(targetEnemy != null){
            if (initialized && other.gameObject == targetEnemy.gameObject)
            {
                isBlocked?.Invoke(false);
            }
        }
    }
    
    private void Attack()
    {
        if (targetEnemy != null && targetEnemy.isActiveAndEnabled)
        {
            targetEnemy.TakeDamage(_attackPower);
        }
        else
        {
            isBlocked.Invoke(false);
            onAttackEnd?.Invoke();
        }



    }
    
    public void RegisterCallbacks(OnValueChanged onAttackFunction, OnValueChanged onAttackEndFunction)
    {
        onAttack += onAttackFunction;
        onAttackEnd += onAttackEndFunction;
    }

    public void RegisterIsBlockedCallback(VoidBoolCallback isBlockedFunction)
    {
        isBlocked += isBlockedFunction;
    }
}
