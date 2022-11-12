using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{

    [SerializeField] private float currHealth;
    [SerializeField] private float maxHealth;

    public void Initialize(float health)
    {
        maxHealth = health;
        currHealth = maxHealth;
    }


    public void TakeDamage(float damage)
    {
        
    }

    public void Heal(float health)
    {
        
    }
}
