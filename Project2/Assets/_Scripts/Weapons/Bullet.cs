using UnityEngine;

namespace _Scripts.Weapons
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] float timeToDestory;
        float _timer;
        
        void Update()
        {
            _timer += Time.deltaTime;
            if (_timer > timeToDestory) Destroy(gameObject);
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            Destroy(this.gameObject);
        }
    }
}
