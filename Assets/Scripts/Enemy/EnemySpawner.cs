using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

   [SerializeField] private Enemy _enemyPrefab;

   private Vector3[] _path;
   public void Initialize(Vector3[] path)
   {
      _path = path;
   }
   public void SpawnEnemy(EnemyData _data)
   {
      Instantiate(_enemyPrefab).Initialize(_data, _path);
      
   }
}
