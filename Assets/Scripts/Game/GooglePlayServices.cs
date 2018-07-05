using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class AuthenticateEventArgs : EventArgs
{
    public AuthenticateEventArgs(bool authenticated)
    {
        Authenticated = authenticated;
    }

    public bool Authenticated { get; set; }
}

public class SavedGameEventArgs : EventArgs
{
    public SavedGameEventArgs(GameData data)
    {
        Data = data;
    }

    public GameData Data { get; set; }
}

public class GooglePlayServices : IStats
{
    public GameManager GameManager => GameManager.Instance;

    public bool IsAuthenticated => Social.localUser.authenticated;

    public event EventHandler<AuthenticateEventArgs> Authenticate;
    public event EventHandler<SavedGameEventArgs> SavedGameRead;
    public event EventHandler<SavedGameEventArgs> SavedGameWritten;

    protected virtual void OnAuthenticate(AuthenticateEventArgs e)
    {
        Authenticate?.Invoke(this, e);
        Extensions.Log(GetType(), $"{nameof(Authenticate)} event raised.");
    }

    protected virtual void OnSavedGameRead(SavedGameEventArgs e)
    {
        SavedGameRead?.Invoke(this, e);
        Extensions.Log(GetType(), $"{nameof(SavedGameRead)} event raised.");
    }

    protected virtual void OnSavedGameWritten(SavedGameEventArgs e)
    {
        SavedGameWritten?.Invoke(this, e);
        Extensions.Log(GetType(), $"{nameof(SavedGameWritten)} event raised.");
    }

    public GooglePlayServices()
    {
        InitializeGooglePlayServices();
    }

    private static void InitializeGooglePlayServices()
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

    public void SignIn()
    {
        if (!IsAuthenticated)
        {
            Social.localUser.Authenticate((s) =>
            {
                string output = (s) ? "Signed in Google Play Games" : "Can't sign in Google Play Games";
                Extensions.Log(GetType(), output);
                OnAuthenticate(new AuthenticateEventArgs(s));
            });
        }
    }

    private void PerformAction(System.Action action)
    {
        if (IsAuthenticated)
        {
            action.Invoke();
        }
    }

    public void IncreaseZombiesKilled()
    {
        System.Action action = () =>
        {
            Action<bool> callback = (s) => { };
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_kill_100_zombies, 1, callback);
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_kill_50_zombies, 1, callback);
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_kill_25_zombies, 1, callback);
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_kill_5_zombies, 1, callback);
            PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_kill_first_zombie, 100f, callback);
        };
        PerformAction(action);
    }

    public void IncreaseHumansKilled()
    {
        System.Action action = () =>
        {
            //TODO: add humans killed
        };
        PerformAction(action);
    }

    public void IncreaseHumansSaved()
    {
        System.Action action = () =>
        {
            Action<bool> callback = (s) => { };
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_save_100_humans, 1, callback);
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_save_50_humans, 1, callback);
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_save_25_humans, 1, callback);
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_save_5_humans, 1, callback);
            PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_save_first_human, 100f, callback);
        };
        PerformAction(action);
    }

    public void InitializeStars()
    {
        System.Action action = () =>
        {

        };
        PerformAction(action);
    }

    public void InitializeScore()
    {
        System.Action action = () =>
        {
            Action<bool> callback = (s) => { };
            Social.ReportScore(GameManager.GameData.GameStats.ScoreTotal, GPGSIds.leaderboard_high_scores, callback);
        };
        PerformAction(action);
    }

    public void OnAchievementsClick()
    {
        System.Action action = () =>
        {
            Social.ShowAchievementsUI();
        };
        PerformAction(action);
    }

    public void OnLeaderboardClick()
    {
        System.Action action = () =>
        {
            Social.ShowLeaderboardUI();
        };
        PerformAction(action);
    }

    public void Write(GameData value)
    {
        System.Action action = () =>
        {
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.OpenWithAutomaticConflictResolution(Globals.GetFilename<GameData>(), DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseMostRecentlySaved,
            (status, game) =>
            {
                if (status == SavedGameRequestStatus.Success)
                {
                    byte[] binaryData = value.Serialize();
                    SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
                    builder = builder.WithUpdatedDescription($"Saved at {DateTime.Now}");
                    SavedGameMetadataUpdate updatedMetadata = builder.Build();
                    savedGameClient.CommitUpdate(game, updatedMetadata, binaryData, (s, g) =>
                    {
                        if (s == SavedGameRequestStatus.Success)
                        {
                            Extensions.Log(GetType(), $"Google Play writing completed. Status: {s}; Filename: {g.Filename}; Description: {g.Description};");
                        }
                        else
                        {
                            Extensions.Log(GetType(), $"Google Play writing failed. Status: {s};");
                        }
                    });
                }
                else
                {
                    Extensions.Log(GetType(), $"Couldn't open saved game. Status: {status};");
                }
            });
        };
        PerformAction(action);
    }

    public void Read()
    {
        System.Action action = () =>
        {
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.OpenWithAutomaticConflictResolution(Globals.GetFilename<GameData>(), DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseMostRecentlySaved,
            (status, game) =>
            {
                if (status == SavedGameRequestStatus.Success)
                {
                    savedGameClient.ReadBinaryData(game, (s, b) =>
                    {
                        if (s == SavedGameRequestStatus.Success)
                        {
                            Extensions.Log(GetType(), $"Google Play reading completed. Status: {s}; Filename: {game.Filename}; Description: {game.Description};");
                            var value = b.Deserialize<GameData>();
                            OnSavedGameRead(new SavedGameEventArgs(value));
                        }
                        else
                        {
                            Extensions.Log(GetType(), $"Google Play reading failed. Status: {s};");
                        }
                    });
                }
                else
                {
                    Extensions.Log(GetType(), $"Couldn't open saved game. Status: {status};");
                }
            });
        };
        PerformAction(action);
    }
}
