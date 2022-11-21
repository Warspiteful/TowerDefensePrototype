using System;
using System.Collections;
using System.Collections.Generic;   
using UnityEngine;

[RequireComponent(typeof(Damageable),typeof(UnitAnimator),typeof(MeleeEnemyAttack))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData _data;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private IntVariable killCount;

    private Damageable _damageable;
    private UnitAnimator _animator;
    private MeleeEnemyAttack _enemyAttack;
    private EnemyPathManager _pathManager;


    public void Initialize(EnemyData _data, Vector3[] path)
    {
        _renderer.sprite = _data.sprite;
        _animator = GetComponent<UnitAnimator>();
        _animator.SetOverrides(_data.animationOverrides);
        
        _damageable = GetComponent<Damageable>();
        _damageable.Initialize(_data.health);
        
        _damageable.RegisterDamageTakenCallback(_animator.PlayTakeDamage);


        _enemyAttack = GetComponent<MeleeEnemyAttack>();
        _enemyAttack.Initialize(_data.atkPower);
        _enemyAttack.RegisterCallbacks(_animator.PlayAttack, _animator.PlayIdle);

        _pathManager = GetComponent<EnemyPathManager>();
        _pathManager.Initialize(path);
        _enemyAttack.RegisterIsBlockedCallback(_pathManager.ControlMoving);
        
        _damageable.RegisterOnDeathCallback(_animator.PlayDeath);
        _damageable.RegisterOnDeathCallback(killCount.Increment);

    }


    



}
