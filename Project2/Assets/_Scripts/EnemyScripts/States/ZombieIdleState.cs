using UnityEngine;

namespace _Scripts.EnemyScripts.States
{
    public class ZombieIdleState : StateMachineBehaviour
    {
        private static readonly int IsPatroling = Animator.StringToHash("isPatroling");
        private static readonly int IsChasing = Animator.StringToHash("isChasing");

        float _timer;
        // public float idleTime = 0f;

        private Transform _player;
        Enemy _enemy;
        EnemyStats _stats;
        ZombieAudio _audio;
        //public float detectionAreaRadius = 18f;
    

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _enemy = animator.GetComponent<Enemy>();
            _stats = _enemy.stats;
            _timer = 0;
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        
            _audio = animator.GetComponent<ZombieAudio>();
        }


        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_enemy.isDead) return;
            if (_audio != null)
                _audio.TickPatrol();
            
            _timer += Time.deltaTime;
            if (_timer > _stats.idleDuration)
            {
                animator.SetBool(IsPatroling , true);
            }
        
            float distanceFromPlayer = Vector3.Distance(_player.position, animator.transform.position);

            if (distanceFromPlayer < _stats.detectionRadius)
            {
                animator.SetBool(IsChasing , true);
            }

        }
    
    }
}

    