using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SavePlayer : MonoBehaviour
{
    [SerializeField] private XP_System _xp_System;

    public Button[] SaveButtons;

    private string _playerNameKey = "playerName";
    private string _imageSaveKey = "playerImage";
    private string _playerLvlKey = "playerLevel";
    private string _playerXplKey = "playerXp";
    private string _playerXpForUplKey = "playerXpForUp";


    public void SaveGame(string playerName, Texture2D profileImage, int playerLvl, float playerXp, float playerXpForUp)
    {
        SavePlayerName(playerName);
        SaveProfilImage(profileImage);
        SavePlayerXPSystem(playerLvl, playerXp, playerXpForUp);
        PlayerPrefs.Save();


        Debug.Log("Data saved");
    }


    public void SavePlayerName(string playerName)
    {
        PlayerPrefs.SetString(_playerNameKey, playerName);
        Debug.Log("Player Name saved : " + playerName);
    }


    public void SaveProfilImage(Texture2D profileImage)
    {
        byte[] imageBytes = profileImage.EncodeToPNG();
        string base64String = System.Convert.ToBase64String(imageBytes);
        PlayerPrefs.SetString(_imageSaveKey, base64String);

        Debug.Log("Profil Image saved");
    }

    public void SavePlayerXPSystem(int playerLvl, float playerXp, float playerXpForUp)
    {
        PlayerPrefs.SetInt(_playerLvlKey, playerLvl);
        Debug.Log("Player Lvl saved : " + playerLvl);

        PlayerPrefs.SetFloat(_playerXplKey, playerXp);
        Debug.Log("Player Xp saved : " + playerXp);

        PlayerPrefs.SetFloat(_playerXpForUplKey, playerXpForUp);
        Debug.Log("Player Xp for up saved : " + playerXpForUp);
    }


    public string GetPlayerName()
    {
        return PlayerPrefs.GetString(_playerNameKey, "Unknown");
    }


    public Texture2D GetProfilImage()
    {
        if (PlayerPrefs.HasKey(_imageSaveKey))
        {
            string base64String = PlayerPrefs.GetString(_imageSaveKey);
            byte[] imageBytes = System.Convert.FromBase64String(base64String);

            Texture2D texture = new Texture2D(100, 100);
            if (texture.LoadImage(imageBytes))
            {
                return texture;
            }
        }
        return null;
    }


    public int GetPlayerLvl()
    {
        return PlayerPrefs.GetInt(_playerLvlKey, 1);
    }


    public float GetPlayerXp()
    {
        return PlayerPrefs.GetFloat(_playerXplKey, 0);
    }


    public float GetPlayerXpForUp()
    {
        return PlayerPrefs.GetFloat(_playerXpForUplKey, _xp_System.Lvl1_xpForUp);
    }
}
