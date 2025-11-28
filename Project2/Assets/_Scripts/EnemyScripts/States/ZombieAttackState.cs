using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.EnemyScripts.States
{
    public class ZombieAttackState : StateMachineBehaviour
    {
        Transform player;
        NavMeshAgent agent;

        //public float stopAttackingDistance = 2.5f;
        Enemy enemy;
        EnemyStats stats;
    
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            agent = animator.GetComponent<NavMeshAgent>();
            enemy = animator.GetComponent<Enemy>();
            stats = enemy.stats;


        }


        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            LookAtPlayer();
        
            float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

            if (distanceFromPlayer > stats.attackRange)
            {
                animator.SetBool("isAttacking", false);
            }
        }

        private void LookAtPlayer()
        {
            Vector3 direction = player.position - agent.transform.position;
            agent.transform.rotation = Quaternion.LookRotation(direction);
        
            var yRotation = agent.transform.eulerAngles.y;
            agent.transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}


