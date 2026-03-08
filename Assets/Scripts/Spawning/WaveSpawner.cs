using System.Collections;
using Tafang.Core;
using Tafang.Enemies;
using Tafang.Pathing;
using UnityEngine;

namespace Tafang.Spawning
{
    public class WaveSpawner : MonoBehaviour
    {
        [System.Serializable]
        public class WaveDefinition
        {
            public GameObject enemyPrefab;
            public int count = 5;
            public float startDelay = 1f;
            public float spawnInterval = 0.7f;
            public float speedMultiplier = 1f;
        }

        [SerializeField] private WaveDefinition[] waves;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private WaypointPath path;
        [SerializeField] private float timeBetweenWaves = 3f;
        [SerializeField] private bool playOnStart = true;
        [SerializeField] private int clearBonusGold = 100;

        public bool AllWavesCompleted { get; private set; }

        private Coroutine routine;

        private void Start()
        {
            if (playOnStart)
            {
                StartSpawning();
            }
        }

        public void StartSpawning()
        {
            if (routine != null)
            {
                StopCoroutine(routine);
            }

            routine = StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            AllWavesCompleted = false;

            if (waves == null || waves.Length == 0)
            {
                Debug.LogWarning("WaveSpawner has no configured waves.", this);
                yield break;
            }

            for (int waveIndex = 0; waveIndex < waves.Length; waveIndex++)
            {
                if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
                {
                    yield break;
                }

                WaveDefinition wave = waves[waveIndex];
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.SetWave(waveIndex + 1);
                }

                yield return new WaitForSeconds(wave.startDelay);

                for (int i = 0; i < wave.count; i++)
                {
                    SpawnEnemy(wave);
                    yield return new WaitForSeconds(wave.spawnInterval);
                }

                while (EnemyHealth.ActiveEnemies > 0)
                {
                    if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
                    {
                        yield break;
                    }

                    yield return null;
                }

                if (waveIndex < waves.Length - 1)
                {
                    yield return new WaitForSeconds(timeBetweenWaves);
                }
            }

            AllWavesCompleted = true;
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddGold(clearBonusGold);
            }
        }

        private void SpawnEnemy(WaveDefinition wave)
        {
            if (wave.enemyPrefab == null || spawnPoint == null || path == null)
            {
                Debug.LogError("WaveSpawner missing enemyPrefab/spawnPoint/path reference.", this);
                return;
            }

            GameObject enemyObj = Instantiate(wave.enemyPrefab, spawnPoint.position, Quaternion.identity);
            EnemyMover mover = enemyObj.GetComponent<EnemyMover>();
            if (mover != null)
            {
                mover.SetPath(path);
                mover.SetMoveSpeed(Mathf.Max(0.1f, mover.MoveSpeed * wave.speedMultiplier));
            }
        }
    }
}
