using UnityEngine;

namespace _Scripts.EnemyScripts
{
    [CreateAssetMenu(fileName = "EnemyStats", menuName = "Enemy/Stats")]
    public class EnemyStats : ScriptableObject
    {
        [Header("Health")] public int maxHP = 100;
        
        [Header("Movement")]
        public float patrolSpeed = 2f;
        public float chaseSpeed = 6f;

        [Header("Detection")]
        public float detectionRadius = 18f;
        public float attackRange = 2.5f;
        public float stopChasingDistance = 21f;
        
        [Header("Behavior Timers")]
        public float patrolDuration = 10f;
        public float idleDuration = 0f;
        
        [Header("Damage")]
        public int attackDamage = 20;

        
    }
}
