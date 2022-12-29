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

      float healthScale;
      Vector3 currScale = HealthIndicator.transform.localScale;
      if (healthbar.activeSelf != true)
      {
         healthbar.SetActive(true);
      }
      
      

      if (_damageable.GetCurrentHealth() <= 0 || _damageable.GetMaxHealth() <= 0)
      {
         healthScale = 0;
      }
      else
      {
         healthScale = (float) _damageable.GetCurrentHealth() / _damageable.GetMaxHealth();
      }
   
   
      HealthIndicator.transform.localScale =
         new Vector3(
            healthScale,
            currScale.y,
            currScale.z
            );
   }
} 
