using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.EnemyScripts.States
{
    public class ZombiePatrolingState : StateMachineBehaviour
    {
        private static readonly int IsPatroling = Animator.StringToHash("isPatroling");
        private static readonly int IsChasing = Animator.StringToHash("isChasing");

        float _timer;
        //public float patrolingTime = 10f;

        private Transform _player;
        NavMeshAgent _agent;
        ZombieAudio _audio;
    
        //public float detectionArea = 18f;
        //public float patrolSpeed = 2f;

        readonly List<Transform> _waypointsList = new List<Transform>();
    
        Enemy _enemy;
        EnemyStats _stats;
    
   
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _agent = animator.GetComponent<NavMeshAgent>();
            _enemy = animator.GetComponent<Enemy>();
            _stats = _enemy.stats;
            _audio = animator.GetComponent<ZombieAudio>();

        
            _agent.speed = _stats.patrolSpeed;
            _timer = 0;
        
            GameObject waypointCluster = GameObject.FindGameObjectWithTag("Waypoints");
            foreach (Transform t in waypointCluster.transform)
            {
                _waypointsList.Add(t);
            }
        
            Vector3 nextPosition = _waypointsList[Random.Range(0 , _waypointsList.Count)].position;
            _agent.SetDestination(nextPosition);

        }


        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_enemy.isDead) return;
            if (!_agent.enabled) return;
            if (!_agent.isOnNavMesh) return;
            
            if (_audio != null)
                _audio.TickPatrol();
            
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                _agent.SetDestination(_waypointsList[Random.Range(0, _waypointsList.Count)].position);
            
            }
            _timer += Time.deltaTime;
            if (_timer > _stats.patrolDuration)
            {
                animator.SetBool(IsPatroling, false);
            }
            float distanceFromPlayer = Vector3.Distance(_player.position, animator.transform.position);

            if (distanceFromPlayer <  _stats.detectionRadius)
            {
                animator.SetBool(IsChasing , true);
            }
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!_agent.enabled) return;
            if (!_agent.isOnNavMesh) return;
            
            _agent.SetDestination(_agent.transform.position);
        }
    }
}


