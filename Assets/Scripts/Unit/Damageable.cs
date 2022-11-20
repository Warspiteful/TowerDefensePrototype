using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{

    [SerializeField] private float currHealth;
    [SerializeField] private float maxHealth;

    private OnValueChanged _damageTaken;
    private OnValueChanged _healthHealed;
    
    private OnValueChanged _onDeath;


    private void OnDestroy()
    {
        _damageTaken = null;
        _healthHealed = null;
        _onDeath = null;
    }

    public void RegisterDamageTakenCallback(OnValueChanged damageCallback)
    {
        _damageTaken += damageCallback;

    }
    
    public void RegisterHealthHealedCallback(OnValueChanged healCallback)
    {
        _healthHealed += healCallback;
    }

    public void RegisterHealthUpdateCallback(OnValueChanged healthChangeCallback)
    {
        _healthHealed += healthChangeCallback;
        _damageTaken += healthChangeCallback;
    }
    
    public void RegisterOnDeathCallback(OnValueChanged deathCallback)
    {
        _onDeath += deathCallback;
    }

    
    public void Initialize(float health)
    {
        maxHealth = health;
        currHealth = maxHealth;
    }

    public float GetCurrentHealth()
    {
        return currHealth;
    }
    
        public float GetMaxHealth()
        {
            return maxHealth;
        }
        
    

    public void TakeDamage(float damage)
    {
   
        currHealth -= damage;
        Debug.Log("Damage taken by Damageable");
        _damageTaken?.Invoke();

        if (currHealth <= 0)
        {
            _onDeath?.Invoke();
        }
        
    }

    public void Heal(float health)
    {
        currHealth += health;
        _healthHealed.Invoke();

    }
}
