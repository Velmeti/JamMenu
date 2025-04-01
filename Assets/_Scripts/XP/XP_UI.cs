using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XP_UI : MonoBehaviour
{
    [SerializeField] private XP_System _xp_System;

    public TextMeshProUGUI XpText;
    [SerializeField] private TextMeshProUGUI _txt_lvl;
    [SerializeField] private Slider _xpBar;
    [SerializeField] private Image _xpFillBar;

    private Coroutine _xpBarCoroutine;


    public void InitXP_UI()
    {
        if (_xpBar != null)
        {
            _xpBar.maxValue = _xp_System.XpForUp;
            _xpBar.value = _xp_System.Xp;
        }

        UpdateXPBar();
        UpdateLvl();
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
        _txt_lvl.text = _xp_System.Lvl.ToString();
    }
}
