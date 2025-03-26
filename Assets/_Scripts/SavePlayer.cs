using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayer : MonoBehaviour
{
    void Start()
    {
        LoadGame();
    }

    public void SaveGame(string playerName, float generalVolume)
    {
        PlayerPrefs.SetString("playerName", playerName);
        PlayerPrefs.SetFloat("volume", generalVolume);
        PlayerPrefs.Save();
        Debug.Log("Données sauvegardées");
    }

    public void LoadGame()
    {
        string playerName = PlayerPrefs.GetString("playerName", "Unknown");
        float generalVolume = PlayerPrefs.GetFloat("volume", 1.0f);
        float volume = PlayerPrefs.GetFloat("volume", 1.0f);

        Debug.Log("Nom du joueur: " + playerName);
        Debug.Log("Volume: " + volume);
    }
}
