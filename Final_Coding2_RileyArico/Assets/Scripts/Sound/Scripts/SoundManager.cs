using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Mixer")]
    public AudioMixer masterMixer;

    [Header("Audio Mixer Groups")]
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup sfxGroup;
    public AudioMixerGroup uiGroup;
    public AudioMixerGroup ambienceGroup;

    [Header("Pool Settings")]
    public int poolSize = 10;

    private Dictionary<SoundEvent, float> _cooldowns = new();
    private Queue<PooledAudioSource> _pool = new();
    private List<PooledAudioSource> _activeSources = new();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = new GameObject($"PooledAudio_{i}");
            obj.transform.SetParent(transform);
            var pooled = obj.AddComponent<PooledAudioSource>();
            pooled.Init(this, null); // Will assign group on use
            _pool.Enqueue(pooled);
        }
    }

    public void PlaySound(SoundEvent soundEvent)
    {
        if (soundEvent == null || soundEvent.clip == null) return;

        if (soundEvent.cooldown > 0f &&
            _cooldowns.TryGetValue(soundEvent, out float lastTime) &&
            Time.time - lastTime < soundEvent.cooldown) return;

        _cooldowns[soundEvent] = Time.time;

        var source = GetAvailableSource();
        if (source == null) return;

        source.GetComponent<AudioSource>().outputAudioMixerGroup = GetMixerGroup(soundEvent.category);
        source.Play(soundEvent);
        _activeSources.Add(source);
    }

    private PooledAudioSource GetAvailableSource()
    {
        if (_pool.Count > 0)
        {
            return _pool.Dequeue();
        }
        else
        {
            //Debug.LogWarning("Audio pool exhausted.");
            return null;
        }
    }

    public void ReleaseSource(PooledAudioSource source)
    {
        if (_activeSources.Contains(source))
            _activeSources.Remove(source);
        _pool.Enqueue(source);
    }

    public void StopAll()
    {
        foreach (var source in _activeSources)
        {
            if (source != null && source.IsPlaying)
                source.Stop();
        }
        _activeSources.Clear();
    }

    public void SetVolume(AudioCategory category, float volume01)
    {
        // Expects 0–1, converts to decibels
        float dB = Mathf.Log10(Mathf.Max(volume01, 0.001f)) * 20f;
        masterMixer.SetFloat($"{category}Volume", dB);
    }

    private AudioMixerGroup GetMixerGroup(AudioCategory category)
    {
        return category switch
        {
            AudioCategory.Music => musicGroup,
            AudioCategory.UI => uiGroup,
            AudioCategory.Ambience => ambienceGroup,
            _ => sfxGroup,
        };
    }
    public PooledAudioSource PlayLoop(SoundEvent soundEvent)
    {
        var source = GetAvailableSource();
        source.Play(soundEvent);
        return source;
    }
}
