using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SFB;
using System.IO;

public class ImageLoader : MonoBehaviour
{
    public RawImage profileImage;
    public Button openFileButton;
    private string saveKey = "profileImagePath";

    void Start()
    {
        openFileButton.onClick.AddListener(OpenFileExplorer);

        LoadSavedImage();
    }

    void OpenFileExplorer()
    {
        var extensions = new[] { new ExtensionFilter("Images", "png", "jpg", "jpeg") };
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Sélectionnez une image", "", extensions, false);

        if (paths.Length > 0 && !string.IsNullOrEmpty(paths[0]))
        {
            string selectedPath = paths[0];
            PlayerPrefs.SetString(saveKey, selectedPath);
            PlayerPrefs.Save();

            StartCoroutine(LoadImage(selectedPath));
        }
    }

    void LoadSavedImage()
    {
        if (PlayerPrefs.HasKey(saveKey))
        {
            string savedPath = PlayerPrefs.GetString(saveKey);
            if (File.Exists(savedPath))
            {
                StartCoroutine(LoadImage(savedPath));
            }
        }
    }

    System.Collections.IEnumerator LoadImage(string filePath)
    {
        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(50, 50);
        if (texture.LoadImage(fileData))
        {
            profileImage.texture = texture;
            //profileImage.rectTransform.sizeDelta = new Vector2(50, 50);
        }
        yield return null;
    }
}
