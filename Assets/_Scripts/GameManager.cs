using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _menuLeaveValidation;


    void Awake()
    {
        if (_menuLeaveValidation == null)
            return;
        _menuLeaveValidation.SetActive(false);
    }

    public void OpenLeaveGameMenu()
    {
        if (_menuLeaveValidation == null)
            return;
        _menuLeaveValidation.SetActive(true);
    }

    public void CloseLeaveGameMenu()
    {
        if (_menuLeaveValidation == null)
            return;
        _menuLeaveValidation.SetActive(false);
    }

    public void LeaveGame()
    {
        #if UNITY_STANDALONE
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
