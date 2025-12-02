using _Scripts.AimStates;
using _Scripts.MovementStates;
using UnityEngine;

namespace _Scripts.Weapons
{
    public class WeaponBloom : MonoBehaviour
    {
        [SerializeField] private float defaultBloomAngle = 3;
        [SerializeField] private float walkBloomMultiplier = 1.5f;
        [SerializeField] private float crouchBloomMultiplier = 0.75f;
        [SerializeField] private float sprintBloomMultiplier = 2f;
        [SerializeField] private float adsBloomMultiplier = 0.5f;
        
        [SerializeField] private float bloomIncreasePerShot = 1.4f;
        [SerializeField] private float bloomRecoverySpeed = 5f;  // how fast bloom goes down

        private float timeSinceLastShot = 0f;
        [SerializeField] private float bloomResetDelay = 1.5f;   // seconds of no firing before full reset

        MovementStateManager _movement;
        AimStateManager _aim;
        
        float _currentBloom;
        
        public float CurrentBloom => _currentBloom;


        void Start()
        {
            _movement = GetComponentInParent<MovementStateManager>();
            _aim = GetComponentInParent<AimStateManager>();
        
        }
        void Update()
        {
            float targetBloom = defaultBloomAngle;

            if (_movement.CurrentState == _movement.Walk)
                targetBloom = defaultBloomAngle * walkBloomMultiplier;

            else if (_movement.CurrentState == _movement.Run)
                targetBloom = defaultBloomAngle * sprintBloomMultiplier;

            else if (_movement.CurrentState == _movement.Crouch)
            {
                if (_movement.movementDirection.magnitude < 0.1f)
                    targetBloom = defaultBloomAngle * crouchBloomMultiplier;
                else
                    targetBloom = defaultBloomAngle * crouchBloomMultiplier * walkBloomMultiplier;
            }

            if (_aim.CurrentState == _aim.Aim)
                targetBloom *= adsBloomMultiplier;

            // Smooth recovery towards target bloom
            _currentBloom = Mathf.Lerp(_currentBloom, targetBloom, Time.deltaTime * bloomRecoverySpeed);
            timeSinceLastShot += Time.deltaTime;
            if (timeSinceLastShot >= bloomResetDelay)
            {
                _currentBloom = Mathf.Lerp(_currentBloom, defaultBloomAngle, Time.deltaTime * bloomRecoverySpeed);
            }
        }


        public Vector3 BloomAngle(Transform barrelPosition)
        {
            // ADD bloom increase here when firing
            _currentBloom += bloomIncreasePerShot;
            timeSinceLastShot = 0f;
            float randX = Random.Range(-_currentBloom, _currentBloom);
            float randY = Random.Range(-_currentBloom, _currentBloom);
            float randZ = Random.Range(-_currentBloom, _currentBloom);

            return barrelPosition.localEulerAngles + new Vector3(randX, randY, randZ);
        }


    }
}
