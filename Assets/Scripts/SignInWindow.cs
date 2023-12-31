using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignInWindow : AccountWindowDataBase
{
    private readonly Dictionary<string, CatalogItem> _catalog = new Dictionary<string, CatalogItem>();

    [SerializeField]
    private Button _signInButton;

    [SerializeField]
    private Button _back;

    public void SignIn()
    {
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest(), ItemsLoadingSuccess, ItemsLoadingFailure);
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

    protected void ItemsLoadingFailure(PlayFabError error)
    {
        var _errorMessage = error.GenerateErrorReport();
        Debug.LogError($"CatalogLoadingFailure: {_errorMessage}");
    }

    protected void ItemsLoadingSuccess(GetCatalogItemsResult result)
    {
        HandleCatalog(result.Catalog);
        Debug.Log($"Catalog was loaded successfully!");
    }

    private void HandleCatalog(List<CatalogItem> catalog)
    {
        foreach (var item in catalog)
        {
            _catalog.Add(item.ItemId, item);
            Debug.Log($"Catalog item {item.ItemId} was added successfully!");
        }
    }
}
