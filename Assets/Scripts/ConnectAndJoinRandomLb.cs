using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConnectAndJoinRandomLb : MonoBehaviour, IConnectionCallbacks, IMatchmakingCallbacks, ILobbyCallbacks
{
    private LoadBalancingClient _lbc;

    [SerializeField]
    private GameObject _defaultLobbyPanel;

    [SerializeField]
    private GameObject _roomPanel;

    [SerializeField]
    private Button _statusButton;

    [SerializeField]
    private Button _roomButton;

    [SerializeField]
    private Button _defaultLobbyButton;

    [SerializeField]
    private TMP_Text _roomText;

    [SerializeField]
    private TMP_Text _roomNameText;

    [SerializeField]
    private TMP_Text _pcText;

    [SerializeField]
    private ServerSettings _serverSettings;

    [SerializeField]
    private TMP_Text _stateUIText;

    private string status;

    public const string MAP_PROP_KEY = "map";
    public const string GAME_MODE_PROP_KEY = "gm";

    private TypedLobby defaultLobby = new TypedLobby("defaultLobby", LobbyType.Default);

    [SerializeField]
    private int _maxPlayersCount;

    [SerializeField]
    private string _roomName;

    [SerializeField]
    private TMP_Text _roomStatusText;

    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();

    private void Start()
    {
        _lbc = new LoadBalancingClient();
        _lbc.AddCallbackTarget(this);
        _lbc.ConnectUsingSettings(_serverSettings.AppSettings);
        SubscriptionUIElements();
    }

    private void OnDestroy()
    {
        _lbc.RemoveCallbackTarget(this);
    }

    private void Update()
    {
        if (_lbc == null)
            return;
        _lbc.Service();

        var state = _lbc.State.ToString();
        _stateUIText.text = state;
    }

    protected void SubscriptionUIElements() 
    {
        _defaultLobbyButton.onClick.AddListener(JoinLobby);
        _roomButton.onClick.AddListener(OnJoinedRoom);
        _statusButton.onClick.AddListener(ChangeRoomStatus);
    }

    private void JoinLobby() 
    {
        _lbc.OpJoinLobby(defaultLobby);
        _defaultLobbyButton.gameObject.SetActive(false);
        _stateUIText.enabled = false;
        _defaultLobbyPanel.SetActive(true);
        Debug.Log("JoinedToDefaultLobby");
    }

    private void UpdateCachedRoomList(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            RoomInfo info = roomList[i];
            if (info.RemovedFromList)
            {
                cachedRoomList.Remove(info.Name);
            }
            else
            {
                cachedRoomList[info.Name] = info;
            }
        }
    }

    void ILobbyCallbacks.OnJoinedLobby()
    {
        cachedRoomList.Clear();
    }
    void ILobbyCallbacks.OnLeftLobby()
    {
        cachedRoomList.Clear();
    }

    void ILobbyCallbacks.OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateCachedRoomList(roomList);
    }

    void IConnectionCallbacks.OnDisconnected(DisconnectCause cause)
    {
        cachedRoomList.Clear();
    }

    public void OnConnected()
    {
        
    }

    public void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
    }

    public void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
    }

    public void OnCreateRoomFailed(short returnCode, string message)
    {
        
    }

    public void OnCustomAuthenticationFailed(string debugMessage)
    {
       
    }

    public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {
       
    }

    public void OnDisconnected(DisconnectCause cause)
    {
       
    }

    public void OnFriendListUpdate(List<FriendInfo> friendList)
    {
       
    }

    public void OnJoinedLobby()
    {
       
    }

    public void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        //_lbc.CurrentRoom.Players.Values.First().UserId;
        _defaultLobbyPanel.SetActive(false);
        _roomPanel.SetActive(true);
        _pcText.text = $"Players limit: {_maxPlayersCount}";
        _roomNameText.text = $"Room name: {_roomName}";
    }

    public void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed");

        var roomOptions = new RoomOptions
        {
            MaxPlayers = _maxPlayersCount,
            PublishUserId = true,
            IsOpen = true,
            CustomRoomPropertiesForLobby = new[] { GAME_MODE_PROP_KEY, MAP_PROP_KEY }
        };

        var enterRoomParams = new EnterRoomParams
        {
            RoomName = _roomName,
            RoomOptions = roomOptions,
            ExpectedUsers = new[] { "" }
        };

        _lbc.OpCreateRoom(enterRoomParams);
    }

    public void ChangeRoomStatus() 
    {
        var roomOptions = new RoomOptions
        {
            IsOpen = false
        };
        status = "Private";
        _roomStatusText.text = $"Room's status: {status}";
        Debug.Log("Room closed");
    }

    public void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRoomFailed");
    }

    public void OnLeftLobby()
    {
       
    }

    public void OnLeftRoom()
    {
       
    }

    public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
       
    }

    public void OnRegionListReceived(RegionHandler regionHandler)
    {
       
    }

    public void OnRoomListUpdate(List<RoomInfo> roomList)
    {
       
    }
}
