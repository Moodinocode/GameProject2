using UnityEngine;

namespace _Scripts.AimStates
{
    public class HipFireState : AimBaseState
    {
        private static readonly int Aiming = Animator.StringToHash("Aiming");

        public override void EnterState(AimStateManager aim)
        {
            aim.anim.SetBool(Aiming,false);
            aim.currentFov = aim.hipFov;
        }

        public override void UpdateState(AimStateManager aim)
        {
            if(Input.GetKey(KeyCode.Mouse1)) aim.SwitchState(aim.Aim);
        }
    }
}
