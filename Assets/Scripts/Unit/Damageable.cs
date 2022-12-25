using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{

    [SerializeField] private int currHealth;
    [SerializeField] private int maxHealth;

    private VoidCallback _damageTaken;
    private VoidCallback _healthHealed;
    
    private VoidCallback _onDeath;


    private void OnDestroy()
    {
        _damageTaken = delegate {  } ;
        _healthHealed = delegate {  };
        _onDeath = delegate {  };
    }

    public void RegisterDamageTakenCallback(VoidCallback damageCallback)
    {
        _damageTaken += damageCallback;

    }
    
    public void RegisterHealthHealedCallback(VoidCallback healCallback)
    {
        _healthHealed += healCallback;
    }

    public void RegisterHealthUpdateCallback(VoidCallback healthChangeCallback)
    {
        _healthHealed += healthChangeCallback;
        _damageTaken += healthChangeCallback;
    }
    
    public void RegisterOnDeathCallback(VoidCallback deathCallback)
    {
        _onDeath += deathCallback;
    }

    
    public void Initialize(int health)
    {
        maxHealth = health;
        currHealth = maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currHealth;
    }
    
        public int GetMaxHealth()
        {
            return maxHealth;
        }
        
    

    public void TakeDamage(int damage)
    {

        currHealth  = Mathf.Clamp(currHealth - damage, 0, currHealth);
        
        _damageTaken?.Invoke();

        if (currHealth <= 0)
        {
            _onDeath?.Invoke();
        }
        
    }

    public void Heal(int health)
    {
        currHealth += health;
        _healthHealed.Invoke();

    }
}
