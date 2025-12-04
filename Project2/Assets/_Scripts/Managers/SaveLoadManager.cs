using _Scripts.ChestScripts;
using _Scripts.Weapons;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Managers
{
    public class SaveLoadManager : MonoBehaviour
    {
        public static SaveLoadManager Instance;
        
        public Player player;
        public WeaponAmmo ammo;
        public ChestOpening chestState;
        public KeyScript keyState;
        
    
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
            int index = SceneManager.GetActiveScene().buildIndex;
            SaveSystem.Save(index);
        }
        public void LoadGame()
        {
            Debug.Log("Loading Game");
            SaveSystem.Load();
        }
        
        public void QuitGame()
        {
            #if UNITY_EDITOR
                    EditorApplication.isPlaying = false;
            #else
                    // If running in a build, close the application
                    Application.Quit();
            #endif
        }
        
    }
}
