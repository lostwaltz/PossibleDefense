using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : SingletonDontDestroy<SoundManager>
{
    [Header ("Volumes")]
    [SerializeField][Range(0f, 1f)] private float soundEffectVolume;
    [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance;
    [SerializeField][Range(0f, 1f)] private float BGMVolume;

    [Header("BGM")]
    [SerializeField] private AudioSource BGMSource;  //for BGM
    [SerializeField] private AudioClip BGM;

    [Header ("SFX")]
    [SerializeField] private List<SoundData> soundDatas;
    [SerializeField] private int initSize = 30;
    [SerializeField] private AudioSource audioSourcePrefab;  //only has AudioSource

    private Dictionary<string, AudioClip> soundDictionary = new Dictionary<string, AudioClip>();
    private Queue<AudioSource> audioSourcePool = new Queue<AudioSource>();
    private HashSet<AudioClip> playingAudioClips = new HashSet<AudioClip>();


    protected override void Awake()
    {
        base.Awake();

        BGMSource.volume = BGMVolume;
        BGMSource.loop = true;

        InitPool();
        InitDictionary();
    }
    
    private void Start()
    {
        ChangeBackGroundMusic(BGM);
    }

    private void InitPool()
    {
        for(int i = 0; i < initSize; i++)
        {
            CreateAudioSource();
        }
    }

    private void CreateAudioSource()
    {
        AudioSource newAudio = Instantiate(audioSourcePrefab, this.transform);
        newAudio.gameObject.SetActive(false);
        audioSourcePool.Enqueue(newAudio);
    }

    private void InitDictionary()
    {
        foreach(var sound in soundDatas)
        {
            if (!soundDictionary.ContainsKey(sound.id))
            {
                soundDictionary.Add(sound.id, sound.clip);
            }
        }
    }

    private void ChangeBackGroundMusic(AudioClip music)
    {
        BGMSource.Stop();
        BGMSource.clip = music;
        BGMSource.Play();
    }

    public void PlayClip(string id, Vector3 position)
    {
        if (!soundDictionary.ContainsKey(id))
        {
            Debug.Log("don't have " + id);
            return;
        }

        AudioClip clip = soundDictionary[id];
        //if (playingAudioClips.Contains(clip))
        //{
        //    return;
        //}
        playingAudioClips.Add(clip);
        AudioSource _audioSource = GetAudioSource();
        _audioSource.transform.position = position;

        //Play Clip
        _audioSource.clip = clip;
        _audioSource.volume = soundEffectVolume;
        _audioSource.Play();
        _audioSource.pitch = 1f + Random.Range(-soundEffectPitchVariance, soundEffectPitchVariance);

        //Stop after playing
        StartCoroutine(ReturnToPoolAfterPlaying(_audioSource, clip));
    }

    public void PlayClipDuplicated(string id, Vector3 position)
    {

    }

    private AudioSource GetAudioSource()
    {
        AudioSource newSource;

        if (audioSourcePool.Count <= 0)
        {
            CreateAudioSource();
        }
        newSource = audioSourcePool.Dequeue();
        newSource.gameObject.SetActive(true);

        return newSource;
    }

    private IEnumerator ReturnToPoolAfterPlaying(AudioSource audioSource, AudioClip clip)
    {
        yield return new WaitForSeconds(audioSource.clip.length + 1);
        playingAudioClips.Remove(clip);
        audioSource.Stop();
        audioSource.clip = null;
        audioSource.gameObject.SetActive(false);
        audioSourcePool.Enqueue(audioSource);
    }

    public void SetBgmVolume(float volume)
    {
        BGMVolume = volume;
        BGMSource.volume = BGMVolume;
    }
    
    public void SetEffectVolume(float volume)
    {
        soundEffectVolume = volume;
    }
}