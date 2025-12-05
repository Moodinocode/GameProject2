using UnityEngine;

namespace _Scripts.EnemyScripts
{
    public class EnemyAttack : MonoBehaviour
    {
        private Enemy _enemy;
        private bool _hasDealtDamage = false;
        private ZombieAudio _audio;
        private void Start()
        {
            _enemy = GetComponent<Enemy>();
            _audio = GetComponent<ZombieAudio>();
        }

        // Animation event calls this
        public void DealDamage()
        {
            if (_enemy.isDead) return;
            if (_hasDealtDamage) return;   
            _hasDealtDamage = true;
            
            if (_audio != null)
                _audio.PlayAttack();
            
            Collider[] hits = Physics.OverlapSphere(transform.position, 2f);

            foreach (var hit in hits)
            {
                if (hit.CompareTag("Player"))
                {
                    Player p = hit.GetComponent<Player>();
                    if (p != null)
                    {
                        p.TakeDamage(_enemy.stats.attackDamage);
                        break;
                    }
                }
            }
        }
        
        public void ResetDamage()
        {
            _hasDealtDamage = false;
        }
    }
}