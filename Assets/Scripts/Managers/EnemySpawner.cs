using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Managers
{
    public class EnemySpawner : Singleton<EnemySpawner>
    {
        public static bool IsWavePlaying = false;
        
        [SerializeField] private float waveTimeInSeconds;
        [SerializeField] private int enemiesCount;
        [SerializeField] private List<GameObject> enemies;

        private List<Vector3> _spawners;
        private float _timeBetweenSpawns;
        private float _timer;
        
        private void Start()
        {
            _spawners = PathManager.Instance.GetStartPositions();
            _timeBetweenSpawns = waveTimeInSeconds / enemiesCount;

            Events.OnStartWave += StartWave;
        }

        private void StartWave()
        {
            IsWavePlaying = true;
            StartCoroutine("SpawnEnemies");
        }

        IEnumerator SpawnEnemies()
        {
            while (_timer < waveTimeInSeconds)
            {
                Instantiate(enemies.First(), _spawners.First(), Quaternion.identity);
                _timer += _timeBetweenSpawns;
                yield return new WaitForSeconds(_timeBetweenSpawns);
            }

            _timer = 0;
            IsWavePlaying = false;
            Events.OnEndWave.Invoke();
        }

        private void OnDestroy()
        {
            Events.OnStartWave -= StartWave;
        }
    }
}