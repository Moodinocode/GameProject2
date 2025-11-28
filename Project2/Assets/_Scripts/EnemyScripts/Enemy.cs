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
            HP -= damageAmount;

            if (HP <= 0)
            {
                isDead = true;
                int radnomValue = Random.Range(0, 2);
                if (radnomValue == 0)
                {
                    animator.SetTrigger("DIE1");
                }
                else
                {
                    animator.SetTrigger("DIE2");
                }
            }
            else
            {
                animator.SetTrigger("DAMAGE");
            }
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
