using Patterns.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicBackground : Singleton<MusicBackground>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip tutorialMusic;
    [SerializeField] private AudioClip comicsMusic;
    [SerializeField] private AudioClip gameMusic;
    [SerializeField] private AudioClip finishMusic;
    [SerializeField] private AudioClip loseMusic;

    private void Start()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
        audioSource.clip = GetAudioClip(SceneManager.GetActiveScene());
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
                return gameMusic;
            case 4:
                return finishMusic;
            case 5:
                return loseMusic;
        }
        return null;
    }
}
