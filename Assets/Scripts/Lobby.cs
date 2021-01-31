using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Lobby : NetworkBehaviour
{
    private Vector2 startposition;
    private int pixels;
    bool touching = false;

    void Awake()
    {

    }
    void Update()
    {
        //if (!isLocalPlayer)
        //{
        //    return;
        //}

        if(!touching && Input.touchCount > 0)
        {
            touching = true;
            startposition = Input.touches[0].position;

        }

        if (touching)
        {
            //if(Input.touches[0].position.y > startposition.y + pixels)
            //{
                Debug.Log($"Position {this.name}");// += new Vector2(0,pixels);
            //}
        }
    }

}
