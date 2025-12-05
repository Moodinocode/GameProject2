using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.EnemyScripts.States
{
    public class ZombieAttackState : StateMachineBehaviour
    {
        private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
        Transform _player;
        NavMeshAgent _agent;

        //public float stopAttackingDistance = 2.5f;
        Enemy _enemy;
        EnemyStats _stats;
    
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _agent = animator.GetComponent<NavMeshAgent>();
            _enemy = animator.GetComponent<Enemy>();
            _stats = _enemy.stats;


        }


        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_enemy.isDead) return;
            if (!_agent.enabled) return;
            if (!_agent.isOnNavMesh) return;
            LookAtPlayer();
        
            float distanceFromPlayer = Vector3.Distance(_player.position, animator.transform.position);

            if (distanceFromPlayer > _stats.attackRange)
            {
                animator.SetBool(IsAttacking, false);
            }
        }

        private void LookAtPlayer()
        {
            Vector3 direction = _player.position - _agent.transform.position;
            _agent.transform.rotation = Quaternion.LookRotation(direction);
        
            var yRotation = _agent.transform.eulerAngles.y;
            _agent.transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}


