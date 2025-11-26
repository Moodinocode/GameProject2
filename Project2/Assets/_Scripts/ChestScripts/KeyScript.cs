using UnityEngine;

namespace _Scripts.ChestScripts
{
    public class KeyScript : MonoBehaviour
    {
        private bool _canPickUp;
        private bool _hasKey;

        private GameObject _keyObject;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Key")) return;

            _canPickUp = true;
            _keyObject = other.gameObject;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Key")) return;

            _canPickUp = false;
            _keyObject = null;
        }

        private void Update()
        {
            if (_canPickUp && Input.GetKeyDown(KeyCode.E))
            {
                _hasKey = true;
                Destroy(_keyObject);
            }
        }
    }
}