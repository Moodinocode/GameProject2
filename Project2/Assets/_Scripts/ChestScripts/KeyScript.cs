using System;
using UnityEngine;

namespace _Scripts.ChestScripts
{
    public class KeyScript : MonoBehaviour
    {
        public bool hasKey;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Key"))
            {
                hasKey = true;
                Destroy(other);
            }
        }
    }
}
