using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Instantiate canvas and create fade effect
/// </summary>

public class Fade : MonoBehaviour
{
    [SerializeField] private GameObject _fadeCanvasPrefab;

    [Header("Values")]
    public float BaseFadeDuration = 1.0f;
    [SerializeField] private float _startHoldDuration = 0.5f;


    private void Awake()
    {
        StartFade(0, BaseFadeDuration, _startHoldDuration);
    }


    public void StartFade(float fadeInDuration, float fadeOutDuration, float holdDuration)
    {
        GameObject fadeCanvas = Instantiate(_fadeCanvasPrefab);
        Image fadeImage = fadeCanvas.GetComponentInChildren<Image>();

        StartCoroutine(FadeSequence(fadeImage, fadeCanvas, fadeInDuration, fadeOutDuration, holdDuration));
    }

    IEnumerator FadeSequence(Image fadeImage, GameObject fadeCanvas, float fadeInduration, float fadeOutDuration, float holdDuration)
    {
        Color color = fadeImage.color;

        float elapsedTime = 0f;
        while (elapsedTime < fadeInduration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, elapsedTime / fadeInduration);
            fadeImage.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(holdDuration);

        elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, elapsedTime / fadeOutDuration);
            fadeImage.color = color;
            yield return null;
        }

        Destroy(fadeCanvas);
    }
}
