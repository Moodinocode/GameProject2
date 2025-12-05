using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace _Scripts.Managers
{
    public class CanvasManager : MonoBehaviour
    {
        public static CanvasManager Instance;
        
        private readonly List<GameObject> _dynamicUI = new List<GameObject>();

        
        private enum UIContext
        {
            None,
            MainMenu,
            PauseMenu
        }
        
        private UIContext _currentContext = UIContext.None;

        [Header("Panels")]
        public GameObject mainMenuPanel;
        public GameObject gameUIPanel;
        public GameObject pausePanel;
        public GameObject optionsPanel;
        public GameObject gameOverPanel;

        private bool _isPaused;
        public static bool GamePaused = false; 

        public Canvas gameUICanavs;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            ShowMainMenu();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex == 3)
            {
                DisableAllChildren();
                return;
            }
            if (scene.buildIndex == 0) 
            {
                ShowMainMenu();
            }
            else
            {
                ShowGameUI();
            }
            
            if (pausePanel != null)
                pausePanel.SetActive(false);

            _isPaused = false;
            Time.timeScale = 1f;
        }
        
        private void DisableAllChildren()
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }


        void Update()
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                    TogglePause();
            }
        }

        public void ShowMainMenu()
        {
            if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
            if (gameUIPanel != null) gameUIPanel.SetActive(false);
            if (pausePanel != null) pausePanel.SetActive(false);
            if (gameOverPanel != null) gameOverPanel.SetActive(false);
            
            DisableAllDynamicUI();
        }

        public void ShowGameUI()
        {
            if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
            if (gameUIPanel != null) gameUIPanel.SetActive(true);
        }

        public void TogglePause()
        {
            _isPaused = !_isPaused;
            GamePaused = _isPaused;

            if (pausePanel)
                pausePanel.SetActive(_isPaused);

            Time.timeScale = _isPaused ? 0f : 1f;
        }
        public void OnPlayButton()
        {
            SceneManager.LoadScene(1);
        }
        
        public void OnMainMenuButton()
        {
            TogglePause();
            Time.timeScale = 1f;
            
            DisableAllDynamicUI();
            SceneManager.LoadScene(0); 
        }
        
        public void OpenOptions()
        {
            if (mainMenuPanel.activeSelf)
                _currentContext = UIContext.MainMenu;
            else if (pausePanel.activeSelf)
                _currentContext = UIContext.PauseMenu;
            
            mainMenuPanel.SetActive(false);
            pausePanel.SetActive(false);
            gameUIPanel.SetActive(false);

            optionsPanel.SetActive(true);
        }

        public void CloseOptions()
        {
            optionsPanel.SetActive(false);

            switch (_currentContext)
            {
                case UIContext.MainMenu:
                    mainMenuPanel.SetActive(true);
                    break;

                case UIContext.PauseMenu:
                    pausePanel.SetActive(true);
                    break;
            }
        }
        
        public void RegisterDynamicUI(GameObject uiObject)
        {
            if (!_dynamicUI.Contains(uiObject))
                _dynamicUI.Add(uiObject);
        }
        
        private void DisableAllDynamicUI()
        {
            foreach (var ui in _dynamicUI)
            {
                if (ui != null)
                    ui.SetActive(false);
            }
        }

        public void ShowGameOver()
        {
            if (pausePanel) pausePanel.SetActive(false);
            if (gameUIPanel) gameUIPanel.SetActive(false);
            if (mainMenuPanel) mainMenuPanel.SetActive(false);

            DisableAllDynamicUI();

            
            if (gameOverPanel)
                gameOverPanel.SetActive(true);

           
            Time.timeScale = 0f;
            GamePaused = true;
            
            StartCoroutine(AutoReturnToMainMenu());
        }
        
        private IEnumerator AutoReturnToMainMenu()
        {
            yield return new WaitForSecondsRealtime(7f);
            
            Time.timeScale = 1f;
            
            SceneManager.LoadScene(0);
            gameOverPanel.SetActive(false);
        }



    }
}
