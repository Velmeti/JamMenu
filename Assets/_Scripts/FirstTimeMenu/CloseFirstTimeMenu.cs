using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Execute action on first time menu close
/// </summary>
/// 
public class CloseFirstTimeMenu : MonoBehaviour
{
    [SerializeField] private FirstTimeManager _firstTimeManager;
    [SerializeField] private Fade _fade;
    [SerializeField] private Button _btnCloseMenu;
    [SerializeField] private float _closeFadeHold;

    void Start()
    {
        _btnCloseMenu.onClick.AddListener(CloseMenu);
    }

    
    private void CloseMenu()
    {
        StartCoroutine(WaitFade());
    }

    IEnumerator WaitFade()
    {
        _fade.StartFade(_fade.BaseFadeDuration, _fade.BaseFadeDuration, _closeFadeHold);
        yield return new WaitForSeconds(_fade.BaseFadeDuration);
        _firstTimeManager.FirstTimeCanvas.SetActive(false);
    }
}
