using UnityEngine;

namespace _Scripts.Managers
{
    public class SaveLoadManager : MonoBehaviour
    {
        public static SaveLoadManager Instance;
    
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
        
        public void SaveGame()
        {
         PlayerPrefs.Save();   
        }
        
    }
}
