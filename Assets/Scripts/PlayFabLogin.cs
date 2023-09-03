using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayFabLogin : MonoBehaviour
{
    private const string AuthGuidKey = "auth_quid_key";

    [SerializeField]
    private RawImage _loading;

    private float delay = 1.5f;

    [SerializeField]
    private RawImage _loadingFailure;

    public void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            PlayFabSettings.staticSettings.TitleId = " A823B";
        }

        var needCreation = PlayerPrefs.HasKey(AuthGuidKey);
        var id = PlayerPrefs.GetString(AuthGuidKey, Guid.NewGuid().ToString());

        var request = new LoginWithCustomIDRequest
        {
            CustomId = id,
            CreateAccount = !needCreation
        };

        PlayFabClientAPI.LoginWithCustomID(request,
        result =>
        {
            _loading.enabled = true;
            Invoke("Loading", delay);
            PlayerPrefs.SetString(AuthGuidKey, id);
            OnLoginSuccess(result);
        }, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made successful API call!");
    }

    private void OnLoginFailure(PlayFabError error)
    {
        _loadingFailure.enabled = true;
        Invoke("LoadingFailure", 2f);
        var errorMessage = error.GenerateErrorReport();
        Debug.LogError($"Something went wrong: {errorMessage}");
    }

    private void Loading() 
    {
        if (_loading != null) 
        {
            _loading.enabled = false;
        }
    }

    private void LoadingFailure() 
    {
        if (_loadingFailure != null) 
        {
            _loadingFailure.enabled = false;
        }
    }
}