using UnityEngine;

namespace _Scripts.Managers
{
    public class EventSystemManager : MonoBehaviour
    {
        public static EventSystemManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);   
            
            }
        }
    }
}
