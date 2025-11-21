using _Scripts.MovementStates.States;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.MovementStates
{
    public class MovementStateManager : MonoBehaviour
    {
        private static readonly int HzInput = Animator.StringToHash("hzInput");
        private static readonly int VInput = Animator.StringToHash("vInput");

        [Header("Movement Settings")]
        public float currentMoveSpeed;
        public float walkSpeed =3,walkBackSpeed =2; 
        public float crouchSpeed=2,crouchBackSpeed=1;
        public float runSpeed = 7, runBackSpeed = 5; 
        [HideInInspector] public Vector3 movementDirection;
        [HideInInspector]public float hzInput,vInput;
        private CharacterController _controller;
        
        [Header("Ground Check")]
        [SerializeField] float groundYOffset;
        [SerializeField] LayerMask groundMask;
        Vector3 _spherePosition;

        [Header("Gravity")]
        [SerializeField] float gravity = -9.81f;
        Vector3 _velocity;
        
        
        [Header("States")]
        public MovementBaseState CurrentState;
        public readonly IdleState Idle = new IdleState();
        public readonly WalkState Walk = new WalkState();
        public readonly RunState Run = new RunState();
        public readonly CrouchState Crouch = new CrouchState();
        [HideInInspector] public Animator anim; 
        
        void Start()
        {
            _controller = GetComponent<CharacterController>();
            anim = GetComponent<Animator>();
            SwitchState(Idle);
        }

        public void SwitchState(MovementBaseState state)
        {
            CurrentState = state;
            CurrentState.EnterState(this);
        }
        
        void Update()
        {
            GetDirectionAndMove();
            Gravity();
            
            anim.SetFloat(HzInput,hzInput);
            anim.SetFloat(VInput,vInput);
            CurrentState.UpdateState(this);
        }

        void GetDirectionAndMove()
        {
            hzInput = Input.GetAxisRaw("Horizontal");
            vInput = Input.GetAxisRaw("Vertical");
            movementDirection = transform.forward * vInput + transform.right * hzInput;
            
            _controller.Move(movementDirection.normalized * (currentMoveSpeed * Time.deltaTime));
        }

        bool IsGrounded()
        {
            _spherePosition = new Vector3(transform.position.x, transform.position.y -groundYOffset, transform.position.z);
            return Physics.CheckSphere(_spherePosition, _controller.radius -0.05f, groundMask);
        }

        void Gravity()
        {
            if (!IsGrounded()) _velocity.y += gravity * Time.deltaTime;
            else if (_velocity.y < 0) _velocity.y -= -2;
            
            _controller.Move(_velocity * Time.deltaTime);
        }

        
    }
}
