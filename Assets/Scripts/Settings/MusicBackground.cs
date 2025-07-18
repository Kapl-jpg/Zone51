using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicBackground : Subscriber
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
    [SerializeField] private float loseVolumeValue;
    
    private static MusicBackground _instance;
    
    public static MusicBackground Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MusicBackground>();
                if (_instance == null)
                {
                    var singletonObject = new GameObject(typeof(MusicBackground).Name);
                    _instance = singletonObject.AddComponent<MusicBackground>();
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as MusicBackground;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
        audioSource.clip = GetAudioClip(SceneManager.GetActiveScene());
        audioSource.volume = volumeValue;
        if (SceneManager.GetActiveScene().buildIndex == 5)
            audioSource.volume = loseVolumeValue;
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    [Event("EnableMusic")]
    private void EnableMusic()
    {
        StartCoroutine(Enable());
    }
    
    [Event("DisableMusic")]
    private void DisableMusic()
    {
        StartCoroutine(Disable());
    }

    private IEnumerator Enable()
    {
        var volume = 0f;
        while (volume < volumeValue)
        {
            volume = Mathf.Clamp(volume + Time.deltaTime/ enableTime, 0f, volumeValue);
            audioSource.volume = volume;
            yield return null;
        }
    }

    private IEnumerator Disable()
    {
        var volume = audioSource.volume;
        while (volume > 0f)
        {
            volume = Mathf.Clamp(volume - Time.deltaTime/ enableTime, 0f, volumeValue);
            audioSource.volume = volume;
            yield return null;
        }
    }

    [Event("EnableGameMusic")]
    private void EnableGameMusic()
    {
        audioSource.clip = gameMusic[Random.Range(0, gameMusic.Length)];
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
    
    [Event("EnableHangarMusic")]
    private void EnableHangarMusic()
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
