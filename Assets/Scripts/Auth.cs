using GooglePlayGames.BasicApi;
using GooglePlayGames;
using UnityEngine;
using System;

public class Auth : MonoBehaviour
{

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
            Init();
    }
    private void Init()
    {
        Debug.LogError("\nIntento de log in\n");
        //Logger.SendLog("\nLogged in successfully\n");
        PlayGamesPlatform.Instance.Authenticate(success =>
        {
            
            Debug.LogError("\n"+success+"\n");
            if (success == SignInStatus.Success)
            {
                Debug.LogError("\nLogged in successfully\n");
                //Logger.SendLog("\nLogged in successfully\n");
            }
            else
            {
                Debug.LogError("\nLogin Failed\n");
                //Logger.SendLog("\nLogin Failed\n");
            }
        });

    }

    static public void ShowAchievements()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (PlayGamesPlatform.Instance.IsAuthenticated())
            {
                PlayGamesPlatform.Instance.ShowAchievementsUI();
                //Logger.SendLog("\nMostrando logros\n");
            }
        }
    }


    static public void UnlockAchievement(string a)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (PlayGamesPlatform.Instance.IsAuthenticated())
            {
                PlayGamesPlatform.Instance.ReportProgress(a, 100f, success => { });
                //Logger.SendLog("\nLogro desbloqueado!\n");
            }
        }
    }

}