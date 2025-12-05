using System.Collections;
using System.Collections.Generic;
using _Scripts.Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.EnemyScripts
{
    public class ZombieSpawnController : MonoBehaviour
    {
        public int initialZombiesPerWave = 2;
        public int currentZombiesPerWave;
        public int maxWaves = 5;
        public bool wavesFinished = false;

        public float spawnDelay = 0.5f;
    
        public int currentWave = 0;
        public float waveCooldown = 10;

        public bool inCooldown;
        public float cooldownCounter = 0;

        public List<Enemy> currentZombiesAlive;
    
        public GameObject zombiePrefab;
        
        public GameObject portal;
        
        public int zombiesSpawnedSoFar;
        
        public bool loadingInProgress;
        
   
        void Start()
        {
            currentZombiesPerWave = initialZombiesPerWave;
            if (SaveSystem.IsLoadingSave)
                return;
            StartNextWave();

        }

        private void StartNextWave()
        {
            zombiesSpawnedSoFar = 0;
            if (currentWave >= maxWaves)
            {
                wavesFinished = true;
                portal.SetActive(true);   // Unlock the portal
                return;
            }
            currentZombiesAlive.Clear();
            currentWave++;

            StartCoroutine(SpawnWave());
        }

        private IEnumerator SpawnWave()
        {
            for (int i = zombiesSpawnedSoFar; i < currentZombiesPerWave; i++)
            {
                Vector3 spawnOffset = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
                Vector3 spawnPosition = transform.position + spawnOffset;
                var zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
                Enemy enemyScript = zombie.GetComponent<Enemy>();
                currentZombiesAlive.Add(enemyScript);
                zombiesSpawnedSoFar++;
                yield return new WaitForSeconds(spawnDelay);
            }
            loadingInProgress = false;
        }

    
        void Update()
        {
            if (loadingInProgress)
                return;
            List<Enemy> zombiesToRemove = new List<Enemy>();
            foreach (Enemy zombie in currentZombiesAlive)
            {
                if (zombie.isDead)
                {
                    zombiesToRemove.Add(zombie);
                }
            }

            foreach (Enemy zombie in zombiesToRemove)
            {
                currentZombiesAlive.Remove(zombie);
            }
        
            zombiesToRemove.Clear();

            if (!wavesFinished && currentZombiesAlive.Count == 0 && inCooldown == false)
            {
                StartCoroutine(WaveCooldown());
            }

            if (inCooldown)
            {
                cooldownCounter += Time.deltaTime;
            }
            else
            {
                cooldownCounter = waveCooldown;
            }
        }

        private IEnumerator WaveCooldown()
        {
            inCooldown = true;
            yield return new WaitForSeconds(waveCooldown);
            inCooldown = false;

            if (wavesFinished) yield break; 
            
            currentZombiesPerWave *= 2;
            StartNextWave();
        
        }
        
        public void Save(ref ZombieSpawnerSaveData data)
        {
            data.currentWave = currentWave;
            data.zombiesPerWave = currentZombiesPerWave;
            data.inCooldown = inCooldown;
            data.cooldownCounter = cooldownCounter;
            data.zombiesSpawnedSoFar = zombiesSpawnedSoFar;
            data.wavesFinished = wavesFinished;                    
            data.waveFullySpawned = (zombiesSpawnedSoFar >= currentZombiesPerWave);  
        }

        
        public void Load(ZombieSpawnerSaveData data)
        {
            loadingInProgress = true;
            currentWave = data.currentWave;
            currentZombiesPerWave = data.zombiesPerWave;
            inCooldown = data.inCooldown;
            cooldownCounter = data.cooldownCounter;
            zombiesSpawnedSoFar = data.zombiesSpawnedSoFar;
            wavesFinished = data.wavesFinished;
            
            if (data.waveFullySpawned && wavesFinished)
            {
                portal.SetActive(true);
                return;  
            }
            if (!inCooldown && zombiesSpawnedSoFar < currentZombiesPerWave)
                StartCoroutine(SpawnWave());
        }

    }
}



[System.Serializable]
public struct ZombieSpawnerSaveData {
    public int currentWave;
    public int zombiesPerWave;
    public bool inCooldown;
    public float cooldownCounter;
    public int zombiesSpawnedSoFar;
    public bool wavesFinished;      
    public bool waveFullySpawned;  
    
}
