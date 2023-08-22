using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BaseScreen
{
    public TextMeshProUGUI textMeshProUGUI;
    public InputField name;
    #region panel
    private void Update()
    {
        textMeshProUGUI.SetText("network: "+ PhotonNetwork.NetworkClientState.ToString());
    }
    public void OnLoginButton()
    {
        string playerName = name.text;
        
        if (playerName != "")
        {
            PhotonNetwork.LocalPlayer.NickName = playerName;
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log(playerName + " connect");
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
    }

    public override void Show(object data)
    {
        base.Show(data);
    }
    #endregion

}

