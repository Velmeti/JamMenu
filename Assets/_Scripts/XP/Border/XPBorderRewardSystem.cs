using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script relative to XP reward (borders)
/// </summary>
public class XPBorderRewardSystem : MonoBehaviour
{
    [SerializeField] private SavePlayer _savePlayer;
    [SerializeField] private Image borderImage;
    [SerializeField] private BorderReward[] borders;

    private void Start()
    {
        ChangeBorder(_savePlayer.GetPlayerLvl());
    }

    public void ChangeBorder(int lvl)
    {
        for (int i = 0; i < borders.Length; i++)
        {
            if (borders[i].SpriteBorder != null && lvl == borders[i].LevelToUnloack)
            {
                borderImage.sprite = borders[i].SpriteBorder;
            }
        }
    }
}
