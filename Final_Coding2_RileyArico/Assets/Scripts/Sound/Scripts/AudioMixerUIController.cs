using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioMixerUIController : MonoBehaviour
{
    public AudioMixer audioMixer;             // Reference to the AudioMixer
    public string exposedParameterName;       // Name of the exposed parameter in the AudioMixer
    public Slider uiSlider;               // Reference to the UI slider

    void Start()
    {
        if (uiSlider == null || audioMixer == null || string.IsNullOrEmpty(exposedParameterName))
        {
            Debug.LogWarning("AudioMixerUIController is missing required references.");
            return;
        }

        // Initialize the slider value based on the AudioMixer parameter
        if (audioMixer.GetFloat(exposedParameterName, out float currentLevel))
        {
            uiSlider.value = Mathf.InverseLerp(-70f, 5f, currentLevel);
        }

        // Add listener
        uiSlider.onValueChanged.AddListener(SetLevel);
    }

    public void SetLevel(float value)
    {
        // Convert 0–1 slider value to -80–0 dB range
        float volumeInDecibels = Mathf.Lerp(-70f, 5f, value);
        audioMixer.SetFloat(exposedParameterName, volumeInDecibels);
    }
}
