using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class OperatorData : UnitData
{
    public DeployLocationType locationType;
    public float deployTime;
    public int deployCost;
    public int guardedUnitNumber;
    public Archetype archtetype;
    public Projectile projectile;

}
