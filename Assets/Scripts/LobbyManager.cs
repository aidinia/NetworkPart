using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class LobbyManager : MonoBehaviour
{
    public InputField nameInput;
    public Button createRoom;
    public Button joinRoom;

    private string roomName;

    private void Start()
    {
        Debug.Log($"Start");
     
        createRoom.onClick.AddListener(OnClickCreateRoom);

        joinRoom.onClick.AddListener(OnClickJoinRoom);
    }

    public void OnClickCreateRoom()
    {

        Debug.Log($"Click");
        roomName = nameInput.text == null? PhotonNetwork.CountOfRooms.ToString() : nameInput.text;
        PhoNetworkManager.instance.CreateRoom(roomName);
    }
    public void OnClickJoinRoom()
    {

        Debug.Log($"JoinRoom");

        roomName = nameInput.text;
        PhoNetworkManager.instance.JoindRoom(roomName);
    }
}
