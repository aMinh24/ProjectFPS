using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginCallbaks : MonoBehaviourPunCallbacks
{
    public void OnConnectedToServer()
    {
        Debug.Log("connectServer");
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("connect Master");
        if (UIManager.HasInstance())
        {
            UIManager.Instance.ShowScreen<MainMenu>(null, true);
        }
        if (GameManager.HasInstance())
        {
            GameManager.Instance.login = true;
        }
    }
}