using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEditor;

public class LobbyManager : MonoBehaviour
{
    public InputField nameInput;
    public Button createRoom;
    public Button joinRoom;


    public int index = 0;
    Object[] guids2;
    private string roomName;


    private void Awake()
    {
        // Load icons
        guids2 = Resources.LoadAll("ARISAN/100 Alchemy item icons-free", typeof(Sprite));
    }
    private void Start()
    {
        Debug.Log($"Start");
     
        createRoom.onClick.AddListener(OnClickCreateRoom);

        joinRoom.onClick.AddListener(OnClickJoinRoom);
    }

    public void OnClickCreateRoom()
    {

        Debug.Log($"Click");
        roomName = nameInput.text;// == null ? PhotonNetwork.CountOfRooms.ToString() : nameInput.text;
        PhoNetworkManager.instance.CreateRoom(roomName);
    }
    public void OnClickJoinRoom()
    {

        Debug.Log($"JoinRoom");

        roomName = nameInput.text;
        PhoNetworkManager.instance.JoindRoom(roomName);
    }


    public void ChangeIcon()
    {
        index++;
        Debug.Log($"Index is {index} and icons are {guids2.Length}");
        if (index == guids2.Length)
        {
            index = 0;
        }
        var icon = guids2[index];
        GameObject thisPlayer = GameObject.Find("LocalPlayer");
        thisPlayer.GetComponent<Image>().sprite = (Sprite)icon;
    }

    public static void CreatePlayer()
    {

        var playerHolder = GameObject.Find("PlayerHolder");
        var playerData = GameObject.Find("LocalPlayer");
        playerHolder.GetComponent<Image>().sprite = playerData.GetComponent<Image>().sprite;
        playerHolder.GetComponentInChildren<Text>().text = GameObject.Find("LocalPlayerName").GetComponent<InputField>().text;

        InitiatePlayer();
    }
    public static void InitiatePlayer(){

        var player = GameObject.Find("PlayerHolder");
        var playerNumber = player.GetComponent<Image>().sprite.name.Substring(0,3);
        if(!playerNumber.Equals("001") && !playerNumber.Equals("004") && !playerNumber.Equals("002") && !playerNumber.Equals("006"))
        {
            playerNumber = "001";
        }
        var currentPlayers = PhotonNetwork.CurrentRoom.PlayerCount;

        var canvas = GameObject.Find("RoomLobby");
        var thisPlayer = PhotonNetwork.Instantiate("Player" + playerNumber, canvas.transform.position, canvas.transform.rotation);
        thisPlayer.GetComponentInChildren<Text>().text = player.GetComponentInChildren<Text>().text;
       
        GameObject.Destroy(player);
        var roomName = GameObject.Find("RoomName").GetComponent<Text>();
        roomName.text = PhotonNetwork.CurrentRoom.Name + " - Current players: " + PhotonNetwork.CurrentRoom.PlayerCount;

    }
}
