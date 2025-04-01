using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SFB;
using System.IO;

public class ProfilPhoto : MonoBehaviour
{
    [SerializeField] private SavePlayer _savePlayer;
    [SerializeField] private RawImage _profileImage;
    [SerializeField] private Button _openFileButton;

    [SerializeField] private RawImage _previewProfileImage;

    private Texture2D _tempLoadedTexture = null;

    void Start()
    {
        _openFileButton.onClick.AddListener(OpenFileExplorer);
        _savePlayer.SaveButton.onClick.AddListener(SaveImage);

        LoadSavedImage();
    }

    void OpenFileExplorer()
    {
        var extensions = new[] { new ExtensionFilter("Images", "png", "jpg", "jpeg") };
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Sélectionnez une image", "", extensions, false);

        if (paths.Length > 0 && !string.IsNullOrEmpty(paths[0]))
        {
            StartCoroutine(TempLoadImage(paths[0]));
        }
    }

    IEnumerator TempLoadImage(string filePath)
    {
        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        if (texture.LoadImage(fileData))
        {
            _tempLoadedTexture = texture;
            _previewProfileImage.texture = _tempLoadedTexture;
        }
        yield return null;
    }

    void LoadSavedImage()
    {
        Texture2D savedTexture = _savePlayer.GetProfilImage();
        if (savedTexture != null)
        {
            _profileImage.texture = savedTexture;
            _profileImage.rectTransform.sizeDelta = new Vector2(100, 100);
        }
    }

    void SaveImage()
    {
        if (_tempLoadedTexture != null)
        {
            _profileImage.texture = _tempLoadedTexture;
            _profileImage.rectTransform.sizeDelta = new Vector2(100, 100);

            _savePlayer.SaveProfilImage(_tempLoadedTexture);
            _tempLoadedTexture = null;
        }
        else
        {
            _profileImage.texture = _previewProfileImage.texture;
            _profileImage.rectTransform.sizeDelta = new Vector2(100, 100);
            _savePlayer.SaveProfilImage((Texture2D)_profileImage.texture);
        }
    }
}
