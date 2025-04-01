using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SFB;
using System.IO;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// Change and load player name Photo
/// </summary>
public class ProfilPhoto : MonoBehaviour
{
    [SerializeField] private SavePlayer _savePlayer;
    [SerializeField] private RawImage _profileImage;
    [SerializeField] private Button[] _openFileButtons;

    [SerializeField] private RawImage[] _previewProfileImages;

    private Texture2D _tempLoadedTexture = null;

    private Button _currentOpenFileButton;
    private RawImage _currentPreviewProfileImage;

    void Start()
    {
        for (int i = 0; i < _openFileButtons.Length; i++)
        {
            int index = i;
            _openFileButtons[i].onClick.AddListener(() => OpenFileExplorer(index));
        }

        for (int y = 0; y < _savePlayer.SaveButtons.Length; y++)
        {
            _savePlayer.SaveButtons[y].onClick.AddListener(SaveImage);
        }

        LoadSavedImage();
    }


    void OpenFileExplorer(int index)
    {
        _currentOpenFileButton = _openFileButtons[index];
        _currentPreviewProfileImage = _previewProfileImages[index];

        var extensions = new[] { new ExtensionFilter("Images", "png", "jpg", "jpeg") };
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Sélectionnez une image", "", extensions, false);

        if (paths.Length > 0 && !string.IsNullOrEmpty(paths[0]))
        {
            StartCoroutine(TempLoadImage(paths[0], index));
        }
    }


    IEnumerator TempLoadImage(string filePath, int index)
    {
        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        if (texture.LoadImage(fileData))
        {
            _tempLoadedTexture = texture;
            _previewProfileImages[index].texture = _tempLoadedTexture;
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

            for (int i = 0; i < _previewProfileImages.Length; i++)
            {
                _previewProfileImages[i].texture = savedTexture;
            }
        }
    }


    void SaveImage()
    {
        if (_tempLoadedTexture != null)
        {
            _profileImage.texture = _tempLoadedTexture;
            _profileImage.rectTransform.sizeDelta = new Vector2(100, 100);

            _savePlayer.SaveProfilImage(_tempLoadedTexture);

            for (int i = 0; i < _previewProfileImages.Length; i++)
            {
                _previewProfileImages[i].texture = _tempLoadedTexture;
            }

            _tempLoadedTexture = null;
        }
        else
        {
            GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
            if (clickedButton != null)
            {
                int index = Array.IndexOf(_openFileButtons, clickedButton.GetComponent<Button>());
                if (index != -1)
                {
                    _profileImage.texture = _previewProfileImages[index].texture;
                    _profileImage.rectTransform.sizeDelta = new Vector2(100, 100);
                    _savePlayer.SaveProfilImage((Texture2D)_profileImage.texture);

                    for (int i = 0; i < _previewProfileImages.Length; i++)
                    {
                        _previewProfileImages[i].texture = _previewProfileImages[index].texture;
                    }
                }
            }
        }
    }

}
