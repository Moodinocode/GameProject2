using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using _Scripts.Managers;
using _Scripts.ChestScripts;
using _Scripts.EnemyScripts;

namespace _Scripts.Managers
{
    public class SaveSystem
    {
        private static SaveData _saveData;
        public static bool IsLoadingSave = false;

        [System.Serializable]
        public struct SaveData
        {
            public int sceneIndex;

            public PlayerSaveData playerData;
            public AmmoSaveData ammoData;

            public ChestSaveData chestData;
            public KeySaveData keyData;

            public SpawnerSaveWrapper[] spawners;
        }

        public static string SaveFileName()
        {
            return Application.persistentDataPath + "/save.save";
        }

        public static void Save(int sceneIndex)
        {
            HandleSaveData();
            _saveData.sceneIndex = sceneIndex;

            File.WriteAllText(SaveFileName(), JsonUtility.ToJson(_saveData, true));
        }

        private static void HandleSaveData()
        {
            SaveLoadManager.Instance.player.Save(ref _saveData.playerData);
            SaveLoadManager.Instance.ammo.Save(ref _saveData.ammoData);
            SaveLoadManager.Instance.chestState.Save(ref _saveData.chestData);
            SaveLoadManager.Instance.keyState.Save(ref _saveData.keyData);
           
            ZombieSpawnController[] spawners =
                GameObject.FindObjectsOfType<ZombieSpawnController>();

            _saveData.spawners = new SpawnerSaveWrapper[spawners.Length];
            

            for (int i = 0; i < spawners.Length; i++)
            {
                // Save spawner state
                spawners[i].Save(ref _saveData.spawners[i].spawnerState);

                // Save zombies belonging to this spawner
                Enemy[] zombies = spawners[i].currentZombiesAlive.ToArray();

                _saveData.spawners[i].zombies = new ZombieSaveData[zombies.Length];

                for (int j = 0; j < zombies.Length; j++)
                {
                    _saveData.spawners[i].zombies[j].position = zombies[j].transform.position;
                    _saveData.spawners[i].zombies[j].hp = zombies[j].CurrentHp;
                    _saveData.spawners[i].zombies[j].isDead = zombies[j].isDead;
                }
            }
        }
        public static void Load()
        {
            IsLoadingSave = true;
            string json = File.ReadAllText(SaveFileName());
            _saveData = JsonUtility.FromJson<SaveData>(json);

            SceneManager.LoadScene(_saveData.sceneIndex);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SaveLoadManager.Instance.StartCoroutine(DelayedLoad());
        }

        private static IEnumerator DelayedLoad()
        {
            // Wait a frame for objects to initialize
            yield return null;
            yield return new WaitForEndOfFrame();

            HandleLoadData();
            IsLoadingSave = false;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private static void HandleLoadData()
        {
            
            SaveLoadManager.Instance.player.Load(_saveData.playerData);
            SaveLoadManager.Instance.ammo.Load(_saveData.ammoData);
            SaveLoadManager.Instance.chestState.Load(_saveData.chestData,_saveData.keyData);
            SaveLoadManager.Instance.keyState.Load(_saveData.keyData);

            ZombieSpawnController[] spawners =
                GameObject.FindObjectsOfType<ZombieSpawnController>();

            for (int i = 0; i < spawners.Length; i++)
            {
                spawners[i].Load(_saveData.spawners[i].spawnerState);
                ZombieSaveData[] zombies = _saveData.spawners[i].zombies;

                foreach (var z in zombies)
                {
                    if (z.isDead) continue;

                    GameObject zombie =
                        Object.Instantiate(spawners[i].zombiePrefab, z.position, Quaternion.identity);

                    Enemy e = zombie.GetComponent<Enemy>();
                    e.SetHp(z.hp);
                    
                    spawners[i].currentZombiesAlive.Add(e);
                }
            }
        }
    }
}


[System.Serializable]
public struct SpawnerSaveWrapper
{
    public ZombieSpawnerSaveData spawnerState;
    public ZombieSaveData[] zombies;
}