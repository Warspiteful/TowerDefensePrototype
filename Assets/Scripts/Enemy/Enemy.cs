using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damageable),typeof(UnitAnimator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData _data;

    private Damageable _damageable;
    private UnitAnimator _animator;
    
    private void Start()
    {
        _animator = GetComponent<UnitAnimator>();
        _animator.SetOverrides(_data.animationOverrides);
        
        _damageable = GetComponent<Damageable>();
        _damageable.Initialize(_data.health);
            
            _damageable.RegisterDamageTakenCallback(_animator.PlayTakeDamage);

    }
}
