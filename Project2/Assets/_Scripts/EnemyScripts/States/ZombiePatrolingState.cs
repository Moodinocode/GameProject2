using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.EnemyScripts.States
{
    public class ZombiePatrolingState : StateMachineBehaviour
    {
        float timer;
        //public float patrolingTime = 10f;

        private Transform player;
        NavMeshAgent agent;
    
        //public float detectionArea = 18f;
        //public float patrolSpeed = 2f;
    
        List<Transform> waypointsList = new List<Transform>();
    
        Enemy enemy;
        EnemyStats stats;
    
   
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            agent = animator.GetComponent<NavMeshAgent>();
            enemy = animator.GetComponent<Enemy>();
            stats = enemy.stats;

        
            agent.speed = stats.patrolSpeed;
            timer = 0;
        
            GameObject waypointCluster = GameObject.FindGameObjectWithTag("Waypoints");
            foreach (Transform t in waypointCluster.transform)
            {
                waypointsList.Add(t);
            }
        
            Vector3 nextPosition = waypointsList[Random.Range(0 , waypointsList.Count)].position;
            agent.SetDestination(nextPosition);

        }


        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.SetDestination(waypointsList[Random.Range(0, waypointsList.Count)].position);
            
            }
            timer += Time.deltaTime;
            if (timer > stats.patrolDuration)
            {
                animator.SetBool("isPatroling", false);
            }
            float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

            if (distanceFromPlayer <  stats.detectionRadius)
            {
                animator.SetBool("isChasing" , true);
            }
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            agent.SetDestination(agent.transform.position);
        }
    }
}


