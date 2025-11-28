using UnityEngine;

namespace _Scripts.EnemyScripts.States
{
    public class ZombieIdleState : StateMachineBehaviour
    {
        float timer;
        // public float idleTime = 0f;

        private Transform player;
        Enemy enemy;
        EnemyStats stats;
        //public float detectionAreaRadius = 18f;
    

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            enemy = animator.GetComponent<Enemy>();
            stats = enemy.stats;
            timer = 0;
            player = GameObject.FindGameObjectWithTag("Player").transform;
        

        }


        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            timer += Time.deltaTime;
            if (timer > stats.idleDuration)
            {
                animator.SetBool("isPatroling" , true);
            }
        
            float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

            if (distanceFromPlayer < stats.detectionRadius)
            {
                animator.SetBool("isChasing" , true);
            }

        }
    
    }
}

    