using UnityEngine;

namespace _Scripts.EnemyScripts
{
    public class EnemyAttack : MonoBehaviour
    {
        private Enemy enemy;
        private bool hasDealtDamage = false;
        private ZombieAudio audio;
        private void Start()
        {
            enemy = GetComponent<Enemy>();
            audio = GetComponent<ZombieAudio>();
        }

        // Animation event calls this
        public void DealDamage()
        {
            if (enemy.isDead) return;
            if (hasDealtDamage) return;   
            hasDealtDamage = true;
            
            if (audio != null)
                audio.PlayAttack();
            
            Collider[] hits = Physics.OverlapSphere(transform.position, 2f);

            foreach (var hit in hits)
            {
                if (hit.CompareTag("Player"))
                {
                    Player p = hit.GetComponent<Player>();
                    if (p != null)
                    {
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