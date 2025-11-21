using UnityEngine;

namespace _Scripts.MovementStates.States
{
    public class RunState : MovementBaseState
    {
        private static readonly int Running = Animator.StringToHash("Running");

        public override void EnterState(MovementStateManager movement)
        {
            movement.anim.SetBool(Running, true);
        }

        public override void UpdateState(MovementStateManager movement)
        {
            if (Input.GetKeyUp(KeyCode.LeftShift)) ExitState(movement, movement.Walk);
            else if (movement.movementDirection.magnitude < 0.1f) ExitState(movement, movement.Idle);
            
            movement.currentMoveSpeed = movement.vInput<0 ? movement.runBackSpeed : movement.runSpeed;
        }
        
        void ExitState(MovementStateManager movement, MovementBaseState state)
        {
            movement.anim.SetBool(Running, false);
            movement.SwitchState(state);
        }
    }
}
