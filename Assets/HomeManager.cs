using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    [SerializeField] private XP_System _xp_System;

    private int _currentHomeWindow = 0;

    [SerializeField] private TextMeshProUGUI _lvl;
    [SerializeField] private Slider _xpBar;
    [SerializeField] private Image _xpFillBar;

    [SerializeField] private GameObject _dropdownableFrame;
    private bool _isShowing = false;

    private Coroutine _xpBarCoroutine;

    void Start()
    {
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



[Serializable]
public class HomeWindow : MonoBehaviour
{

    public HomeWindowList WindowsList;
    public enum HomeWindowList
    {
        HOME = 0,
        PROGRESSION = 1,
        ARMOURY = 2,
        STORE = 3,
        SETTINGS = 4,
    }
}