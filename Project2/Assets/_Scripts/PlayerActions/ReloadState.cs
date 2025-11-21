using UnityEngine;

namespace _Scripts.PlayerActions
{
    public class ReloadState : ActionBaseState
    {
        private static readonly int Reload = Animator.StringToHash("Reload");

        public override void EnterState(ActionStateManager action)
        {
            action.rHandAim.weight = 0;
            action.lHandIK.weight = 0;
            action.anim.SetTrigger(Reload);
        }

        public override void UpdateState(ActionStateManager action)
        {
        }
    }
}
