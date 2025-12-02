using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.EnemyScripts
{
    public class Enemy : MonoBehaviour
    {
        public EnemyStats stats;
        private int HP;
        private Animator animator;
        public bool isDead = false;
    
        private NavMeshAgent navAgent;
    
    
    
        private void Start()
        {
            animator = GetComponent<Animator>();
            navAgent = GetComponent<NavMeshAgent>();
            HP = stats.maxHP;
        }

        public void TakeDamage(int damageAmount)
        {
            if (isDead) return;

            HP -= damageAmount;

            if (HP <= 0)
            {
                Die();
                return;
            }

            animator.SetTrigger("DAMAGE");
        }
        
        private void Die()
        {
            if (isDead) return;
            isDead = true;

            // Stop NavMeshAgent
            if (navAgent != null)
            {
                navAgent.isStopped = true;
                navAgent.ResetPath();
                navAgent.enabled = false;
            }
            
            foreach (var col in GetComponentsInChildren<Collider>())
                col.enabled = false;

            // play a random death animation
            int randomValue = Random.Range(0, 2);
            animator.SetTrigger(randomValue == 0 ? "DIE1" : "DIE2");

            // destroy after 7 seconds
            Destroy(gameObject, 7f);
        }


        private void onDrawGizmos()
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
