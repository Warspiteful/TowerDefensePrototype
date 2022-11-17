using UnityEngine;


[RequireComponent(typeof(OperatorAttack),typeof(Damageable), typeof(UnitAnimator)), RequireComponent(typeof(UnitInput))]
public class DeployedUnit : MonoBehaviour
{
    [SerializeField] private Direction _direction;
    
    //private Direction _direction;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    private OperatorData _operatorData;

    [SerializeField] private int currentHealth;



    private OperatorAttack _attack;
    private Damageable _damageable;
    private UnitAnimator _animator;
    private UnitInput _input;
    

   

    public void Initialize(OperatorData _operator)
    {

        _input = GetComponent<UnitInput>();
        _animator = GetComponent<UnitAnimator>();         
        _damageable = GetComponent<Damageable>();
        
       
        _spriteRenderer.sprite = _operator.sprite;
        _operatorData = _operator;


        _animator.SetOverrides(_operator.animationOverrides);

        _damageable.Initialize(_operator.health);
        _damageable.RegisterDamageTakenCallback(_animator.PlayTakeDamage);



        _attack = GetComponent<OperatorAttack>();
        _attack.Initialize(_operator.range, _operator.atkPower, _operator.projectile );


        _attack.RegisterCallbacks(_animator.PlayAttack, _animator.PlayIdle);
    }
    
    public Direction GetDirection()
    { 
        return _direction;
    }
    
    public Vector2 GetRange()
    { 
        return _operatorData.range;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("AAAH");
        }
    }
    
    
    public void RegisterOnClickCallback(params VoidCallback[] callback)
    {
            _input.RegisterOnClickCallback(callback);
    }



}
