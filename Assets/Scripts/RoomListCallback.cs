using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            roomRow.InitRow((byte)roomObject.Count, roomInfo.Name, "City", (byte)roomInfo.PlayerCount, (byte)roomInfo.MaxPlayers);
            roomObject.Add(row);
        }
    }
    private void ClearRoom()
    {
        foreach (GameObject room in roomObject)
        {
            Destroy(room);
            roomObject.Remove(room);
        }
    }
}
