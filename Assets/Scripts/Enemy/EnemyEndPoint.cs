using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEndPoint : MonoBehaviour
{
   [SerializeField] private IntVariable Lives;
   private void OnTriggerEnter(Collider col)
   {
      if (col.CompareTag("Enemy"))
      {
         Damageable _enemyHealth = col.gameObject.GetComponent<Damageable>();
         _enemyHealth.TakeDamage(_enemyHealth.GetMaxHealth());
         Lives.Value -= 1;
      }
   }
}
