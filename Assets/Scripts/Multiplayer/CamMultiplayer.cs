using Cinemachine;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMultiplayer : MonoBehaviourPun
{
    public AudioListener Listener;
    private void Awake()
    {
        if (!photonView.IsMine) { return; }
        if (photonView.CreatorActorNr != PhotonNetwork.LocalPlayer.ActorNumber)
        {
            return;
        }
        brain.enabled = true;
        Listener.enabled = true;
    }
    private void Start()
    {
        //this.ChangeCam(virCam.Main);
        if (BaseManager<ListenerManager>.HasInstance())
        {
            BaseManager<ListenerManager>.Instance.Register(ListenType.ON_PLAYER_DEATH, new Action<object>(this.DeathCamera));
        }
    }

    // Token: 0x06000109 RID: 265 RVA: 0x00006CA7 File Offset: 0x00004EA7
    public void OnDestroy()
    {
        if (BaseManager<ListenerManager>.HasInstance())
        {
            BaseManager<ListenerManager>.Instance.Unregister(ListenType.ON_PLAYER_DEATH, new Action<object>(this.DeathCamera));
        }
    }

    // Token: 0x0600010A RID: 266 RVA: 0x00006CC7 File Offset: 0x00004EC7
    public void DeathCamera(object data)
    {
        this.brain.m_DefaultBlend.m_Time = 2f;
        this.ChangeCam(virCam.Death);
    }

    // Token: 0x0600010B RID: 267 RVA: 0x00006CE5 File Offset: 0x00004EE5
    public void TurnOffScope()
    {
        if (BaseManager<ListenerManager>.HasInstance())
        {
            BaseManager<ListenerManager>.Instance.BroadCast(ListenType.CHANGE_SCOPE, true);
        }
        this.ChangeCam(virCam.Main);
        this.isAiming = false;
    }

    // Token: 0x0600010C RID: 268 RVA: 0x00006D0D File Offset: 0x00004F0D
    public void ChangeScope()
    {
        if (BaseManager<ListenerManager>.HasInstance())
        {
            BaseManager<ListenerManager>.Instance.BroadCast(ListenType.CHANGE_SCOPE, null);
        }
        if (this.isAiming)
        {
            this.ChangeCam(virCam.Main);
            this.isAiming = false;
            return;
        }
        this.ChangeCam(virCam.Scope);
        this.isAiming = true;
    }

    // Token: 0x0600010D RID: 269 RVA: 0x00006D48 File Offset: 0x00004F48
    private void ChangeCam(virCam cam)
    {
        CinemachineVirtualCamera[] array = this.virtualCameras;
        for (int i = 0; i < array.Length; i++)
        {
            array[i].Priority = 0;
        }
        this.virtualCameras[(int)cam].Priority = 20;
    }

    // Token: 0x04000144 RID: 324

    // Token: 0x04000145 RID: 325
    public CinemachineVirtualCamera[] virtualCameras;

    // Token: 0x04000146 RID: 326
    public CinemachineBrain brain;

    // Token: 0x04000147 RID: 327
    public bool isAiming;
}
