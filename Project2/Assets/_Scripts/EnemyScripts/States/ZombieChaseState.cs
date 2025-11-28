using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.EnemyScripts.States
{
    public class ZombieChaseState : StateMachineBehaviour
    {
        NavMeshAgent agent;
        Transform player;

        /*public float chaseSpeed = 6f;
        public float stopChasingDistance = 21;
        public float attackingDistance = 2.5f;*/

        Enemy enemy;
        EnemyStats stats;
        
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            agent = animator.GetComponent<NavMeshAgent>();
            enemy = animator.GetComponent<Enemy>();
            stats = enemy.stats;
            agent.speed = stats.chaseSpeed;

        }


        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            agent.SetDestination(player.position);
            animator.transform.LookAt(player);
        
            float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

            if (distanceFromPlayer > stats.stopChasingDistance)
            {
                animator.SetBool("isChasing", false);
            }

            if (distanceFromPlayer < stats.attackRange)
            {
                animator.SetBool("isAttacking", true);
            }

        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            agent.SetDestination(animator.transform.position);

        }
    }
}


