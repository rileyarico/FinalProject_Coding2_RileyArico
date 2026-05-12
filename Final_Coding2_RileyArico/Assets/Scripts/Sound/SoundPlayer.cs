using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public SoundEvent sound;

    private void Start()
    {
        PlayThis(sound);
    }
    public void PlayThis(SoundEvent sound)
    {
        SoundManager.Instance.PlayLoop(sound);
    }
}
