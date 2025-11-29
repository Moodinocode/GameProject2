using UnityEngine;

using _Scripts.EnemyScripts;
using _Scripts.ObjectPooling;
using UnityEngine.Pool;

namespace _Scripts.Weapons
{
    public class Bullet : MonoBehaviour, IPooledObject
    {
        [SerializeField] float timeToDestory = 2f;
        float _timer;
        [SerializeField]int _damage;

        void Update()
        {
            _timer += Time.deltaTime;
            if (_timer > timeToDestory)
                gameObject.SetActive(false);    
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Collision");
            Debug.Log("Bullet collided with: " + collision.gameObject.name);
            if (collision.gameObject.CompareTag("Zombie")||
                collision.collider.transform.root.CompareTag("Zombie"))
            {
                Debug.Log("Hit");
                Enemy zombie = collision.gameObject.GetComponent<Enemy>();
                if (zombie != null)
                {
                    zombie.TakeDamage(_damage);
                }
            }
            
            gameObject.SetActive(false);
        }

        public void OnObjectSpawn()
        {
            _timer = 0f;
        }
    }
}