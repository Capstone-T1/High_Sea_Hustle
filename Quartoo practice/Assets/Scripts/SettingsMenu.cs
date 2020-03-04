using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider musicSlider;
    public Slider soundEffectsSlider;
    public AudioMixer masterMixer;

    void Awake()
    {
        float savedMusicVol = GameInfo.musicVolume;
        float savedSoundFXVol = GameInfo.soundFXVolume;

        // Manually set value & volume before subscribing to ensure it is set even if slider.value happens to start at the same value as is saved
        SetMusicVolume(savedMusicVol);
        SetSoundFXVolume(savedSoundFXVol);

        musicSlider.value = savedMusicVol;
        soundEffectsSlider.value = savedSoundFXVol;
    }

    public void SetMusicVolume(float volume)
    {
        masterMixer.SetFloat("musicVolume", ConvertToDecibel(volume / musicSlider.maxValue)); //Dividing by max allows arbitrary positive slider maxValue
        GameInfo.musicVolume = volume;
    }

    public void SetSoundFXVolume(float volume)
    {
        masterMixer.SetFloat("soundFXVolume", ConvertToDecibel(volume / soundEffectsSlider.maxValue)); //Dividing by max allows arbitrary positive slider maxValue
        GameInfo.soundFXVolume = volume;
    }

    //  Converts a percentage fraction to decibels,
    // with a lower clamp of 0.0001 for a minimum of -80dB, same as Unity's Mixers.
    public float ConvertToDecibel(float volume)
    {
        return Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20f;
    }
}
