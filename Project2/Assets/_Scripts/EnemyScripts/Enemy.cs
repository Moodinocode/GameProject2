using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.EnemyScripts
{
    public class Enemy : MonoBehaviour
    {
        public EnemyStats stats;
        private int _hp;
        private Animator _animator;
        public bool isDead = false;
    
        private NavMeshAgent _navAgent;
        private ZombieAudio _audio; 
        
        public int CurrentHp => _hp;

    
    
    
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _navAgent = GetComponent<NavMeshAgent>();
            _audio = GetComponent<ZombieAudio>();
            _hp = stats.maxHP;
        }

        public void TakeDamage(int damageAmount)
        {
            if (isDead) return;

            _hp -= damageAmount;

            if (_hp <= 0)
            {
                Die();
                return;
            } 
            if (_audio != null) _audio.PlayHurt();
            _animator.SetTrigger("DAMAGE");
        }
        
        private void Die()
        {
            if (isDead) return;
            isDead = true;
            
            if (_audio != null)
                _audio.PlayDeath();

            // Stop NavMeshAgent
            if (_navAgent != null)
            {
                _navAgent.isStopped = true;
                _navAgent.ResetPath();
                _navAgent.enabled = false;
            }
            
            foreach (var col in GetComponentsInChildren<Collider>())
                col.enabled = false;

            // play a random death animation
            int randomValue = Random.Range(0, 2);
            _animator.SetTrigger(randomValue == 0 ? "DIE1" : "DIE2");

            // destroy after 7 seconds
            Destroy(gameObject, 7f);
        }

        public void SetHp(int hp)
        {
            _hp = hp;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 2.5f);
        
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, 18f);
        
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 21f);
        }
    }
}


[System.Serializable]
public struct ZombieSaveData {
    public Vector3 position;
    public int hp;
    public bool isDead;
}