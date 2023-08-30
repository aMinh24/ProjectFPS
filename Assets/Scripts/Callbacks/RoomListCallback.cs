using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class RoomListCallback : MonoBehaviourPunCallbacks
{
    public ListRoomPanel listRoomPanel;
    public List <GameObject> roomObject;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        ClearRoom();
        foreach (RoomInfo roomInfo in roomList)
        {
            if (roomInfo.RemovedFromList)
            {
                continue;
            }
            GameObject row = Instantiate(listRoomPanel.prefabRoom,listRoomPanel.All.board.transform);
            RoomRow roomRow = row.GetComponent<RoomRow>();
            roomRow.toggle.group = listRoomPanel.All.board.GetComponent<ToggleGroup>();
            string[] info = roomInfo.Name.Split('_');
            roomRow.InitRow((byte)roomObject.Count, info[0], info[1], (byte)roomInfo.PlayerCount, (byte)roomInfo.MaxPlayers);
            roomObject.Add(row);
        }
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        
    }
    
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        PhotonNetwork.LoadLevel(PhotonNetwork.CurrentRoom.Name.Split('_')[1]);
        //Debug.Log("joined room "+PhotonNetwork.CurrentRoom);
        
    }

    private void ClearRoom()
    {
        foreach (GameObject room in roomObject)
        {
            Destroy(room);
            
        }
        roomObject.Clear();
    }
    //private IEnumerator waitConnectMaster()
    //{
    //    while (!isConnected)
    //    {
    //        yield return null;
    //    }
    //    PhotonNetwork.JoinLobby();
    //}
    

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }
}
