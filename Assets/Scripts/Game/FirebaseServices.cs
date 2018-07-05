using Firebase;
using Firebase.Messaging;
using UnityEngine;

public class FirebaseServices
{
    public FirebaseServices()
    {
        InitializeFirebase();
    }

    private void InitializeFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                // Set a flag here indiciating that Firebase is ready to use by your
                // application.
                FirebaseMessaging.TokenReceived += OnTokenReceived;
                FirebaseMessaging.MessageReceived += OnMessageReceived;
                Extensions.Log(GetType(), "Firebase ready to use");
            }
            else
            {
                Extensions.Log(GetType(), $"Could not resolve all Firebase dependencies: { dependencyStatus}");
            }
        });
    }

    public void OnTokenReceived(object sender, TokenReceivedEventArgs token)
    {
        Extensions.Log(GetType(), $"Received Registration Token: {token.Token}");
    }

    public void OnMessageReceived(object sender, MessageReceivedEventArgs e)
    {
        Extensions.Log(GetType(), $"Received a new message from: {e.Message.From}");
    }
}