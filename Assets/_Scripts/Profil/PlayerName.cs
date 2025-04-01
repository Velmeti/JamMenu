using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Change and load player name UI
/// </summary>
public class PlayerName : MonoBehaviour
{
    [SerializeField] private SavePlayer _savePlayer;
    [SerializeField] private TMP_InputField[] _playerNameInput;
    [SerializeField] private TextMeshProUGUI _playerNameText;

    void Start()
    {
        string savedName = _savePlayer.GetPlayerName();
        _playerNameText.text = savedName;

        for (int i = 0; i < _playerNameInput.Length; i++)
        {
            _playerNameInput[i].text = savedName;
        }

        for (int y = 0; y < _savePlayer.SaveButtons.Length; y++)
        {
            int index = y;
            _savePlayer.SaveButtons[y].onClick.AddListener(() => SavePlayerName(index));
        }
    }

    void SavePlayerName(int index)
    {
        if (index < 0 || index >= _playerNameInput.Length) return;

        string playerName = _playerNameInput[index].text;
        _savePlayer.SavePlayerName(playerName);
        _playerNameText.text = playerName;

        for (int i = 0; i < _playerNameInput.Length; i++)
        {
            _playerNameInput[i].text = playerName;
        }
    }

}
