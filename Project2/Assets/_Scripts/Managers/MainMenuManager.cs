using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
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
