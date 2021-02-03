using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviourPun
{
    Vector3 move = new Vector3(1, 0, 0);

    void Awake()
    {

    }
    void Start()
    {
        Debug.Log(this.transform.position);
    }

    void Update()
    {


        if (Input.touchCount > 0)
        {
            if (photonView.IsMine)
            {
                this.transform.position = this.transform.position + move;
                Debug.Log(this.transform.position);
            }
        }

    }
}
