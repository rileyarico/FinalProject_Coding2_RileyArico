using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Audio/Sound Event")]
public class SoundEvent : ScriptableObject
{
    public AudioClip clip;
    public AudioCategory category = AudioCategory.SFX;
    [Range(0f, 2f)] public float volume = 1f;
    [Range(0.1f, 3f)] public float pitch = 1f;
    public float pitchRandomization = 0.05f;
    public bool loop;
    public float cooldown = 0f;
}