using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDie : BaseUI
{
    private TextMeshProUGUI retryText => GetUI<TextMeshProUGUI>("RetryText");
    private TextMeshProUGUI lobbyText => GetUI<TextMeshProUGUI>("LobbyText");

    void Start()
    {
        GetEvent("RetryText").Enter += data => retryText.color = Color.red;
        GetEvent("LobbyText").Enter += data => lobbyText.color = Color.red;

        GetEvent("RetryText").Exit += data => retryText.color = Color.white;
        GetEvent("LobbyText").Exit += data => lobbyText.color = Color.white;

        GetEvent("RetryText").Click += data => SceneManager.LoadSceneAsync(2);
        GetEvent("LobbyText").Click += data => SceneManager.LoadSceneAsync(0);
    }

}
