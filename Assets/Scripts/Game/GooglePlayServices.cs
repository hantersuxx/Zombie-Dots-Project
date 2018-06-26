using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using UnityEngine;

public class GooglePlayServices : IStats
{
    public bool Saving { get; private set; } = false;

    public GooglePlayServices()
    {
        InitializePlayGames();
    }

    public void SignIn()
    {
        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.Authenticate((s) =>
            {
                string output = (s) ? "Signed in Google Play Games" : "Can't sign in Google Play Games";
                Extensions.Log(GetType(), output);
            }, false);
        }
        else
        {
            SignOut();
        }
    }

    public void SignOut()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.SignOut();
            Extensions.Log(GetType(), "Signed out from Google Play Games");
        }
    }

    private void InitializePlayGames()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        // enables saving game progress.
        .EnableSavedGames()
        // registers a callback to handle game invitations received while the game is not running.
        //.WithInvitationDelegate(< callback method >)
        // registers a callback for turn based match notifications received while the
        // game is not running.
        //.WithMatchDelegate(( callback method >)
        // requests the email address of the player be available.
        // Will bring up a prompt for consent.
        //.RequestEmail()
        // requests a server auth code be generated so it can be passed to an
        //  associated back end server application and exchanged for an OAuth token.
        //.RequestServerAuthCode(false)
        // requests an ID token be generated.  This OAuth token can be used to
        //  identify the player to other services such as Firebase.
        //.RequestIdToken()
        .Build();

        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
    }

    public void IncreaseZombiesKilled()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            Action<bool> callback = (s) => { };
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_kill_100_zombies, 1, callback);
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_kill_50_zombies, 1, callback);
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_kill_25_zombies, 1, callback);
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_kill_5_zombies, 1, callback);
            PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_kill_first_zombie, 100f, callback);
        }
    }

    public void IncreaseHumansKilled()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {

        }
    }

    public void IncreaseHumansSaved()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            Action<bool> callback = (s) => { };
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_save_100_humans, 1, callback);
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_save_50_humans, 1, callback);
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_save_25_humans, 1, callback);
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_save_5_humans, 1, callback);
            PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_save_first_human, 100f, callback);
        }
    }

    public void InitializeStars()
    {

    }

    public void InitializeScore()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            Action<bool> callback = (s) => { };
            PlayGamesPlatform.Instance.ReportScore(GameManager.Instance.GameStats.ScoreTotal, GPGSIds.leaderboard_high_scores, callback);
        }
    }

    public void OnAchievementsClick()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowAchievementsUI((s) =>
            {
                Extensions.Log(GetType(), $"Show achievements status: {s}");
            });
        }
    }

    public void OnLeaderboardClick()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            Extensions.Log(GetType(), $"Show leaderboard");
            Social.ShowLeaderboardUI();
            //PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_high_scores, LeaderboardTimeSpan.AllTime, (s) =>
            //  {
            //      Extensions.Log(GetType(), $"Show leaderboard status: {s}");
            //  });
        }
    }
}
