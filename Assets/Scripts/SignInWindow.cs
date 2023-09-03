using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignInWindow : AccountWindowDataBase
{
    [SerializeField]
    private Button _signInButton;

    [SerializeField]
    private Button _back;

    public void SignIn()
    {
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest
        {
            Username = _username,
            Password = _password
        }, result =>
        {
            _loading.enabled = true;
            Invoke("Loading", delay);
            Debug.Log($"Success: {_username}");
            Invoke("EnterInGameScene", delay);
        }, error =>
        {
            _loadingFailure.enabled = true;
            Invoke("LoadingFailure", delay);
            Debug.LogError($"Fail: {error.ErrorMessage}");
        });
    }

    protected override void SubscriptionUIElements()
    {
        base.SubscriptionUIElements();
        _signInButton.onClick.AddListener(SignIn);
    }
}
