using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundEffect : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;
    [SerializeField] AudioSource _SoundButton;


    void Start()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].onClick.AddListener(OnButtonClick);
        }
    }

    
    void OnButtonClick()
    {
        if (_SoundButton != null && _SoundButton.clip != null)
        {
            _SoundButton.Play();
        }
    }
}
