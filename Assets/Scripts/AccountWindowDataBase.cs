using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AccountWindowDataBase : MonoBehaviour
{
    [SerializeField]
    private InputField _usernameField;

    [SerializeField]
    private InputField _passwordField;

    protected string _username;
    protected string _password;

    protected float delay = 1.2f;

    [SerializeField]
    protected RawImage _loading;

    [SerializeField]
    protected RawImage _loadingFailure;

    private void Start()
    {
        SubscriptionUIElements();
    }

    protected virtual void SubscriptionUIElements()
    {
        _usernameField.onValueChanged.AddListener(UpdateUsername);
        _passwordField.onValueChanged.AddListener(UpdatePassword);
    }

    private void UpdatePassword(string password)
    {
        _password = password;
    }

    private void UpdateUsername(string username)
    {
        _username = username;
    }

    protected void EnterInGameScene() 
    {
        SceneManager.LoadScene(1);
    }

    protected void Loading()
    {
        if (_loading != null)
        {
            _loading.enabled = false;
        }
    }

    protected void LoadingFailure()
    {
        if (_loadingFailure != null)
        {
            _loadingFailure.enabled = false;
        }
    }
}
