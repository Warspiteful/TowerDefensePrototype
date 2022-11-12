using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damageable))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData _data;

    private void Start()
    {
        GetComponent<Damageable>().Initialize(_data.health);
    }
}
