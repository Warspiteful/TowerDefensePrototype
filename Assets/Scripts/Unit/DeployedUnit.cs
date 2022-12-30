using System;
using UnityEngine;


[RequireComponent(typeof(OperatorAttack),typeof(Damageable), typeof(UnitAnimator)), RequireComponent(typeof(UnitInput))]
public class DeployedUnit : MonoBehaviour
{
    
    //private Direction _direction;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    private OperatorData _operatorData;




    private OperatorAttack _attack;
    private Damageable _damageable;
    private UnitAnimator _animator;
    private UnitInput _input;


    private VoidCallback onDestroy;


   

    public void RegisterOnDestroyCallback(VoidCallback callback)
    {
        _damageable.RegisterOnDeathCallback(callback);
    }
    
    

    public void RegisterHealthChange(VoidIntInitCallback _callback)
    {
        _damageable.RegisterHealthTrackingCallback(_callback);
    }


    public void Initialize(OperatorData _operator)
    {        

        _input = GetComponent<UnitInput>();
        _animator = GetComponent<UnitAnimator>();         
        _damageable = GetComponent<Damageable>();
        _spriteRenderer.sprite = _operator.sprite;
        _operatorData = _operator;
        _animator.SetOverrides(_operator.animationOverrides);

        
        _damageable.Initialize(_operator.CurrentHealth);
        _damageable.RegisterDamageTakenCallback(_animator.PlayTakeDamage);
        _damageable.RegisterDamageTakenCallback(() => _operatorData.CurrentHealth = _damageable.GetCurrentHealth());
        _attack = GetComponent<OperatorAttack>();
        _attack.Initialize(_operator.range, _operator.atkPower, _operator.guardedUnitNumber, _operator.projectile );
        _attack.RegisterCallbacks(_animator.PlayAttack, _animator.PlayIdle);
        _damageable.RegisterOnDeathCallback(_animator.PlayDeath);
        
        gameObject.SetActive(false);
    }

    public void Deploy( Direction dir)
    {
        gameObject.SetActive(true);
        _attack.GenerateAttackTiles(dir);

    }



    public Vector2[] GetRange()
    { 
        return _attack.GetRange();
    }


   
    public void RegisterOnClickCallback(params VoidCallback[] callback)
    {
            _input.RegisterOnClickCallback(callback);
    }

    public void RegisterOnClickElsewhereCallback(VoidCallback callback)
    {
        _input.RegisterOnElsewhereClickCallback(callback);
    }



}
