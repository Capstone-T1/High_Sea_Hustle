using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    void Start()
    {
        //Get the saved music volume, standard = 10f
        float music = PlayerPrefs.GetFloat(("mVolume"), 10f);
        float soundFX = PlayerPrefs.GetFloat(("sfxVolume"), 10f);

        //Set the music volume to the saved volume
        AdjustMusicVolume(music);
    }

    public void AdjustMusicVolume(float volume)
    {
        //Update AudioMixer
        audioMixer.SetFloat("musicVolume", volume);
        
        //Update PlayerPrefs "Music"
        PlayerPrefs.SetFloat("Music", volume);

        //Save changes
        PlayerPrefs.Save();
    }

    public void AdjustSoundFXVolume(float volume)
    {
        //Update AudioMixer
        audioMixer.SetFloat("soundFXVolume", volume);

        //Update PlayerPrefs "Music"
        PlayerPrefs.SetFloat("sfxVolume", volume);

        //Save changes
        PlayerPrefs.Save();
    }
}
