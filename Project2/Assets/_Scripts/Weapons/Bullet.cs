using UnityEngine;

using _Scripts.EnemyScripts;
using _Scripts.ObjectPooling;
using UnityEngine.Pool;
using UnityEngine.Serialization;

namespace _Scripts.Weapons
{
    public class Bullet : MonoBehaviour, IPooledObject
    {
        [SerializeField] float timeToDestory = 2f;
        float _timer;
        [FormerlySerializedAs("_damage")] [SerializeField] int damage;

        void Update()
        {
            _timer += Time.deltaTime;
            if (_timer > timeToDestory)
                gameObject.SetActive(false);    
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Zombie")||
                collision.collider.transform.root.CompareTag("Zombie"))
            {
                Enemy zombie = collision.gameObject.GetComponent<Enemy>();
                if (zombie != null)
                {
                    zombie.TakeDamage(damage);
                }
            }
            
            gameObject.SetActive(false);
        }

        public void OnObjectSpawn()
        {
            _timer = 0f;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;          // Reset old leftover force
            rb.angularVelocity = Vector3.zero;   // Reset spin
            transform.localRotation = Quaternion.identity;
        }
    }
}