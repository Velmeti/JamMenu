using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBorderRewardSystem : MonoBehaviour
{
    [SerializeField] private Image borderImage;
    [SerializeField] private BorderReward[] borders;
    
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
