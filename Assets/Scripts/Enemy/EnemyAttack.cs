using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    

    private OnValueChanged onAttack;
    private OnValueChanged onAttackEnd;

    private VoidBoolCallback isBlocked;
    private bool initialized = false;
    
    private float _attackPower;
    
    [SerializeField] private Damageable targetEnemy;


    public void Initialize(float attackPower)
    {
        _attackPower = attackPower;
        initialized = true;
    }
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.name);
        if(initialized && collision.gameObject.CompareTag("Operator") && collision.gameObject.GetComponentInParent<Damageable>() != null)
        {
            if(targetEnemy == null){
                targetEnemy = collision.gameObject.GetComponent<Damageable>();
                onAttack?.Invoke();
                isBlocked?.Invoke(true);

            }
        }
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
        if (targetEnemy != null)
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
