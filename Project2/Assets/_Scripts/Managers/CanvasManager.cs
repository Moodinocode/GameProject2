using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUICanvas : MonoBehaviour
{
    public static GameUICanvas Instance;

    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject gameUIPanel;   
    public GameObject pausePanel;    
    public GameObject optionsPanel;
    public GameObject dialoguePanel;
    public GameObject creditPanel;

    void Awake()
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        mainMenuPanel?.SetActive(false);
        gameUIPanel?.SetActive(false);
        pausePanel?.SetActive(false);
        optionsPanel?.SetActive(false);
        dialoguePanel?.SetActive(false);
        creditPanel?.SetActive(false);
        
        if (scene.buildIndex == 0)
        {
            if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
        }
        else
        {
            if (scene.buildIndex == 1) dialoguePanel.SetActive(true);
        
            if (gameUIPanel != null) gameUIPanel.SetActive(true);
            //DialoguePanel?.SetActive(false);
        }
    }
}