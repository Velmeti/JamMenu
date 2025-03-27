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


    void Start()
    {
        _openFileButton.onClick.AddListener(OpenFileExplorer);

        Texture2D savedTexture = FindObjectOfType<SavePlayer>().GetProfilImage();
        if (savedTexture != null)
        {
            _profileImage.texture = savedTexture;
            _profileImage.rectTransform.sizeDelta = new Vector2(100, 100);
        }
    }

    void OpenFileExplorer()
    {
        var extensions = new[] { new ExtensionFilter("Images", "png", "jpg", "jpeg") };
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Sélectionnez une image", "", extensions, false);

        if (paths.Length > 0 && !string.IsNullOrEmpty(paths[0]))
        {
            StartCoroutine(LoadAndSaveImage(paths[0]));
        }
    }

    System.Collections.IEnumerator LoadAndSaveImage(string filePath)
    {
        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        if (texture.LoadImage(fileData))
        {
            _profileImage.texture = texture;
            _profileImage.rectTransform.sizeDelta = new Vector2(100, 100);

            FindObjectOfType<SavePlayer>().SaveProfilImage(texture);
        }
        yield return null;
    }
}
