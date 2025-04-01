using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSettings : MonoBehaviour
{
    void Start()
    {
        LoadGame();
    }

    public void SaveGame()
    {

    }


    public void SaveBinds()
    {

    }

    public void SaveAudio(float generalVolume, float musicVolume, float UIVolume)
    {
        PlayerPrefs.SetFloat("generalVolume", generalVolume);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("UIVolume", UIVolume);
        PlayerPrefs.Save();
        Debug.Log("Données sauvegardées");
    }


    public void LoadGame()
    {
        float generalVolume = PlayerPrefs.GetFloat("generalVolume", 1.0f);
        float musicVolume = PlayerPrefs.GetFloat("musicVolume", 0.25f);
        float UIVolume = PlayerPrefs.GetFloat("UIVolume", 1.0f);
    }


    public float GetGeneralVolume()
    {
        return PlayerPrefs.GetFloat("generalVolume", 1.0f);
    }

    public float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat("musicVolume", 1.0f);
    }

    public float GetUIVolume()
    {
        return PlayerPrefs.GetFloat("UIVolume", 1.0f);
    }
}
