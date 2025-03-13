using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP_System : MonoBehaviour
{
    [SerializeField] private HomeManager _homeManager;

    public int _lvl = 1;
    public float _xp = 0;
    [SerializeField] private XP_Curve[] _xpCurveStages;


    [SerializeField] private float _lvl1_xpForUp = 100;
    public float _xpForUp = 0;
    private float _currentRatio;



    void Start()
    {
        _xpForUp = _lvl1_xpForUp;
        _currentRatio = _xpCurveStages[0]._xpForUp_ratio;
        InitXp();
    }

    // Update is called once per frame
    void Update()
    {
        LevelUp();
    }


    public void EarnXP(float xp_earned)
    {
        _xp += xp_earned;
        _homeManager.UpdateXPBar();
    }


    void LevelUp()
    {
        if (_xp >= _xpForUp)
        {
            _lvl++;
            _xp = _xp - _xpForUp;
            CalculateRatio(_lvl);
            CalculateNextLvlXP();
            _homeManager.UpdateXPBar();
            _homeManager.UpdateLvl();
        }
    }


    void CalculateRatio(int lvl)
    {
        for (int i = 0; i < _xpCurveStages.Length; i++)
        {
            if (lvl >= _xpCurveStages[i]._lvlStage)
            {
                _currentRatio = _xpCurveStages[i]._xpForUp_ratio;
            }
        }
    }

    void CalculateNextLvlXP()
    {
        _xpForUp = _xpForUp * _currentRatio;
    }

    void InitXp()
    {
        for (int i = 1; i < _lvl; i++)
        {
            Debug.Log(i);
            CalculateNextLvlXP();
        }
    }
}


[Serializable]
public class XP_Curve
{
    public int _lvlStage = 0;
    public float _xpForUp_ratio = 1;
}