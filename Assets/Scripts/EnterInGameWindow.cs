using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterInGameWindow : MonoBehaviour
{
    [SerializeField]
    private Button _sighInButton;

    [SerializeField]
    private Button _createAccountButton;

    [SerializeField]
    private Canvas _enterInGameCanvas;

    [SerializeField]
    private Canvas _sighInCanvas;

    [SerializeField]
    private Canvas _createAccountCanvas;

    [SerializeField]
    private Button _sighInBackButton;

    [SerializeField]
    private Button _createAccountBackButton;

    void Start()
    {
        _sighInButton.onClick.AddListener(OpenSignWindow);
        _sighInBackButton.onClick.AddListener(SighInBack);
        _createAccountButton.onClick.AddListener(OpenCreateAccountWindow);
        _createAccountBackButton.onClick.AddListener(CreateAccountBack);
    }

    private void CreateAccountBack()
    {
        _createAccountCanvas.enabled = false;
        _enterInGameCanvas.enabled = true;
    }

    private void SighInBack()
    {
        _sighInCanvas.enabled = false;
        _enterInGameCanvas.enabled = true;
    }

    private void OpenCreateAccountWindow()
    {
        _createAccountCanvas.enabled = true;
        _enterInGameCanvas.enabled = false;
    }

    private void OpenSignWindow()
    {
        _sighInCanvas.enabled = true;
        _enterInGameCanvas.enabled = false;
    }
}
