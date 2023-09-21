using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharactersManager : MonoBehaviour
{
    private string _characterName;

    [SerializeField]
    private GameObject _characterMenu;

    [SerializeField]
    private GameObject _newCharacterCreatePanel;

    [SerializeField]
    private Button _createCharacterButton;

    [SerializeField]
    private Button _characterMenuButton;

    [SerializeField]
    private TMP_InputField _inputField;

    [SerializeField]
    private List<SlotCharacterWidget> _slots;


    private void Start()
    {
        _characterMenuButton.onClick.AddListener(GetCharacterMenu);

        GetCharacters();

        foreach (var slot in _slots)
            slot.slotButton.onClick.AddListener(OpenCreateNewCharacter);

        _inputField.onValueChanged.AddListener(OnNameChanged);
        _createCharacterButton.onClick.AddListener(CreateCharacter);
    }

    private void GetCharacterMenu()
    {
        _characterMenu.SetActive(true);
    }

    private void CreateCharacter()
    {
        PlayFabClientAPI.GrantCharacterToUser(new GrantCharacterToUserRequest
        {
            CharacterName = _characterName,
            ItemId = "accessToken",
        },
        result =>
        {
            UpdateCharacterStatistics(result.CharacterId);
        }, OnError);
    }

    private void UpdateCharacterStatistics(string characterId)
    {
        PlayFabClientAPI.UpdateCharacterStatistics(new UpdateCharacterStatisticsRequest
        {
            CharacterId = characterId,
            CharacterStatistics = new Dictionary<string, int>
            {
                { "Damage", 3 },
                { "Health", 5 },
                { "XP", 0 }
            }
        },
        result =>
        {
            Debug.Log("Character completed!!!");
            CloseCreateNewCharacter();
            GetCharacters();
        }, OnError);
    }

    private void OnNameChanged(string changedName)
    {
        _characterName = changedName;
    }

    private void OpenCreateNewCharacter()
    {
        _newCharacterCreatePanel.SetActive(true);
    }

    private void CloseCreateNewCharacter()
    {
        _newCharacterCreatePanel.SetActive(false);
    }

    private void GetCharacters()
    {
        PlayFabClientAPI.GetAllUsersCharacters(new ListUsersCharactersRequest(),
        result =>
        {
            Debug.Log($"Character's count: {result.Characters.Count}");
            ShowCharactersInSlot(result.Characters);
        }, OnError);
    }

    private void ShowCharactersInSlot(List<CharacterResult> characters)
    {
        if (characters.Count == 0)
        {
            foreach (var slot in _slots)
            {
                slot.ShowEmptySlot();
            }
        }
        else if (characters.Count > 0 && characters.Count <= _slots.Count)
        {
            PlayFabClientAPI.GetCharacterStatistics(new GetCharacterStatisticsRequest
            {
                CharacterId = characters.First().CharacterId
            },
            result =>
            {
                var damage = result.CharacterStatistics["Damage"].ToString();
                var health = result.CharacterStatistics["Health"].ToString();
                var xp = result.CharacterStatistics["XP"].ToString();

                _slots.First().ShowInfoCharacterSlot(characters.First().CharacterName, damage, health, xp);
            }, OnError);
        }
        else
        {
            Debug.LogError("Add slots for characters.");
        }
    }

    private void OnError(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        Debug.LogError(errorMessage);
    }
}
