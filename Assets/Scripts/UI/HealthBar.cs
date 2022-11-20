using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
   [SerializeField] private SpriteRenderer HealthIndicator;

   [SerializeField] private GameObject healthbar;
   private Damageable _damageable;
   
   

   private void Start()
   {
      _damageable = GetComponentInParent<Damageable>();
      _damageable.RegisterHealthUpdateCallback(UpdateDisplay);
      healthbar.SetActive(false);
      
   }

   private void UpdateDisplay()
   {
      Vector3 currScale = HealthIndicator.transform.localScale;
      if (healthbar.activeSelf != true)
      {
         healthbar.SetActive(true);
      }

      HealthIndicator.transform.localScale =
         new Vector3(
            _damageable.GetCurrentHealth() / _damageable.GetMaxHealth(),
            currScale.y,
            currScale.z
            );
   }
} 
