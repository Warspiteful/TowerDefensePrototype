using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployedUnit : MonoBehaviour
{
    private Direction _direction;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    private OperatorData _operatorData;

    [SerializeField] private int currentHealth;

   

    public void Initialize(OperatorData _operator)
    {
        _spriteRenderer.sprite = _operator.sprite;
        currentHealth = _operator.health;
    }
}
