using UnityEngine;

namespace _Scripts.Weapons
{
    public class WeaponRecoil : MonoBehaviour
    {
        [SerializeField] Transform recoilFollowPosition;
        [SerializeField] float kickBackAmount = -1;
        [SerializeField] float kickBackSpeed = 10,returnSpeed = 20;
        float _currentRecoilPoistion, _finalRecoilPosition;
        void Update()
        {
            _currentRecoilPoistion = Mathf.Lerp(_currentRecoilPoistion,0,returnSpeed * Time.deltaTime);
            _finalRecoilPosition = Mathf.Lerp(_finalRecoilPosition,_currentRecoilPoistion,kickBackSpeed * Time.deltaTime);
            recoilFollowPosition.localPosition = new Vector3(0,0,_finalRecoilPosition);
        }
        
        public void TriggerRecoil() => _currentRecoilPoistion += kickBackAmount;
    }
}
