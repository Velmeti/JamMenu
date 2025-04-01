using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Performs actions about the first launch
/// </summary>

public class FirstTimeManager : MonoBehaviour
{
    public GameObject FirstTimeCanvas;

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
            FirstTimeCanvas.SetActive(true);
            PlayerPrefs.SetInt("FirstTime", 1);
            PlayerPrefs.Save();
        }
        else
        {
            FirstTimeCanvas.SetActive(false);
        }
    }



    // Debug function to show again first time menu
    private void ResetFirstTime()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0) && PlayerPrefs.HasKey("FirstTime"))
        {
            PlayerPrefs.DeleteKey("FirstTime");
            FirstTimeCanvas.SetActive(true);
        }
    }
}
