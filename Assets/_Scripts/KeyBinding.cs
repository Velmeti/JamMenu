using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.EventSystems;


public class KeyBinding : MonoBehaviour
{
    private KeyCode _nextKeyPressed;
    private bool _waitingForKey = false;
    private TextMeshProUGUI _keyText;

    private bool _isBidingButtonPressed = false;


    void Update()
    {
        SetBind();
    }


    public void BindingButtonPressed()
    {
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        _keyText = clickedButton.transform.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _isBidingButtonPressed = true;
        _keyText.text = "Press any key...";
    }


    void SetBind()
    {
        if (!_isBidingButtonPressed)
            return;

        if (Input.anyKeyDown)
        {
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    _keyText.text = key.ToString();
                    _isBidingButtonPressed = false;
                    break;
                }
            }
        }

    }
}