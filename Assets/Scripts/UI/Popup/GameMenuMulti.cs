
using Photon.Pun;
using UnityEngine;

// Token: 0x02000064 RID: 100
public class GameMenuMulti : BasePopup
{
	// Token: 0x060001BD RID: 445 RVA: 0x00009F79 File Offset: 0x00008179
	public override void Hide()
	{
        //if (ListenerManager.HasInstance())
        //{
        //    ListenerManager.Instance.Unregister(ListenType.ON_PLAYER_DEATH,ResumeGame);
        //}
        base.Hide();
        
    }

	// Token: 0x060001BE RID: 446 RVA: 0x00009F81 File Offset: 0x00008181
	public override void Init()
	{
		base.Init();
		
		//this.Hide();
	}

	// Token: 0x060001BF RID: 447 RVA: 0x00009F9A File Offset: 0x0000819A
	public override void Show(object data)
	{
		base.Show(data);
        CharacterAimingMultiplayer[] characterAimingMultiplayers = Object.FindObjectsOfType<CharacterAimingMultiplayer>();
        foreach (CharacterAimingMultiplayer aim in characterAimingMultiplayers)
        {
            if (aim.photonView.IsMine && aim.photonView.CreatorActorNr == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                characterAimingMultiplayer = aim;
                photon = characterAimingMultiplayer.photonView; break;
            }


        }
		//if (ListenerManager.HasInstance())
		//{
		//	ListenerManager.Instance.Register(ListenType.ON_PLAYER_DEATH, ResumeGame);
		//}
    }

	// Token: 0x060001C0 RID: 448 RVA: 0x00009FA4 File Offset: 0x000081A4
	public void OnLeaveGameButton()
	{
		this.Hide();
		if (BaseManager<UIManager>.HasInstance())
		{
			BaseManager<UIManager>.Instance.ShowNotify<LoadingGame>("Main", true);
			BaseManager<UIManager>.Instance.HideAllPopups();
		}
		if (BaseManager<ObjectPool>.HasInstance())
		{
			Object.Destroy(BaseManager<ObjectPool>.Instance.gameObject);
		}
		if (BaseManager<CameraManager>.HasInstance())
		{
			Object.Destroy(BaseManager<CameraManager>.Instance.gameObject);
		}
		if (MultiplayerManager.HasInstance())
		{
			Destroy(MultiplayerManager.Instance.gameObject);
		}
		if (characterAimingMultiplayer!= null)
		{
            if (!photon.IsMine) { return; }
            if (photon.CreatorActorNr != PhotonNetwork.LocalPlayer.ActorNumber)
            {
                return;
            }
			PhotonNetwork.LocalPlayer.CustomProperties.Clear();
			PhotonNetwork.LeaveRoom();
        }
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x0000A004 File Offset: 0x00008204
	public void ResumeGame(object? data)
	{
		OnResumeButton();
	}
	public void OnResumeButton()
	{
        if (!photon.IsMine) { return; }
        if (photon.CreatorActorNr != PhotonNetwork.LocalPlayer.ActorNumber)
        {
            return;
        }
        Cursor.visible = true;
		Cursor.lockState = CursorLockMode.Locked;
		if (characterAimingMultiplayer != null)
		{
			characterAimingMultiplayer.isEcs = false;
		}
		this.Hide();
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x0000A032 File Offset: 0x00008232
	public void OnExitButton()
	{
        if (!photon.IsMine) { return; }
        if (photon.CreatorActorNr != PhotonNetwork.LocalPlayer.ActorNumber)
        {
            return;
        }
        if (BaseManager<UIManager>.HasInstance())
		{
			BaseManager<UIManager>.Instance.ShowOverlap<ConfirmBox>(null, true);
		}
	}

	// Token: 0x040001C1 RID: 449
	private PhotonView photon;
	public CharacterAimingMultiplayer characterAimingMultiplayer;
}
