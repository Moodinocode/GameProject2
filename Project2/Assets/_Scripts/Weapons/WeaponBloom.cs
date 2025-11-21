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
        
        MovementStateManager _movement;
        AimStateManager _aim;
        
        float _currentBloom;

        void Start()
        {
            _movement = GetComponentInParent<MovementStateManager>();
            _aim = GetComponentInParent<AimStateManager>();
        
        }

        public Vector3 BloomAngle(Transform barrelPosition)
        {
            if (_movement.CurrentState == _movement.Idle) _currentBloom = defaultBloomAngle;
            else if (_movement.CurrentState == _movement.Walk) _currentBloom = defaultBloomAngle * walkBloomMultiplier;
            else if (_movement.CurrentState == _movement.Run) _currentBloom = defaultBloomAngle * sprintBloomMultiplier;
            else if (_movement.CurrentState == _movement.Crouch)
            {
                if (_movement.movementDirection.magnitude < 0.1f) _currentBloom = defaultBloomAngle * crouchBloomMultiplier;
                else _currentBloom = defaultBloomAngle * crouchBloomMultiplier * walkBloomMultiplier;
            }
            if(_aim.CurrentState == _aim.Aim) _currentBloom *= adsBloomMultiplier;
            
            float randX = Random.Range(-_currentBloom, _currentBloom);
            float randY = Random.Range(-_currentBloom, _currentBloom);
            float randZ = Random.Range(-_currentBloom, _currentBloom);
            Vector3 randomRotation = new Vector3(randX,randY,randZ);
            return barrelPosition.localEulerAngles + randomRotation;
        }

    }
}
