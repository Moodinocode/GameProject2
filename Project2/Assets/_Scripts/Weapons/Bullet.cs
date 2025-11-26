using UnityEngine;

using _Scripts.EnemyScripts; 

namespace _Scripts.Weapons
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] float timeToDestory = 2f;
        float _timer;

        int _damage;
        public void SetDamage(int dmg) => _damage = dmg;

        void Update()
        {
            _timer += Time.deltaTime;
            if (_timer > timeToDestory)
                Destroy(gameObject);
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

            Destroy(gameObject);
        }
    }
}