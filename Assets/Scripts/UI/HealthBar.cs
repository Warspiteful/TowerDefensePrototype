using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
   [SerializeField] private SpriteRenderer HealthIndicator;

   [SerializeField] private GameObject healthbar;

   [SerializeField]
   private bool displayByDefault;
   private Damageable _damageable;
   
   

   private void Start()
   {
      _damageable = GetComponentInParent<Damageable>();
      _damageable.RegisterHealthUpdateCallback(UpdateDisplay);
      healthbar.SetActive(displayByDefault);
      
   }

   private void UpdateDisplay()
   {
      Vector3 currScale = HealthIndicator.transform.localScale;
      if (healthbar.activeSelf != true)
      {
         healthbar.SetActive(true);
      }

      if (_damageable.GetCurrentHealth() <= 0 || _damageable.GetMaxHealth() <= 0)
      {
         return;
      }
   
   
      HealthIndicator.transform.localScale =
         new Vector3(
            (float)_damageable.GetCurrentHealth() / _damageable.GetMaxHealth(),
            currScale.y,
            currScale.z
            );
   }
} 
