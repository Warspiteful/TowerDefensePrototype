using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{

    [SerializeField] private float currHealth;
    [SerializeField] private float maxHealth;

    private OnValueChanged damageTaken;


    public void RegisterDamageTakenCallback(OnValueChanged function)
    {
        damageTaken += function;
    }
    public void Initialize(float health)
    {
        maxHealth = health;
        currHealth = maxHealth;
    }


    public void TakeDamage(float damage)
    {
        currHealth -= damage;
        damageTaken?.Invoke();   
    }

    public void Heal(float health)
    {
        
    }
}
