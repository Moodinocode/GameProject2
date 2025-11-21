using UnityEngine;

namespace _Scripts.MovementStates.States
{
    public class WalkState : MovementBaseState
    {
        private static readonly int Walking = Animator.StringToHash("Walking");

        public override void EnterState(MovementStateManager movement)
        {
            movement.anim.SetBool(Walking, true);
        }

        public override void UpdateState(MovementStateManager movement)
        {
            if(Input.GetKey(KeyCode.LeftShift)) ExitState(movement, movement.Run);
           else if (Input.GetKeyDown(KeyCode.C)) ExitState(movement, movement.Crouch);
           else if (movement.movementDirection.magnitude < 0.1f) ExitState(movement, movement.Idle);

            movement.currentMoveSpeed = movement.vInput<0 ? movement.walkBackSpeed : movement.walkSpeed;
        }
        
        void ExitState(MovementStateManager movement, MovementBaseState state)
        {
            movement.anim.SetBool(Walking, false);
            movement.SwitchState(state);
        }
    }
}
