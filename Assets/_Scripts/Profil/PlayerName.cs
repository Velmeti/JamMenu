using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{
    [SerializeField] private SavePlayer _savePlayer;

    [SerializeField] private TMP_InputField _playerNameInput;
    [SerializeField] private TextMeshProUGUI _playerNameText;


    void Start()
    {
        string savedName = _savePlayer.GetPlayerName();
        _playerNameText.text = savedName;
        _playerNameInput.text = savedName;

        _savePlayer.SaveButton.onClick.AddListener(SavePlayerName);
    }

    void SavePlayerName()
    {
        string playerName = _playerNameInput.text;
        _savePlayer.SavePlayerName(playerName);
        _playerNameText.text = playerName;
    }
}
