using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PhoNetworkManager : MonoBehaviourPunCallbacks
{
    public static PhoNetworkManager instance;

    public Photon.Realtime.Room currentRoom;

     void Awake()
    {
        if(instance != null && instance != this)
        {
            gameObject.SetActive(false);

        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); 
    }
    public override void OnConnectedToMaster()
    {
        //        base.OnConnectedToMaster();
        Debug.Log($"Connected to Master");

        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        Debug.Log($"Join Lobby");
    }

    public override void OnCreatedRoom()
    {
        //        base.OnCreatedRoom();
        Debug.Log($"Connected to Room {PhotonNetwork.CurrentRoom.Name}");

    ChangeScene("Lobby");
    }
    public override void OnJoinedRoom()
    {
        ChangeScene("Lobby");

    }

    public void CreateRoom(string name)
    {
        PhotonNetwork.CreateRoom(name);
        Debug.Log($"Created room name is {name}");
    }

    public void JoindRoom(string name)
    {
        PhotonNetwork.JoinRoom(name);
    }

    public void ChangeScene(string name)
    {
        PhotonNetwork.LoadLevel(name);
       
    }
}
