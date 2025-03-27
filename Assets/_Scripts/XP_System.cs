using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP_System : MonoBehaviour
{
    [SerializeField] private MainMenuManager _mainMenuManager;
    [SerializeField] private SavePlayer _savePlayer;

    public int Lvl = 1;
    public float Xp = 0;
    [SerializeField] private XP_Curve[] _xpCurveStages;


    public float Lvl1_xpForUp = 100;
    [HideInInspector] public float XpForUp;
    public float _currentRatio;



    void Start()
    {
        LoadXpDatas();
    }


    void Update()
    {
        LevelUp();
    }



    private void LoadXpDatas()
    {
        int lvl = _savePlayer.GetPlayerLvl();
        float xp = _savePlayer.GetPlayerXp();
        float xpForUp = _savePlayer.GetPlayerXpForUp();

        Lvl = lvl;

        if (lvl <= 1)
        {
            XpForUp = Lvl1_xpForUp;
        }
        else
        {
            XpForUp = xpForUp;
        }

        Xp = xp;

        CalculateRatio(lvl);

    }




    public void EarnXP(float xp_earned)
    {
        Xp += xp_earned;
        _mainMenuManager.UpdateXPBar();

        _savePlayer.SavePlayerXPSystem(Lvl, Xp, XpForUp); 
    }


    void LevelUp()
    {
        if (Xp >= XpForUp)
        {
            Lvl++;
            Xp = Xp - XpForUp;
            CalculateRatio(Lvl);
            CalculateNextLvlXP();
            _mainMenuManager.UpdateXPBar();
            _mainMenuManager.UpdateLvl();
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
        XpForUp = XpForUp * _currentRatio;
    }
}


[Serializable]
public class XP_Curve
{
    public int _lvlStage = 0;
    public float _xpForUp_ratio = 1;
}