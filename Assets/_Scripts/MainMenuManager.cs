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
        /*
        _homeButton.onClick.AddListener(() => OnButtonClick(MenuWindows.HOME));
        _progressionButton.onClick.AddListener(() => OnButtonClick(MenuWindows.PROGRESSION));
        _armoryButton.onClick.AddListener(() => OnButtonClick(MenuWindows.ARMOURY));
        _storeButton.onClick.AddListener(() => OnButtonClick(MenuWindows.STORE));
        _settingsButton.onClick.AddListener(() => OnButtonClick(MenuWindows.SETTINGS));
        */


        Debug.Log("Menu Lenght : " + _menus.Length);
        for (int i = 0; i < _menus.Length; i++)
        {
            int index = i;
            Debug.Log("add listener " + index);
            _menus[index].ThisMenuButton.onClick.AddListener(() => ChangeMenu(index));
        }

        _currentMenu = _menus[0];

        //DisableButtonCurrentMenu(_homeButton, _homeButtonAsset);

        if (_xpBar != null)
        {
            _xpBar.maxValue = _xp_System._xpForUp;
            _xpBar.value = _xp_System._xp;
        }

        UpdateXPBar();
        UpdateLvl();

        _dropdownableFrame.SetActive(false);
    }

    void Update()
    {

    }


    void OnButtonClick(int buttonIndex)
    {
        //DisableButtonCurrentMenu(_menus[buttonIndex].ThisMenuButton, _menus[buttonIndex].AssetButtonSelected);

        /*
        switch (menu)
        {
            case MenuWindows.HOME:
                break;
            case MenuWindows.PROGRESSION:
                DisableButtonCurrentMenu(_progressionButton, _homeButtonAsset);
                break;
            case MenuWindows.ARMOURY:
                DisableButtonCurrentMenu(_armoryButton, _homeButtonAsset);
                break;
            case MenuWindows.STORE:
                DisableButtonCurrentMenu(_storeButton, _homeButtonAsset);
                break;
            case MenuWindows.SETTINGS:
                DisableButtonCurrentMenu(_settingsButton, _homeButtonAsset);
                break;
        }
        */
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

    void DisableButtonCurrentMenu(Button button, Sprite buttonAsset)
    {
        //EnableButton();

        button.interactable = false;
        button.image.enabled = true;
        button.image.sprite = buttonAsset;
    }


    /*
    void EnableButton()
    {
        _homeButton.interactable = true;
        _progressionButton.interactable = true;
        _armoryButton.interactable = true;
        _storeButton.interactable = true;
        _settingsButton.interactable = true;

        _homeButton.image.enabled = false;
        _progressionButton.image.enabled = false;
        _armoryButton.image.enabled = false;
        _storeButton.image.enabled = false;
        _settingsButton.image.enabled = false;

        _homeButton.image.sprite = null;
        _progressionButton.image.sprite = null;
        _armoryButton.image.sprite = null;
        _storeButton.image.sprite = null;
        _settingsButton.image.sprite = null;
    }
    */


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
        if (_xp_System._xp < _xp_System._xpForUp / 100)
        {
            _xpFillBar.enabled = false;
        }
        else
        {
            _xpFillBar.enabled = true;
        }

        if (_xpBar != null)
        {
            _xpBar.maxValue = _xp_System._xpForUp;
            AnimateXPBar(_xp_System._xp);
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
        _lvl.text = _xp_System._lvl.ToString();
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