using UnityEngine;

namespace _Scripts.AimStates
{
    public class AimState : AimBaseState
    {
        private static readonly int Aiming = Animator.StringToHash("Aiming");

        public override void EnterState(AimStateManager aim)
        {
           aim.anim.SetBool(Aiming,true);
           aim.currentFov = aim.adsFov;
        }

        public override void UpdateState(AimStateManager aim)
        {
            if(Input.GetKeyUp(KeyCode.Mouse1)) aim.SwitchState(aim.Hip); 
        }
    }
}
