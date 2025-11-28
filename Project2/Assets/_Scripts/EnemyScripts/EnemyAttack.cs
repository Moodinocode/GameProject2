using UnityEngine;

namespace _Scripts.EnemyScripts
{
    public class EnemyAttack : MonoBehaviour
    {
        private Enemy enemy;   // reference to stats

        private void Start()
        {
            enemy = GetComponent<Enemy>();
        }

        // Animation event calls this
        public void DealDamage()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, 2f);

            foreach (var hit in hits)
            {
                if (hit.CompareTag("Player"))
                {
                    Player p = hit.GetComponent<Player>();
                    if (p != null)
                    {
                        p.TakeDamage(enemy.stats.attackDamage);
                    }
                }
            }
        }
    }
}