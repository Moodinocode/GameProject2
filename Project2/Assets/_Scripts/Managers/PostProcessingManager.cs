using UnityEngine;

namespace _Scripts.Managers
{
    public class PostProcessingManager : MonoBehaviour
    {
        public static PostProcessingManager Instance;

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
