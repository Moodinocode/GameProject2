using UnityEngine;

namespace _Scripts.MovementStates.States
{
    public class CrouchState : MovementBaseState
    {
        private static readonly int Crouching = Animator.StringToHash("Crouching");

        public override void EnterState(MovementStateManager movement)
        {
            movement.anim.SetBool(Crouching, true);
        }

        public override void UpdateState(MovementStateManager movement)
        {
            if (Input.GetKeyUp(KeyCode.LeftShift)) ExitState(movement, movement.Run);
            else if (Input.GetKeyDown(KeyCode.C))
            {
                if (movement.movementDirection.magnitude < 0.1f) ExitState(movement, movement.Idle);
                else ExitState(movement, movement.Walk);
            }
            
            movement.currentMoveSpeed = movement.vInput<0 ? movement.crouchBackSpeed : movement.crouchSpeed;
            
        }
        
        void ExitState(MovementStateManager movement, MovementBaseState state)
        {
            movement.anim.SetBool(Crouching, false);
            movement.SwitchState(state);
        }
    }
}
