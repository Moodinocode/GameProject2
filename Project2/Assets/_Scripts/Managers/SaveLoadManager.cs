using UnityEditor;
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
        
        public void QuitGame()
        {
            Debug.Log("Quit Game button pressed.");
    
            
            #if UNITY_EDITOR
                    EditorApplication.isPlaying = false;
            #else
                    // If running in a build, close the application
                    Application.Quit();
            #endif
        }
        
    }
}
