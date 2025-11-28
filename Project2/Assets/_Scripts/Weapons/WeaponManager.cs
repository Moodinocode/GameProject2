using _Scripts.AimStates;
using _Scripts.ObjectPooling;
using _Scripts.PlayerActions;
using UnityEngine;

namespace _Scripts.Weapons
{
    public class WeaponManager : MonoBehaviour
    {
        [Header("Fire Rate")]
        [SerializeField] float fireRate;
        [SerializeField] bool semiAuto;
        private float _fireRateTimer;
        
        [Header("Bullet Properties")]
        [SerializeField] GameObject bullet;
        [SerializeField] Transform barrelPosition;
        [SerializeField] float bulletVelocity;
        [SerializeField] int bulletPerShot;
        AimStateManager _aim;
        
        [SerializeField] AudioClip gunShot;
        [SerializeField] AudioClip reloadReminder;
        AudioSource _audioSource;
        WeaponAmmo _ammo;
        ActionStateManager _action;
        WeaponRecoil _recoil;
        WeaponBloom _bloom;
        
        
        void Start()
        {
            _recoil = GetComponent<WeaponRecoil>();
            _bloom = GetComponent<WeaponBloom>();
            _audioSource = GetComponent<AudioSource>();
            _aim = GetComponentInParent<AimStateManager>();
            _ammo = GetComponent<WeaponAmmo>();
            _action = GetComponentInParent<ActionStateManager>();
            _fireRateTimer = fireRate; 
        }
        
        void Update()
        {
           if (ShouldFire()) Fire(); 
        }

        bool ShouldFire()
        {
            _fireRateTimer += Time.deltaTime;
            if (_fireRateTimer < fireRate) return false;
            if (_ammo.currentAmmo == 0) return false;
            if (_action.CurrentState == _action.Reload) return false;
            if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;
            if (!semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;
            return false;
        }
        
        void Fire()
        {
            _fireRateTimer = 0f;
            barrelPosition.LookAt(_aim.aimPosition);
            barrelPosition.localEulerAngles = _bloom.BloomAngle(barrelPosition);
            _audioSource.PlayOneShot(gunShot);
            _ammo.currentAmmo--;
            _recoil.TriggerRecoil();
            
            if (_ammo.currentAmmo == 5)
            {
                _audioSource.PlayOneShot(reloadReminder);
            }
            for (int i = 0; i < bulletPerShot; i++)
            {
               //GameObject currentBullet = Instantiate(bullet, barrelPosition.position, barrelPosition.rotation);
               GameObject currentBullet = ObjectPooler.Instance.GetFromPool(
                   "Bullet",
                   barrelPosition.position,
                   barrelPosition.rotation
               );

               //Rigidbody currentBulletRigidbody = currentBullet.GetComponent<Rigidbody>();
               //currentBulletRigidbody.AddForce(barrelPosition.forward * bulletVelocity,ForceMode.Impulse);
            }
        }
    }
}
