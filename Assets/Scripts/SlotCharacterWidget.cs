using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotCharacterWidget : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    [SerializeField]
    private GameObject _emptySlot;

    [SerializeField]
    private GameObject _infoCharacterSlot;

    [SerializeField]
    private TMP_Text _nameLabel;

    [SerializeField]
    private TMP_Text _healthLabel;

    [SerializeField]
    private TMP_Text _damageLabel;

    [SerializeField]
    private TMP_Text _xpLabel;

    public Button slotButton => _button;

    public void ShowInfoCharacterSlot(string name, string damage, string health, string xp) 
    {
        _nameLabel.text = name;
        _damageLabel.text = $"Damage: {damage}";
        _healthLabel.text = $"Health: {health}";
        _xpLabel.text = $"XP: {xp}";

        _infoCharacterSlot.SetActive(true);
        _emptySlot.SetActive(false);
    }

    public void ShowEmptySlot() 
    {
        _infoCharacterSlot.SetActive(false);
        _emptySlot.SetActive(true);
    }
}
