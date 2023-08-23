using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ListRoomPanel : BaseScreen
{
    public TabMission All;
    public GameObject prefabRoom;
    public GameObject curSelectedRoomObject;
    public Text curSelectedRoomName;
    public Text curSelectedRoomMode;
    public Text curSelectedRoomMap;

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
        All.TurnOn();
        OnDeselectRoom(null);
        if (ListenerManager.HasInstance())
        {
            ListenerManager.Instance.Register(ListenType.ON_SELECTED_ROOM, OnSelectRoom);
            ListenerManager.Instance.Register(ListenType.ON_DESELECTED_ROOM, OnDeselectRoom);
        }
    }
    public void OnDeselectRoom(object? data)
    {
        if (curSelectedRoomObject.active)
        {
            curSelectedRoomObject.SetActive(false);
        }
    }
    public void OnSelectRoom(object data)
    {
        if (data is RoomRow roomRow)
        {
            curSelectedRoomObject.SetActive(true);
            curSelectedRoomName.text = roomRow.nameText.text;
            curSelectedRoomMode.text = roomRow.modeText.text;
            curSelectedRoomMap.text = roomRow.mapText.text;
        }
        
    }
    public void OnJoinRoomButton()
    {
        PhotonNetwork.JoinRoom(curSelectedRoomName.text);
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
