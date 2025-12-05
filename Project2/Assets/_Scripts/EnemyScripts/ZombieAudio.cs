using UnityEngine;

namespace _Scripts.EnemyScripts
{
    public class ZombieAudio : MonoBehaviour
    {
        [Header("Audio Sources")]
        public AudioSource audioSource;

        [Header("Patrol Sounds")]
        public AudioClip[] patrolClips;
        public float patrolMinDelay = 4f;
        public float patrolMaxDelay = 9f;
        private float _patrolTimer;

        [Header("Chase Sounds")]
        public AudioClip[] chaseClips;
        public float chaseMinDelay = 2f;
        public float chaseMaxDelay = 4f;
        private float _chaseTimer;

        [Header("Attack Sounds")]
        public AudioClip[] attackClips;

        [Header("Hurt Sounds")]
        public AudioClip[] hurtClips;

        [Header("Death Sounds")]
        public AudioClip[] deathClips;


        void Start()
        {
            ResetPatrolTimer();
            ResetChaseTimer();
        }
        
        void ResetPatrolTimer() => _patrolTimer = Random.Range(patrolMinDelay, patrolMaxDelay);
        void ResetChaseTimer() => _chaseTimer = Random.Range(chaseMinDelay, chaseMaxDelay);
        
        public void PlayAttack() => PlayRandom(attackClips);
        public void PlayHurt() => PlayRandom(hurtClips);
        public void PlayDeath() => PlayRandom(deathClips);

        public void TickPatrol()
        {
            _patrolTimer -= Time.deltaTime;
            if (_patrolTimer <= 0)
            {
                PlayRandom(patrolClips);
                ResetPatrolTimer();
            }
        }

        public void TickChase()
        {
            _chaseTimer -= Time.deltaTime;
            if (_chaseTimer <= 0)
            {
                PlayRandom(chaseClips);
                ResetChaseTimer();
            }
        }

        void PlayRandom(AudioClip[] clips)
        {
            if (clips.Length == 0) return;

            float pitch = Random.Range(0.95f, 1.05f);
            float volume = Random.Range(0.85f, 1.0f);

            audioSource.pitch = pitch;
            audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)], volume);
        }
    }
}
