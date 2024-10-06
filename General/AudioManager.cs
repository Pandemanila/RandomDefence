using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgmSource;
    public static AudioManager instance;
    public Slider bgmSlider; 
    public Slider sfxSlider; 

    private float bgmVolume = 1.0f; 
    private float sfxVolume = 1.0f;
    void Awake()
    {
        bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
    }
    void Start()
    {
        if (bgmSlider != null)
        {
            bgmSlider.value = bgmVolume;
            bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = sfxVolume;
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }

        ApplyInitialVolume();
    }

    private void ApplyInitialVolume()
    {
        bgmSource.volume = bgmVolume;
        ApplySFXVolumeToAllSources();
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = volume;
        bgmSource.volume = bgmVolume;
        PlayerPrefs.SetFloat("BGMVolume", bgmVolume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;


        ApplySFXVolumeToAllSources();


        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }
    public void ApplySFXVolumeToAllSources()
    {

        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audioSource in allAudioSources)
        {
            if (audioSource != bgmSource)
            {
                audioSource.volume = sfxVolume;
            }
        }
    }
    public void ApplySFXVolume(AudioSource audioSource)
    {
        audioSource.volume = sfxVolume;
    }
}