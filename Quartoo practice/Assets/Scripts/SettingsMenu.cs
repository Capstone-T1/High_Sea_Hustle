using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public Slider volume;
    //public AudioMixer audioMixer;

    public void SetMusicVolume (float volume)
    {
        //audioMixer.SetFloat("volume", volume);
        Debug.Log(volume);
    }

    public void SetSoundEffectsVolume (float volume)
    {
        Debug.Log(volume);
    }
}
