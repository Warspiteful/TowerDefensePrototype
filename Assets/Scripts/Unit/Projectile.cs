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
    public void Initialize(Transform target, float damage)
    {
        _target = target;
        _damage = damage;
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
            Damageable enemyHealth = collision.gameObject.GetComponent<Damageable>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(_damage);
            }
            Destroy(gameObject);
        }
    }
}
