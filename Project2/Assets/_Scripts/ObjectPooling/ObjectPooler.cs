using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.ObjectPooling
{
    public class ObjectPooler : MonoBehaviour
    {
        [System.Serializable]
        public class Pool
        {
            public string Tag;
            public GameObject Prefab;
            public int PoolSize;
        }

        public static ObjectPooler Instance;

        public Dictionary<string, Queue<GameObject>> PoolDictionary;
        public List<Pool> pools;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

                SceneManager.sceneLoaded += OnSceneLoaded;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // If we return to MAIN MENU, destroy pooler completely
            if (scene.buildIndex == 0)
            {
                Destroy(gameObject);
                Instance = null;
                return;
            }

            // If we enter a gameplay scene, rebuild pools
            BuildPools();
        }

        private void Start()
        {
            // Start is called only once due to DontDestroyOnLoad
            // So we must build manually here for first gameplay scene
            BuildPools();
        }

        private void BuildPools()
        {
            PoolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (var pool in pools)
            {
                var objectPool = new Queue<GameObject>();

                for (int i = 0; i < pool.PoolSize; i++)
                {
                    var obj = Instantiate(pool.Prefab);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                PoolDictionary.Add(pool.Tag, objectPool);
            }
        }

        public GameObject GetFromPool(string tag, Vector3 pos, Quaternion rot)
        {
            if (!PoolDictionary.ContainsKey(tag))
                return null;

            var obj = PoolDictionary[tag].Dequeue();

            // IMPORTANT SAFETY CHECK
            if (obj == null)
            {
                Debug.LogWarning($"[ObjectPooler] Object in pool '{tag}' was destroyed. Recreating...");
                obj = Instantiate(GetPrefab(tag));
            }

            obj.SetActive(true);
            obj.transform.position = pos;
            obj.transform.rotation = rot;

            var pooledObject = obj.GetComponent<IPooledObject>();
            if (pooledObject != null)
                pooledObject.OnObjectSpawn();

            PoolDictionary[tag].Enqueue(obj);
            return obj;
        }

        private GameObject GetPrefab(string tag)
        {
            foreach (var p in pools)
                if (p.Tag == tag)
                    return p.Prefab;

            return null;
        }
    }
}
