using UnityEngine;

namespace _Scripts.PlayerActions
{
    public class DefaultState : ActionBaseState
    {
        public override void EnterState(ActionStateManager action)
        {
            
        }

        public override void UpdateState(ActionStateManager action)
        {
            action.rHandAim.weight = Mathf.Lerp(action.rHandAim.weight,1,Time.deltaTime*10);
            action.lHandIK.weight = Mathf.Lerp(action.lHandIK.weight,1,Time.deltaTime*10);
            if (Input.GetKeyDown(KeyCode.R) && CanReload(action))
            {
                action.SwitchState(action.Reload);
            }
        }

        bool CanReload(ActionStateManager action)
        {
            if(action.ammo.currentAmmo == action.ammo.clipSize)return false;
            else if (action.ammo.extraAmmo == 0) return false;
            else return true;
        }
    }
}
