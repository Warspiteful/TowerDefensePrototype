using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditAutofill : MonoBehaviour
{

    [SerializeField] IntVariable Credits;
      [SerializeField] private float timeToIncrement;
       [SerializeField] private float currentTime;
       [SerializeField] private FloatVariable percent;

       private void Start()
       {
           currentTime = timeToIncrement;
       }
    
    private void Update()
    {
        currentTime -= Time.deltaTime;
        percent.Value = 1 - (currentTime / timeToIncrement);
        if (currentTime < 0)
        {
            Credits.Value += 1;
            currentTime = timeToIncrement;
        }
    }

}
