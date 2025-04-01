using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RooterMenu : MonoBehaviour
{

    [SerializeField] private MenuInfos[] _menus;
    private MenuInfos _currentMenu;
    [SerializeField] private Sprite _invisibleSprite;

    //[SerializeField] private MenuWindows _menuWindows;
    //private int _currentHomeWindow = 0;





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
        DisableAllMenu();
        EnableMenu(0);




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

        //_menuWindows = _menus[buttonIndex].WindowType;

        _menus[buttonIndex].ThisMenu.SetActive(true);

        _currentMenu = _menus[buttonIndex];
    }


    void DisablePreviousMenu()
    {
        _currentMenu.ThisMenuButton.interactable = true;
        _currentMenu.ThisMenuButton.image.sprite = _invisibleSprite;

        _currentMenu.ThisMenu.SetActive(false);
    }


    void DisableAllMenu()
    {
        for (int i = 0; i < _menus.Length; i++)
        {
            _menus[i].ThisMenu.SetActive(false);
        }
    }
}


/*
public enum MenuWindows
{
    HOME,
    PROGRESSION,
    ARMOURY,
    STORE,
    SETTINGS
}*/