using UnityEngine;

public class SoundManager : SingletonDontDestroy<SoundManager>
{
    [SerializeField][Range(0f, 1f)] private float soundEffectVolume;
    [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance;
    [SerializeField][Range(0f, 1f)] private float musicVolume;

    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioClip musicClip;
    
    [SerializeField] private ObjectPool objectPool;


    protected override void Awake()
    {
        base.Awake();

        musicAudioSource.volume = musicVolume;
        musicAudioSource.loop = true;
    }
    
    private void Start()
    {
        ChangeBackGroundMusic(musicClip);
    }

    private void ChangeBackGroundMusic(AudioClip music)
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = music;
        musicAudioSource.Play();
    }

    public void PlayClip(AudioClip clip)
    {
        GameObject obj = objectPool.SpawnFromPool("SoundSource");
        obj.SetActive(true);
        SoundSource soundSource = obj.GetComponent<SoundSource>();
        soundSource.Play(clip, soundEffectVolume, soundEffectPitchVariance);
    }

    public void SetBgmVolume(float volume)
    {
        musicVolume = volume;
    }
    
    public void SetEffectVolume(float volume)
    {
        soundEffectVolume = volume;
    }
}