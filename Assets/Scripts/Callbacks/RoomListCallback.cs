using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
public enum BUTTON
{
    CREATE,
    LEAVE,
    JOIN,
    CANCEL,
    START,
    READY
}
public class RoomListCallback : MonoBehaviourPunCallbacks
{
    public ListRoomPanel listRoomPanel;
    public List <GameObject> roomObject;
    public GameObject room;
    public GameObject roomList;
    public GameObject[] buttons;

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
        PhotonNetwork.LoadLevel("Main");
        room.SetActive(false);
        roomList.SetActive(true);
        foreach(GameObject button in buttons)
        {
            button.SetActive(false);
        }
        buttons[(int)BUTTON.CREATE].SetActive(true);
        buttons[(int)BUTTON.JOIN].SetActive(true);
    }
    
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        room.SetActive(true);
        roomList.SetActive(false );
        foreach (GameObject button in buttons)
        {
            button.SetActive(false);
        }
        if (PhotonNetwork.IsMasterClient)
        {
            buttons[(int)BUTTON.START].SetActive(true);
        }
        else
        {
            buttons[(int)BUTTON.READY].SetActive(true);
        }
        buttons[(int)BUTTON.LEAVE].SetActive(true);
        PhotonNetwork.LoadLevel(PhotonNetwork.CurrentRoom.Name.Split('_')[1]);
        listRoomPanel.playerListUpdate();
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

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        listRoomPanel.playerListUpdate();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        listRoomPanel.playerListUpdate();

    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        listRoomPanel.playerListUpdate();
    }
}
