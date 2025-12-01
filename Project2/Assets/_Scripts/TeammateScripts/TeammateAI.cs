using _Scripts.EnemyScripts;
using _Scripts.ObjectPooling;
using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.TeammateScripts
{
    public class TeammateAI : MonoBehaviour
    {
        public Transform player;
        public float followDistance = 5f;
        public float shootingRange = 30f;
        public float fireRate = 0.75f;

        public Transform projectileSpawn;
        public float bulletSpeed = 40f;

        private NavMeshAgent agent;
        private Animator anim;
        private float fireTimer;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            fireTimer -= Time.deltaTime;

            FollowPlayer();
            HandleCombat();
            UpdateAnimations();
        }

        private void FollowPlayer()
        {
            if (!player) return;

            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > followDistance)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                agent.ResetPath();
            }
        }

        private void HandleCombat()
        {
            GameObject zombie = FindClosestZombie();
            if (!zombie) return;

            float distance = Vector3.Distance(transform.position, zombie.transform.position);

            // Rotate smoothly toward target
            Vector3 direction = zombie.transform.position - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(direction),
                10f * Time.deltaTime
            );

            if (distance <= shootingRange && fireTimer <= 0)
            {
                anim.SetTrigger("Attack");
                Fire(zombie.transform);
                fireTimer = fireRate;
            }
        }

        private void UpdateAnimations()
        {
            float speed = agent.velocity.magnitude;
            anim.SetFloat("Speed", speed);
        }

        private GameObject FindClosestZombie()
        {
            GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
            GameObject closest = null;
            float minDist = Mathf.Infinity;

            foreach (var z in zombies)
            {
                Enemy enemyComp = z.GetComponent<Enemy>();
                if (enemyComp == null) continue;
                if (enemyComp.isDead) continue;

                float dist = Vector3.Distance(transform.position, z.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = z;
                }
            }

            return closest;
        }

        private void Fire(Transform target)
        {
            // Compute direction first
            Vector3 direction = (target.position - projectileSpawn.position).normalized;

            // Pull bullet from pool
            GameObject bullet = ObjectPooler.Instance.GetFromPool(
                "Bullet",
                projectileSpawn.position,
                Quaternion.LookRotation(direction)
            );

            // Apply velocity
            if (bullet.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.velocity = direction * bulletSpeed;
            }
        }
    }
}
