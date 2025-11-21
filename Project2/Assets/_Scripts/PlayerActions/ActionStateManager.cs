using _Scripts.Weapons;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace _Scripts.PlayerActions
{
    public class ActionStateManager : MonoBehaviour
    {
        [HideInInspector] public ActionBaseState CurrentState;
        public ReloadState Reload = new ReloadState();
        public DefaultState Default = new DefaultState();
        public GameObject curretnWeapon;
        [HideInInspector] public WeaponAmmo ammo;
        AudioSource _audioSource;
        [HideInInspector] public Animator anim;

        public MultiAimConstraint rHandAim;
        public TwoBoneIKConstraint lHandIK;
        void Start()
        {
            ammo = curretnWeapon.GetComponent<WeaponAmmo>();
            _audioSource = curretnWeapon.GetComponent<AudioSource>();
            anim = GetComponent<Animator>();
            SwitchState(Default);
           
        }


        void Update()
        {
            CurrentState.UpdateState(this);
        }
        
        public void SwitchState(ActionBaseState state)
        {
            CurrentState = state;
            CurrentState.EnterState(this);
        }

        public void WeaponReloaded()
        {
            ammo.Reload();
            SwitchState(Default);
        }

        public void Magout()
        {
            _audioSource.PlayOneShot(ammo.magOutSound);
        }
        public void Magin()
        {
            _audioSource.PlayOneShot(ammo.magInSound);
        }

        public void ReleaseSlide()
        {
            _audioSource.PlayOneShot(ammo.releaseSlideSound);
        }
    }
}
