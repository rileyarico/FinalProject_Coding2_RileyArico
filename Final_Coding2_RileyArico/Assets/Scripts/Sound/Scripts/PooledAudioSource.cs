using UnityEngine;
using UnityEngine.Audio;
public class PooledAudioSource : MonoBehaviour
{
    private AudioSource source;
    private SoundManager poolOwner;

    void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
    }

    public void Init(SoundManager owner, AudioMixerGroup mixerGroup)
    {
        poolOwner = owner;
        source.outputAudioMixerGroup = mixerGroup;
    }


    public void Play(SoundEvent soundEvent)
    {
        source.clip = soundEvent.clip;
        source.volume = soundEvent.volume;
        source.pitch = soundEvent.pitch + Random.Range(-soundEvent.pitchRandomization, soundEvent.pitchRandomization);
        source.loop = soundEvent.loop;
        source.Play();

        if (!soundEvent.loop)
            Invoke(nameof(Release), soundEvent.clip.length / source.pitch);
    }

    public void Stop()
    {
        source.Stop();
        Release();
    }

    private void Release()
    {
        poolOwner.ReleaseSource(this);
    }

    public bool IsPlaying => source.isPlaying;
}
