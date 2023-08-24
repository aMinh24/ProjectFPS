using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCallbacks : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        Debug.Log("create room");
        if (PhotonNetwork.CurrentRoom != null)
        {
            PhotonNetwork.Instantiate("Player", new Vector3(98, 2f, 89), Quaternion.identity);
        }
    }

    //public override void OnPlayerEnteredRoom(Player newPlayer)
    //{
    //    base.OnPlayerEnteredRoom(newPlayer);
    //    Debug.Log("joinedroom");
    //    if (PhotonNetwork.CurrentRoom != null)
    //    {
    //        PhotonNetwork.Instantiate("Player", new Vector3(98, 2f, 89), Quaternion.identity);
    //    }
    //}
}
