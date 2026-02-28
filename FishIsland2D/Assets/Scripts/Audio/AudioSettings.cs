using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    [Header("Volume Sliders")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("AudioMixerGroups")]
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Load saved values, or use default if they dont exist
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0f);
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0f);

        //Apply saved (or default) settings to sliders
        musicSlider.value = savedMusicVolume;
        sfxSlider.value = savedSFXVolume;

        //Apply them
        ApplyMusicVolume(savedMusicVolume);
        ApplySFXVolume(savedSFXVolume);
    }

    public void ApplyMusicVolume(float value)
    {
        if (musicMixer != null)
        {
            //Convert slider value to volume the audiomixer will recognize
            musicMixer.SetFloat("MusicVolume", value);
            PlayerPrefs.SetFloat("MusicVolume", value);
        }
    }

    public void ApplySFXVolume(float value)
    {
        if (sfxMixer != null)
        {
            //Convert slider value to volume the audiomixer will recognize
            sfxMixer.SetFloat("SFXVolume", value);
            PlayerPrefs.SetFloat("SFXVolume", value);
        }
    }
}
