using System.Collections;
using System.Collections.Generic;
using RoboRyanTron.Unite2017.Events;
using UnityEngine;

public class EndConditions : MonoBehaviour
{
    [Header("Lives")] [SerializeField] private IntVariable Lives;

    [Header("Enemies Defeated")]
    [SerializeField]
    private IntVariable totalEnemies;
    [SerializeField] 
    private IntVariable killedEnemies;

    [Header("Events")] 
    [SerializeField] private GameEvent WinConditionEvent;
    [SerializeField] private GameEvent LossConditionEvent;


    // Start is called before the first frame update
    void Start()
    {
        killedEnemies.onValueChanged += CheckEnemiesKilled;
        Lives.onValueChanged += CheckLives;
    }
    
    private void CheckLives()
    {
        if (Lives.Value <= 0)
        {
            LossConditionEvent.Raise();
        }
    }
    private void CheckEnemiesKilled()
    {
    
        if (totalEnemies.Value == killedEnemies.Value)
        {
            WinConditionEvent.Raise();
        }
    }


}
