using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListRoomPanel : BaseScreen
{
    public TabMission All;
    public GameObject prefabRoom;

    public override void Hide()
    {
        base.Hide();
    }

    public override void Init()
    {
        base.Init();
    }

    public override void Show(object data)
    {
        base.Show(data);
    }
    public void OnCreateRoomButton()
    {
        //Debug.Log("create " + PhotonNetwork.CurrentRoom==null?"null":"OK");
        if (PhotonNetwork.CurrentRoom != null) { return; }
        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = 4
        };
        string roomName = PhotonNetwork.NickName + "'s room";
        PhotonNetwork.CreateRoom(roomName, roomOptions, null);
        Debug.Log(PhotonNetwork.NetworkClientState);
        //PhotonNetwork.JoinRoom(PhotonNetwork.NickName + "'s room");
    }
    public void OnLeaveRoomButton()
    {
        if (PhotonNetwork.CurrentRoom == null) { return; }
        PhotonNetwork.LeaveRoom();
    }
}
