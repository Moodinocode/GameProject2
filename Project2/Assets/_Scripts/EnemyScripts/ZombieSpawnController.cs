using System.Collections;
using System.Collections.Generic;
using _Scripts.EnemyScripts;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieSpawnController : MonoBehaviour
{
    public int initialZombiesPerWave = 5;
    public int currentZombiesPerWave;

    public float spawnDelay = 0.5f;
    
    public int currentWave = 0;
    public float waveCooldown = 10;

    public bool inCooldown;
    public float cooldownCounter = 0;

    public List<Enemy> currentZombiesAlive;
    
    public GameObject zombiePrefab;
    
    
   
    void Start()
    {
        currentZombiesPerWave = initialZombiesPerWave;

        StartNextWave();

    }

    private void StartNextWave()
    {
        currentZombiesAlive.Clear();
        currentWave++;

        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < currentZombiesPerWave; i++)
        {
            Vector3 spawnOffset = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            Vector3 spawnPosition = transform.position + spawnOffset;
            var zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
            Enemy enemyScript = zombie.GetComponent<Enemy>();
            currentZombiesAlive.Add(enemyScript);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    
    void Update()
    {
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

        if (currentZombiesAlive.Count == 0 && inCooldown == false)
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

        currentZombiesPerWave *= 2;
        StartNextWave();
        
    }
}
