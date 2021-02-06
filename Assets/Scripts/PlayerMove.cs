using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviourPun
{
    Vector3 move = new Vector3(1, 0, 0);
    string[] guids2;
    public static int index = 0;

    void Awake()
    {
       
       
    }
    void Start()
    {
        // Load icons
        guids2 = AssetDatabase.FindAssets("t:Texture2D", new[] { "Assets/Resources/ARISAN" });

        for (int i = 0; i < guids2.Length; i++)
        {
            guids2[i] = AssetDatabase.GUIDToAssetPath(guids2[i]).Replace(".png", "");
            guids2[i] = guids2[i].Replace("Assets/Resources/", "");
        }

    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0) {
            if (photonView.IsMine)
            {
                index++;
                if(index == guids2.Length)
                {
                    index = 0;
                }
                this.GetComponent<Image>().sprite = Resources.Load<Sprite>(guids2[index]);
               

            }
        }

    }
}
