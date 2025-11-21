namespace _Scripts.AimStates
{
    public abstract class AimBaseState 
    {
        public abstract void EnterState(AimStateManager aim);
        public abstract void UpdateState(AimStateManager aim);
    }
}
