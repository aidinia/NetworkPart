using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{

    public Text roomName;
    public Button startButton;
    [SerializeField] public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        var playersCanvas = GameObject.Find("Players").transform;
        var currentPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
        var placeInX = ((playersCanvas.GetComponent<RectTransform>().rect.width / 4)+ player.GetComponent<RectTransform>().rect.width/2)*currentPlayers;
        Vector3 slot = new Vector3 (placeInX, playersCanvas.position.y, playersCanvas.position.z);
        var thisPlayer = PhotonNetwork.Instantiate(player.name, slot, playersCanvas.rotation);

        thisPlayer.transform.parent = playersCanvas;
        roomName.text = PhotonNetwork.CurrentRoom.Name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
