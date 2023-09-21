using PlayFab;
using PlayFab.ClientModels;
using System;
using TMPro;
using UnityEngine;

public class PlayFabAccountManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _titleLabel;

    [SerializeField]
    private TMP_Text _createdTime;

    [SerializeField]
    private TMP_Text _userName;

    private void Start()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnGetAccount, OnError);
    }

    private void OnError(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        Debug.LogError(errorMessage);
    }

    private void OnGetAccount(GetAccountInfoResult result)
    {
        _titleLabel.text = $"Playfab ID: {result.AccountInfo.PlayFabId}";
        _createdTime.text = $"Created: {result.AccountInfo.Created}";
        _userName.text = $"Username: {result.AccountInfo.Username}";
    }
}
