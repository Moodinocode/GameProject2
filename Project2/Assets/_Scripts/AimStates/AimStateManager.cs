using _Scripts.MovementStates;
using Cinemachine;
using UnityEngine;

namespace _Scripts.AimStates
{
    public class AimStateManager : MonoBehaviour
    {
        [Header("Aim States")]
        public AimBaseState CurrentState;
        public readonly HipFireState Hip = new HipFireState();
        public readonly AimState Aim = new AimState();
        
        [Header("Camera Settings")]
        [SerializeField] private float sensitivity = 1f;
        float _xAxis, _yAxis;
        [SerializeField] private Transform cameraFollowPosition;
        
        [Header("FOV")]
        [HideInInspector] public Animator anim;
        [HideInInspector] public CinemachineVirtualCamera virtualCamera;
        public float adsFov = 40;
        [HideInInspector] public float hipFov;
        [HideInInspector] public float currentFov;
        public float fovSmoothSpeed = 10;
        
        [Header("Aim")]
        [SerializeField] float aimSmoothSpeed = 20;
        [SerializeField] LayerMask aimMask;
        public Transform aimPosition;
        
        [Header("Camera Follow")]
        float _xFollowPosition;
        float _yFollowPosition, _oldYFollowPosition;
        [SerializeField] float crouchCamHeight = 0.6f;
        [SerializeField] float shoulderSwapSpeed = 10;
        MovementStateManager _movement;

        
        void Start()
        {
            _movement = GetComponent<MovementStateManager>();
            _xFollowPosition = cameraFollowPosition.localPosition.x;
            _oldYFollowPosition = cameraFollowPosition.localPosition.y;
            _yFollowPosition = _oldYFollowPosition;
            virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
            hipFov = virtualCamera.m_Lens.FieldOfView;
            anim = GetComponent<Animator>();
            SwitchState(Hip);
        }
    
        void Update()
        {
            _xAxis += Input.GetAxisRaw("Mouse X") * sensitivity;
            _yAxis -= Input.GetAxisRaw("Mouse Y") * sensitivity;
            _yAxis = Mathf.Clamp(_yAxis, -80f, 80f);

            CurrentState.UpdateState(this);
            MoveCamera();
            
            Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = Camera.main.ScreenPointToRay(screenCenter); //check null because the warning is annoying
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask)) aimPosition.position = Vector3.Lerp(aimPosition.position, hit.point, Time.deltaTime * aimSmoothSpeed);
            
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, currentFov, Time.deltaTime * fovSmoothSpeed);
        }

        private void LateUpdate()
        {
            cameraFollowPosition.localEulerAngles = new Vector3(_yAxis, cameraFollowPosition.localEulerAngles.y,cameraFollowPosition.localEulerAngles.z);
            transform.localEulerAngles = new Vector3(transform.eulerAngles.x, _xAxis,transform.localEulerAngles.z);
        }

        public void SwitchState(AimBaseState state)
        {
            CurrentState = state;
            CurrentState.EnterState(this);
        }
        
        void MoveCamera()
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt)) _xFollowPosition = -_xFollowPosition;
            _yFollowPosition = _movement.CurrentState == _movement.Crouch ? crouchCamHeight : _oldYFollowPosition;
            
            Vector3 newFollowPosition = new Vector3(_xFollowPosition, _yFollowPosition, cameraFollowPosition.localPosition.z);
            cameraFollowPosition.localPosition = Vector3.Lerp(cameraFollowPosition.localPosition, newFollowPosition, Time.deltaTime * shoulderSwapSpeed);
        }
        
    }
}
