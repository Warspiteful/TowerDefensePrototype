using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private IntVariable lives;
    [SerializeField] private int startingLives;


    private void Awake()
    {
        lives.Value = startingLives;
    }
}
