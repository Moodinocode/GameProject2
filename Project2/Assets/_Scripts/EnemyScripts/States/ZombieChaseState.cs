using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.EnemyScripts.States
{
    public class ZombieChaseState : StateMachineBehaviour
    {
        private static readonly int IsChasing = Animator.StringToHash("isChasing");
        private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
        NavMeshAgent _agent;
        Transform _player;
        ZombieAudio _audio;

        /*public float chaseSpeed = 6f;
        public float stopChasingDistance = 21;
        public float attackingDistance = 2.5f;*/

        Enemy _enemy;
        EnemyStats _stats;
        
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _agent = animator.GetComponent<NavMeshAgent>();
            _enemy = animator.GetComponent<Enemy>();
            _stats = _enemy.stats;
            _agent.speed = _stats.chaseSpeed;
            _audio = animator.GetComponent<ZombieAudio>();

        }


        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_enemy.isDead) return;
            if (!_agent.enabled) return;
            if (!_agent.isOnNavMesh) return;
            
            if (_audio != null)
                _audio.TickChase();

            _agent.SetDestination(_player.position);
            animator.transform.LookAt(_player);
        
            float distanceFromPlayer = Vector3.Distance(_player.position, animator.transform.position);

            if (distanceFromPlayer > _stats.stopChasingDistance)
            {
                animator.SetBool(IsChasing, false);
            }

            if (distanceFromPlayer < _stats.attackRange)
            {
                animator.SetBool(IsAttacking, true);
            }

        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!_agent.enabled) return;
            if (!_agent.isOnNavMesh) return;
            _agent.SetDestination(animator.transform.position);
        }
    }
}


