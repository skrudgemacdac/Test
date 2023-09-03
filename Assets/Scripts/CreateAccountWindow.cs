using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class CreateAccountWindow : AccountWindowDataBase
{
    [SerializeField]
    private InputField _mailField;

    [SerializeField]
    private Button _createAccountButton;

    private string _mail;

    protected override void SubscriptionUIElements() 
    {
        base.SubscriptionUIElements();

        _mailField.onValueChanged.AddListener(UpdateMail);
        _createAccountButton.onClick.AddListener(CreateAccount);
    }

    private void CreateAccount()
    {
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest
        {
            Username = _username,
            Email = _mail,
            Password = _password,
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

    private void UpdateMail(string mail)
    {
        _mail = mail;
    }
}
