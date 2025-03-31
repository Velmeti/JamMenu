using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Performs actions on first launch
/// </summary>

public class FirstTime : MonoBehaviour
{
    public GameObject firstTimeCanvas;

    void Awake()
    {
        CheckFirstTime();
    }


    private void Update()
    {
        ResetFirstTime();
    }


    private void CheckFirstTime()
    {
        if (!PlayerPrefs.HasKey("FirstTime"))
        {
            firstTimeCanvas.SetActive(true);
            PlayerPrefs.SetInt("FirstTime", 1);
            PlayerPrefs.Save();
        }
        else
        {
            firstTimeCanvas.SetActive(false);
        }
    }



    // Debug function to show again first time menu
    private void ResetFirstTime()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0) && PlayerPrefs.HasKey("FirstTime"))
        {
            PlayerPrefs.DeleteKey("FirstTime");
            firstTimeCanvas.SetActive(true);
        }
    }
}
