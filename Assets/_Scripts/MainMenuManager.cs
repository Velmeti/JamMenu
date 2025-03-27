using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private XP_System _xp_System;

    /*
    [SerializeField] private Button _homeButton;
    [SerializeField] private Sprite _homeButtonAsset;
    [SerializeField] private Button _progressionButton;
    [SerializeField] private Sprite _progressionButtonAsset;
    [SerializeField] private Button _armoryButton;
    [SerializeField] private Sprite _armoryButtonAsset;
    [SerializeField] private Button _storeButton;
    [SerializeField] private Sprite _storeButtonAsset;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Sprite _settingsButtonAsset;
    */

    [SerializeField] private MenuInfos[] _menus;
    private MenuInfos _currentMenu;
    [SerializeField] private Sprite _invisibleSprite;

    [SerializeField] private MenuWindows _menuWindows;
    private int _currentHomeWindow = 0;

    [SerializeField] private TextMeshProUGUI _lvl;
    [SerializeField] private Slider _xpBar;
    [SerializeField] private Image _xpFillBar;

    [SerializeField] private GameObject _dropdownableFrame;
    private bool _isShowing = false;

    private Coroutine _xpBarCoroutine;

    void Start()
    {

        Debug.Log("Menu Lenght : " + _menus.Length);
        for (int i = 0; i < _menus.Length; i++)
        {
            int index = i;
            Debug.Log("add listener " + index);
            _menus[index].ThisMenuButton.onClick.AddListener(() => ChangeMenu(index));
        }

        _currentMenu = _menus[0];
        EnableMenu(0);


        if (_xpBar != null)
        {
            _xpBar.maxValue = _xp_System.XpForUp;
            _xpBar.value = _xp_System.Xp;
        }

        UpdateXPBar();
        UpdateLvl();

        _dropdownableFrame.SetActive(false);
    }


    void ChangeMenu(int buttonIndex)
    {
        DisablePreviousMenu();
        EnableMenu(buttonIndex);
    }


    void EnableMenu(int buttonIndex)
    {
        _menus[buttonIndex].ThisMenuButton.interactable = false;
        _menus[buttonIndex].ThisMenuButton.image.enabled = true;
        _menus[buttonIndex].ThisMenuButton.image.sprite = _menus[buttonIndex].AssetButtonSelected;

        _menuWindows = _menus[buttonIndex].WindowType;

        _menus[buttonIndex].ThisMenu.SetActive(true);

        _currentMenu = _menus[buttonIndex];
    }


    void DisablePreviousMenu()
    {
        _currentMenu.ThisMenuButton.interactable = true;
        _currentMenu.ThisMenuButton.image.sprite = _invisibleSprite;

        _currentMenu.ThisMenu.SetActive(false);
    }


    public void OnDropdownButtonClicked()
    {
        _isShowing = !_isShowing;

        if (_isShowing == true)
        {
            _dropdownableFrame.SetActive(true);
        }

        if (_isShowing == false)
        {
            _dropdownableFrame.SetActive(false);
        }
    }


    public void UpdateXPBar()
    {
        if (_xp_System.Xp < _xp_System.XpForUp / 100)
        {
            _xpFillBar.enabled = false;
        }
        else
        {
            _xpFillBar.enabled = true;
        }

        if (_xpBar != null)
        {
            _xpBar.maxValue = _xp_System.XpForUp;
            AnimateXPBar(_xp_System.Xp);
        }
    }


    public void AnimateXPBar(float targetXP)
    {
        if (_xpBarCoroutine != null)
            StopCoroutine(_xpBarCoroutine);

        _xpBarCoroutine = StartCoroutine(AnimateXPBarRoutine(targetXP));
    }

    private IEnumerator AnimateXPBarRoutine(float targetXP)
    {
        float startXP = _xpBar.value;
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _xpBar.value = Mathf.Lerp(startXP, targetXP, elapsed / duration);
            yield return null;
        }

        _xpBar.value = targetXP;
    }

    public void UpdateLvl()
    {
        _lvl.text = _xp_System.Lvl.ToString();
    }
}



public enum MenuWindows
{
    HOME,
    PROGRESSION,
    ARMOURY,
    STORE,
    SETTINGS
}