using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Managers
{
    public class EnemySpawner : Singleton<EnemySpawner>
    {
        [SerializeField] private float waveTimeInSeconds;
        [SerializeField] private int enemiesCount;
        [SerializeField] private List<GameObject> enemies;

        private List<Vector3> _spawners;
        private float _timeBetweenSpawns;
        private float _timer;
        
        private IEnumerator _spawnEnemies;
        
        public override void Awake()
        {
            base.Awake();
            _spawnEnemies = SpawnEnemies();
        }

        private void Start()
        {
            _spawners = PathManager.Instance.GetStartPositions();
            _timeBetweenSpawns = waveTimeInSeconds / enemiesCount;

            StartCoroutine(_spawnEnemies);
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
        }
    }
}