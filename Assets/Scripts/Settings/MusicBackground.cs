using System.Collections;
using Patterns.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicBackground : Singleton<MusicBackground>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip tutorialMusic;
    [SerializeField] private AudioClip comicsMusic;
    [SerializeField] private AudioClip finishMusic;
    [SerializeField] private AudioClip loseMusic;
    [SerializeField] private AudioClip[] gameMusic;
    [SerializeField] private AudioClip[] hangarMusic;
    [SerializeField] private float enableTime;
    [SerializeField] private float volumeValue;
    
    private void Start()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
        audioSource.clip = GetAudioClip(SceneManager.GetActiveScene());
        audioSource.volume = volumeValue;
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    public void EnableMusic()
    {
        StartCoroutine(Enable());
    }
    
    public void DisableMusic()
    {
        StartCoroutine(Disable());
    }

    private IEnumerator Enable()
    {
        var volume = audioSource.volume;
        while (volume < volumeValue)
        {
            volume += Time.deltaTime/ enableTime;
            audioSource.volume = volume;
            yield return null;
        }
    }

    private IEnumerator Disable()
    {
        var volume = audioSource.volume;
        while (volume > 0f)
        {
            volume -= Time.deltaTime/ enableTime;
            audioSource.volume = volume;
            yield return null;
        }
    }

    public void EnableGameMusic()
    {
        audioSource.clip = gameMusic[Random.Range(0, gameMusic.Length)];
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
    
    public void EnableHangarMusic()
    {
        audioSource.clip = hangarMusic[Random.Range(0, hangarMusic.Length)];
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
    
    private void ChangedActiveScene(Scene current, Scene next)
    {
        if (!audioSource.clip.Equals(GetAudioClip(next)))
        {
            audioSource.clip = GetAudioClip(next);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    private AudioClip GetAudioClip(Scene scene)
    {
        switch (scene.buildIndex)
        {
            case 0:
                return mainMenuMusic;
            case 1:
                return tutorialMusic;
            case 2:
                return comicsMusic;
            case 3:
                return gameMusic[Random.Range(0, gameMusic.Length)];
            case 4:
                return finishMusic;
            case 5:
                return loseMusic;
        }
        return null;
    }
}
