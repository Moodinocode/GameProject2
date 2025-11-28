using System.Collections.Generic;
using UnityEngine;

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
            // using singleton for having the same pooler for multiple scenes
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);  
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }
        
        
        void Start()
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
            Debug.Log("Getting object from pool with tag: " + tag);
            if (!PoolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning("Pool with tag: " + tag + " doesn't exist.");
                return null;
            }

            var objectToSpawn = PoolDictionary[tag].Dequeue();

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = pos;
            objectToSpawn.transform.rotation = rot;

            var pooledObject = objectToSpawn.GetComponent<IPooledObject>();

            if (pooledObject != null)
            {
                pooledObject.OnObjectSpawn();
            }

            PoolDictionary[tag].Enqueue(objectToSpawn);
            Debug.Log("Object returned to pool with tag: " + tag);

            return objectToSpawn;
        }
    }
}
