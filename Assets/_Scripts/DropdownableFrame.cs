using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropdownableFrame : MonoBehaviour
{

    [SerializeField] private GameObject _dropdownableFrame;
    private bool _isShowing = false;

    void Start()
    {
        _dropdownableFrame.SetActive(false);
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
}
