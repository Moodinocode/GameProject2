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
        //public void SetDamage(int dmg) => _damage = dmg;

        void Update()
        {
            _timer += Time.deltaTime;
            if (_timer > timeToDestory)
                gameObject.SetActive(false);    
            //Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Zombie"))
            {
                Enemy zombie = collision.gameObject.GetComponent<Enemy>();
                if (zombie != null)
                {
                    zombie.TakeDamage(_damage);
                }
            }

            //Destroy(gameObject); set as inactive instead of destroying for object pooling
            gameObject.SetActive(false);
        }

        public void OnObjectSpawn()
        {
            _timer = 0f;
        }
    }
}