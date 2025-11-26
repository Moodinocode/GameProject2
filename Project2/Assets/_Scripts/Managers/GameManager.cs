using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public bool isPaused = false;
        
        /*[Header("UI References")]
        //public GameObject pauseMenu;
        //public GameObject optionsMenu;
    
        [Header("Audio")]
        //private AudioSource pauseMusic;*/
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else Destroy(gameObject);
        }
        
        
        void Start()
        {
            //pauseMusic = GameObject.FindGameObjectWithTag("UIAudio").GetComponent<AudioSource>();
            //pauseMusic.Pause();
        }

        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                /*if (GameUICanvas.Instance.optionsPanel.activeSelf)
                {
                    CloseOptions();
                }
                else
                {
                    TogglePause();
                }*/
            }
        }

        public void TogglePause()
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                Time.timeScale = 0f;
                /*GameUICanvas.Instance.pausePanel.SetActive(true);
                if (BackgroundMusicManager.Instance != null)
                {
                    BackgroundMusicManager.Instance.PauseMusic();
                }
                pauseMusic.Pause();*/
                
            }
            else
            {
                Time.timeScale = 1f;
                /*GameUICanvas.Instance.pausePanel.SetActive(false);
                if (BackgroundMusicManager.Instance != null)
                {
                    BackgroundMusicManager.Instance.ResumeMusic();
                }
                pauseMusic.UnPause();*/
            }
            
        }
        public void ResumeGame()
        {
            Debug.Log("resuming");
            TogglePause();
        }
        
        public void RestartLevel()
        {
            Time.timeScale = 1f;
            /*SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            if (BackgroundMusicManager.Instance != null)
            {
                BackgroundMusicManager.Instance.ResumeMusic();
            }*/
        }
        
        public void OpenOptions()
        {
            /*GameUICanvas.Instance.pausePanel.SetActive(false);
            GameUICanvas.Instance.optionsPanel.SetActive(true);*/
        }

        public void CloseOptions()
        {
            /*GameUICanvas.Instance.optionsPanel.SetActive(false);
            GameUICanvas.Instance.pausePanel.SetActive(true);*/
        }
        public void ExitToMainMenu()
        {
            SceneManager.LoadScene(0);
        
        }
        
        public void PlayGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            //ameUICanvas.Instance.dialoguePanel.SetActive(true);
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
