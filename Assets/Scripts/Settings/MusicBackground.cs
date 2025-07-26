using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicBackground : Subscriber
{
    [SerializeField] private AudioSource mainMenuMusic;
    [SerializeField] private AudioSource tutorialMusic;
    [SerializeField] private AudioSource comicsMusic;
    [SerializeField] private AudioSource finishMusic;
    [SerializeField] private AudioSource loseMusic;
    [SerializeField] private AudioSource[] gameMusic;
    [SerializeField] private AudioSource[] hangarMusic;
    [SerializeField] private float enableTime;

    private Dictionary<AudioSource, float> _volumeBySource = new();
    
    private AudioSource _activeAudioSource;
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
            _instance = this;
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
        _activeAudioSource = GetAudioSource(SceneManager.GetActiveScene().buildIndex);
        _activeAudioSource.Play();
        SetVolume();
    }

    private void SetVolume()
    {
        _volumeBySource.TryAdd(mainMenuMusic, mainMenuMusic.volume);
        _volumeBySource.TryAdd(tutorialMusic, tutorialMusic.volume);
        _volumeBySource.TryAdd(comicsMusic, comicsMusic.volume);
        _volumeBySource.TryAdd(finishMusic, finishMusic.volume);
        _volumeBySource.TryAdd(loseMusic, loseMusic.volume);
        foreach (var audioSource in gameMusic)
        {
            _volumeBySource.TryAdd(audioSource, audioSource.volume);
        }

        foreach (var audioSource in hangarMusic)
        {
            _volumeBySource.TryAdd(audioSource, audioSource.volume);
        }
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
        while (volume < _volumeBySource[_activeAudioSource])
        {
            volume = Mathf.Clamp(volume + Time.deltaTime/ enableTime, 0f, _volumeBySource[_activeAudioSource]);
            _activeAudioSource.volume = volume;
            yield return null;
        }
    }

    private IEnumerator Disable()
    {
        var volume = _activeAudioSource.volume;
        while (volume > 0f)
        {
            volume = Mathf.Clamp(volume - Time.deltaTime/ enableTime, 0f, _volumeBySource[_activeAudioSource]);
            _activeAudioSource.volume = volume;
            yield return null;
        }
    }

    [Event("EnableGameMusic")]
    private void EnableGameMusic()
    {
        _activeAudioSource.Stop();
        _activeAudioSource = gameMusic[Random.Range(0, gameMusic.Length)];
        _activeAudioSource.Play();
    }
    
    [Event("EnableHangarMusic")]
    private void EnableHangarMusic()
    {
        _activeAudioSource.Stop();
        _activeAudioSource = hangarMusic[Random.Range(0, hangarMusic.Length)];
        _activeAudioSource.Play();
    }
    
    private void ChangedActiveScene(Scene current, Scene next)
    {
        if (_activeAudioSource != GetAudioSource(next.buildIndex))
        {
            _activeAudioSource.Stop();
            _activeAudioSource = GetAudioSource(next.buildIndex);
            if (!_activeAudioSource.isPlaying)
            {
                _activeAudioSource.Play();
                print(_activeAudioSource.name);
            }
        }
    }

    private AudioSource GetAudioSource(int index)
    {
        switch (index)
        {
            case 0:
                return mainMenuMusic;
            case 1:
                return tutorialMusic;
            case 2:
                return comicsMusic;
            case 3:
                foreach (var source in gameMusic)
                {
                    if (source.isPlaying)
                    {
                        return source;
                    }
                }
                return gameMusic[Random.Range(0, gameMusic.Length)];
            case 4:
                return finishMusic;
            case 5:
                return loseMusic;
        }
        return null;
    }
}
