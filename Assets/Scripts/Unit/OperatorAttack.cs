using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorAttack : MonoBehaviour
{

    private float _attackSpeed;
    private float _attackPower;

    [SerializeField] private GameObject targetEnemy;
    private List<GameObject> inRangeEnemies;
    private bool initialized = false;


    private void Start()
    {
        inRangeEnemies = new List<GameObject>();

    }

    public void Initialize(float attackSpeed, float attackPower)
    {
        _attackPower = attackPower;
        _attackSpeed = attackSpeed;
        initialized = true;
    }
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider collision)
    {
        
        if(initialized && collision.gameObject.CompareTag("Enemy"))
        {
            if(targetEnemy == null){
                targetEnemy = collision.gameObject;
            }
            else
            {
                inRangeEnemies.Add(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (initialized && other.gameObject == targetEnemy)
        {
            if(inRangeEnemies.Count == 0)
            {
                targetEnemy = null;
            }
            else
            {
                targetEnemy = inRangeEnemies[0];
                inRangeEnemies.Remove(targetEnemy);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(targetEnemy!=null && initialized){
            Debug.DrawLine(transform.position, targetEnemy.transform.position,Color.red);
        }
    }

    private void RemoveEnemy()
    {
        
    }
}
