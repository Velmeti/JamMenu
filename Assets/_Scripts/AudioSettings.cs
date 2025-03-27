using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _menu_AudioMixer;
    [SerializeField] private Slider _slider_General_Volume;
    [SerializeField] private Slider _slider_Music_Volume;
    [SerializeField] private Slider _slider_UI_Volume;


    [SerializeField] private SaveSettings _saveSettings;

    private void Start()
    {
        LoadAudioSettings();
    }


    private void LoadAudioSettings()
    {        
        float generalVolume = _saveSettings.GetGeneralVolume();
        float musicVolume = _saveSettings.GetMusicVolume();
        float UIVolume = _saveSettings.GetUIVolume();

        _slider_General_Volume.value = generalVolume;
        _slider_Music_Volume.value = musicVolume;
        _slider_UI_Volume.value = UIVolume;

        _menu_AudioMixer.SetFloat("general_volume", Mathf.Log10(generalVolume) * 20);
        _menu_AudioMixer.SetFloat("music_volume", Mathf.Log10(musicVolume) * 20);
        _menu_AudioMixer.SetFloat("UI_volume", Mathf.Log10(UIVolume) * 20);

        if (generalVolume == 0)
            _menu_AudioMixer.SetFloat("general_volume", -80);
        if (musicVolume == 0)
            _menu_AudioMixer.SetFloat("music_volume", -80);
        if (UIVolume == 0)
            _menu_AudioMixer.SetFloat("UI_volume", -80);
    }



    public void SetGeneralVolume()
    {
        float volume = _slider_General_Volume.value;
        _menu_AudioMixer.SetFloat("general_volume", Mathf.Log10(volume) * 20);
        if (volume == 0)
        {
            _menu_AudioMixer.SetFloat("general_volume", -80);
        }

        SaveAudioSettings();
    }


    public void SetMusicVolume()
    {
        float volume = _slider_Music_Volume.value;
        _menu_AudioMixer.SetFloat("music_volume", Mathf.Log10(volume) * 20);
        if (volume == 0)
        {
            _menu_AudioMixer.SetFloat("music_volume", -80);
        }

        SaveAudioSettings();
    }

    
    public void SetUIVolume()
    {
        float volume = _slider_UI_Volume.value;
        _menu_AudioMixer.SetFloat("UI_volume", Mathf.Log10(volume) * 20);
        if (volume == 0)
        {
            _menu_AudioMixer.SetFloat("UI_volume", -80);
        }

        SaveAudioSettings();
    }



    private void SaveAudioSettings()
    {
        float generalVolume = _slider_General_Volume.value;
        float musicVolume = _slider_Music_Volume.value;
        float UIVolume = _slider_UI_Volume.value;

        _saveSettings.SaveAudio(generalVolume, musicVolume, UIVolume);
    }
}
