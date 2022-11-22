using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{

    [SerializeField]
    private IntVariable totalEnemies;
    [SerializeField]
    private IntVariable EnemyKillCount;
    
    [SerializeField] private List<Timestamp> _spawnTimes;

    [SerializeField] private List<EnemySpawner> _spawnList;

    [SerializeField] private bool initialized;
    [SerializeField] private float timer;
    
    [SerializeField] private List<Timestamp> finished;

    public void Initialize(List<EnemySpawner> _spawners)
    {
        int enemyCount = 0;
        _spawnList = _spawners;
        initialized = true;
        foreach (Timestamp _timestamp in _spawnTimes)
        {
            enemyCount = _timestamp._spawnDatas.Count;
        }

        totalEnemies.Value = enemyCount;
        EnemyKillCount.Value = 0;
    }

    private void Update()
    {
        if (initialized)
        {
            timer += Time.deltaTime;
            CheckTime();

        }
    }

    private void CheckTime()
    {
        foreach (Timestamp _timestamp in _spawnTimes)
        {
            if(!finished.Contains(_timestamp)){
            if (_timestamp.timestamp > timer && !_timestamp.completed)
            {
                finished.Add(_timestamp);
                StartCoroutine(SpawnEnemy(_timestamp));


            }
        }
    }

    IEnumerator SpawnEnemy(Timestamp _timestamp)
    {
        foreach (SpawnData _spawnData in _timestamp._spawnDatas)
        {
            Debug.Log("Spawning " + _spawnData._enemy.name);
            try
            {
                if(_spawnList.Count >= _spawnData.spawnerIndex){
                    _spawnList[_spawnData.spawnerIndex].SpawnEnemy(_spawnData._enemy);
                }

            }
            catch(NullReferenceException e)
            {
                Debug.LogError("Invalid Spawn Index " + _spawnData.spawnerIndex + " for Timestamp " + _timestamp.timestamp);
            }

            yield return new WaitForSeconds(1);
        }
    }
    }

    [Serializable]
    private struct Timestamp
    {
        [SerializeField] public float timestamp;
        [SerializeField] public bool completed;
        [SerializeField]  public List<SpawnData> _spawnDatas;
        public void Complete()
        {
            Debug.Log("COMPLETED");
            completed = true;
        }
    }

    [Serializable]
    private struct SpawnData
    {
        public EnemyData _enemy;
        public int spawnerIndex;
    }
}
