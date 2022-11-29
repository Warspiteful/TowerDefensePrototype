using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float speed;

    private float _damage;

    private Transform _target; 
    private Damageable _targetHealth; 
    public void Initialize(Damageable target, float damage)
    {
        _target = target.transform;
        _damage = damage;
        _targetHealth = target;
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            Vector3 dirNormalized = (_target.transform.position - transform.position).normalized;
            this.transform.position += dirNormalized * speed * Time.deltaTime;
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(collision.gameObject.GetComponent<Damageable>() == _targetHealth)
            {
                _targetHealth.TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
}
