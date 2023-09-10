using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BaseScreen
{
    public InputField name;
    #region panel
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnLoginButton();
        }
    }
    public void OnLoginButton()
    {
        string playerName = name.text;
        
        if (playerName != "")
        {
            PhotonNetwork.LocalPlayer.NickName = playerName;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.LogError("Player Name is invalid.");
        }
    }

    public override void Hide()
    {
        base.Hide();
    }

    public override void Init()
    {
        base.Init();
        name.Select();
    }

    public override void Show(object data)
    {
        base.Show(data);
    }
    #endregion

}

