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
        PhotonNetwork.Instantiate(player.name, player.transform.position, player.transform.rotation);
        roomName.text = PhotonNetwork.CurrentRoom.Name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
