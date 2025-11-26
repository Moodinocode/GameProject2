using System.Collections;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public AudioSource audioSourceLight;
    public AudioSource audioSourceDark;
    public static BackgroundMusicManager Instance;

    public bool isLightMode = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartCoroutine(PreloadAudioSources());
    }

    private IEnumerator PreloadAudioSources()
    {
        audioSourceLight.Play();
        yield return new WaitForSeconds(0.05f);
        audioSourceLight.Pause();

        audioSourceDark.Play();
        yield return new WaitForSeconds(0.05f);
        audioSourceDark.Pause();
        
        isLightMode = true;
        audioSourceLight.Play();
        audioSourceDark.Stop();
    }

    public void SwitchMusic()
    {
        float currentTime;

        if (isLightMode)
        {
            currentTime = audioSourceLight.time;
            audioSourceLight.Pause();
            
            audioSourceDark.time = Mathf.Min(currentTime, audioSourceDark.clip.length - 0.01f);
            audioSourceDark.Play();
        }
        else
        {
            currentTime = audioSourceDark.time;
            audioSourceDark.Pause();

            audioSourceLight.time = Mathf.Min(currentTime, audioSourceLight.clip.length - 0.01f);
            audioSourceLight.Play();
        }

        isLightMode = !isLightMode;
    }
    
    public void PauseMusic()
    {
        if (isLightMode)
            audioSourceLight.Pause();
        else
            audioSourceDark.Pause();
    }

    public void ResumeMusic()
    {
        if (isLightMode)
            audioSourceLight.UnPause();
        else
            audioSourceDark.UnPause();
    }

}