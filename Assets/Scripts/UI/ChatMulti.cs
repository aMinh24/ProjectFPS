using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Chat.Demo;
using UnityEngine.UI;

public static class ColorTextChat
{
    public const string all = "f0dc8cff";
    public const string ally = "279effff";
    public const string enemy = "ff0000ff";
}
public class ChatMulti : MonoBehaviour, IChatClientListener
{
    public ChatClient chatClient;
    public ChatAppSettings chatAppSettings;
    public Text chatText;
    public InputField chatField;
    public string currentChannel;
    public float time;
    public GameObject chatBox;
    private void Start()
    {
        chatAppSettings = PhotonNetwork.PhotonServerSettings.AppSettings.GetChatSettings();
        this.chatClient = new ChatClient(this);
        this.chatClient.AuthValues = new AuthenticationValues(PhotonNetwork.LocalPlayer.NickName);
        this.chatClient.ConnectUsingSettings(this.chatAppSettings);
        currentChannel = "Global";
        
    }
    private void Update()
    {
        if (this.chatClient != null)
        {
            this.chatClient.Service(); // make sure to call this regularly! it limits effort internally, so calling often is ok!
        }
        if (!string.IsNullOrEmpty(chatField.text))
        {
            time = 10;
        }
        else
        {
            time -= Time.deltaTime;
        }
        if (time <= 0)
        {
            chatBox.SetActive(false);
        }
        OnEnterSend();
    }
    public void OnEnterSend()
    {
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
        {
            if (chatBox.active)
            {
                SendMsg(chatField.text);
                chatField.text = "";
            }
            else
            {
                chatBox.SetActive(true);
                chatField.Select();
                time = 10;
            }
        }
        
    }
    private void SendMsg(string msg)
    {
        if (string.IsNullOrEmpty(msg)) { return; }
        chatClient.PublishMessage(currentChannel, msg);
        Debug.Log("msg");
    }
    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log("Debug");
    }

    public void OnChatStateChange(ChatState state)
    {
        Debug.Log("ChangeState");
    }

    public void OnConnected()
    {
        chatClient.SetOnlineStatus(ChatUserStatus.Online);
        chatClient.Subscribe(currentChannel);
        Debug.Log("Onconnected");
    }

    public void OnDisconnected()
    {
        chatClient.SetOnlineStatus(ChatUserStatus.Offline);
        Debug.Log("OnDisconectChat");
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        ShowChannel(channelName);
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new System.NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        foreach (var channel in channels)
        {
            chatClient.PublishMessage(channel, PhotonNetwork.LocalPlayer.NickName + " join chat");
        }
        ShowChannel(channels[0]);
    }

    public void OnUnsubscribed(string[] channels)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }
    public void ShowChannel(string channelName)
    {
        if (string.IsNullOrEmpty(channelName))
        {
            return;
        }

        ChatChannel channel = null;
        bool found = this.chatClient.TryGetChannel(channelName, out channel);
        if (!found)
        {
            Debug.Log("ShowChannel failed to find channel: " + channelName);
            return;
        }

        //this.selectedChannelName = channelName;
        string[] txt = channel.ToStringMessages().Split('\n');
        string chat = "";
        foreach (string s in txt)
        {
            Debug.Log(s);
            if (string.IsNullOrEmpty(s)) continue;
            string[] strings = s.Split(':');
            chat += $"<color=#{ColorTextChat.all}>[{currentChannel}] {strings[0]}:</color>{strings[1]}\n";
        }
        chatText.text = chat;
        
    }
}
