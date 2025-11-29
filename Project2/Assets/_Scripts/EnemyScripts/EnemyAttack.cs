using UnityEngine;

namespace _Scripts.EnemyScripts
{
    public class EnemyAttack : MonoBehaviour
    {
        private Enemy enemy;   // reference to stats
        private bool hasDealtDamage = false;
        private void Start()
        {
            enemy = GetComponent<Enemy>();
        }

        // Animation event calls this
        public void DealDamage()
        {
            Debug.Log("DealDamage fired!");
            if (hasDealtDamage) return;   
            hasDealtDamage = true;
            
            Collider[] hits = Physics.OverlapSphere(transform.position, 2f);

            foreach (var hit in hits)
            {
                if (hit.CompareTag("Player"))
                {
                    Player p = hit.GetComponent<Player>();
                    if (p != null)
                    {
                        Debug.Log("Player took damage");
                        Debug.Log(enemy.stats.attackDamage);
                        
                        p.TakeDamage(enemy.stats.attackDamage);
                        break;
                    }
                }
            }
        }
        
        public void ResetDamage()
        {
            hasDealtDamage = false;
        }
    }
}