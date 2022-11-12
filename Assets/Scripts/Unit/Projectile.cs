using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float speed;

    private Transform _target; 
    public void Initialize(Transform target)
    {
        _target = target;
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            Vector3 dirNormalized = (_target.transform.position - transform.position).normalized;
            this.transform.position += dirNormalized * speed * Time.deltaTime;
            
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
