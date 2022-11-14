using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{

    [SerializeField] private float currHealth;
    [SerializeField] private float maxHealth;

    private OnValueChanged _damageTaken;


    public void RegisterDamageTakenCallback(OnValueChanged damageCallback)
    {
        _damageTaken += damageCallback;
        foreach (OnValueChanged method in _damageTaken.GetInvocationList())
        {
            Debug.Log(method.ToString());
        }
    }
    
    public void Initialize(float health)
    {
        maxHealth = health;
        currHealth = maxHealth;
    }


    public void TakeDamage(float damage)
    {
        foreach (OnValueChanged method in _damageTaken.GetInvocationList())
        {
            Debug.Log(method.ToString());
        }
        currHealth -= damage;
        Debug.Log("Damage taken by Damageable");
        _damageTaken.Invoke();
        
    }

    public void Heal(float health)
    {
        
    }
}
