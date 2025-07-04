using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDie : BaseUI
{
    private TextMeshProUGUI retryText => GetUI<TextMeshProUGUI>("RetryText");
    private TextMeshProUGUI lobbyText => GetUI<TextMeshProUGUI>("LobbyText");

    private Coroutine routine;

    void Start()
    {
        GetEvent("RetryText").Enter += data => retryText.color = Color.red;
        GetEvent("LobbyText").Enter += data => lobbyText.color = Color.red;

        GetEvent("RetryText").Exit += data => retryText.color = Color.white;
        GetEvent("LobbyText").Exit += data => lobbyText.color = Color.white;

        //이부분에 초기화 작업 해줘야함
        GetEvent("RetryText").Click += data =>
        {
            if (Manager.Game.IsDead)
                Manager.Game.IsDead = false;

            if(routine == null)
                routine = StartCoroutine(RetryRoutine());
        };
        GetEvent("LobbyText").Click += data =>
        {
            SceneManager.LoadSceneAsync(0);
            Manager.UI.ClosePopUp();
        };
    }

    private IEnumerator RetryRoutine()
    {
        Manager.UI.Fade.PlayFade(1, 1.5f);
        yield return Utile.GetRealTimeDelay(1f);

        Manager.UI.ClosePopUp();
        Manager.Game.OnRetry?.Invoke();
        Manager.Data.ResetPlayerStat();
        SceneManager.LoadSceneAsync(2);

        Manager.Game.TimeRestart();
        routine = null;
    }
}
