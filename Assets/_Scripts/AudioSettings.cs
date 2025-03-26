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


    private void Start()
    {
        SetGeneralVolume();
        SetMusicVolume();
        SetUIVolume();
    }


    public void SetGeneralVolume()
    {
        float volume = _slider_General_Volume.value;
        _menu_AudioMixer.SetFloat("general_volume", Mathf.Log10(volume) * 20);
        if (volume == 0)
        {
            _menu_AudioMixer.SetFloat("general_volume", -80);
        }
    }


    public void SetMusicVolume()
    {
        float volume = _slider_Music_Volume.value;
        _menu_AudioMixer.SetFloat("music_volume", Mathf.Log10(volume) * 20);
        if (volume == 0)
        {
            _menu_AudioMixer.SetFloat("music_volume", -80);
        }
    }

    
    public void SetUIVolume()
    {
        float volume = _slider_UI_Volume.value;
        _menu_AudioMixer.SetFloat("UI_volume", Mathf.Log10(volume) * 20);
        if (volume == 0)
        {
            _menu_AudioMixer.SetFloat("UI_volume", -80);
        }
    }
}
