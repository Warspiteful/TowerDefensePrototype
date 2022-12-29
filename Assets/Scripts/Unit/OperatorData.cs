using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class OperatorData : UnitData
{

    public VoidCallback onHealthChange;
    private int currentHealth;
    public DeployLocationType locationType;
    public float deployTime;
    public int deployCost;
    public int guardedUnitNumber;
    public Archetype archtetype;
    public Projectile projectile;

    public void RestoreHealthToMax()
    {
        currentHealth = health;
    }

    public int CurrentHealth
    {
        set
        {
            onHealthChange?.Invoke();
            currentHealth = value;
        }

        get
        {
            return currentHealth;
        }
    }

    
    
}
