using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SavePlayer : MonoBehaviour
{
    private string _playerNameKey = "playerName";
    private string _imageSaveKey = "playerImage";


    public void SaveGame(string playerName, Texture2D profileImage)
    {
        SavePlayerName(playerName);
        SaveProfilImage(profileImage);

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
}
