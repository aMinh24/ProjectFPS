
using Photon.Pun;
using UnityEngine;

// Token: 0x02000064 RID: 100
public class GameMenuMulti : BasePopup
{
	// Token: 0x060001BD RID: 445 RVA: 0x00009F79 File Offset: 0x00008179
	public override void Hide()
	{
		base.Hide();
	}

	// Token: 0x060001BE RID: 446 RVA: 0x00009F81 File Offset: 0x00008181
	public override void Init()
	{
		base.Init();
		CharacterAimingMultiplayer[] characterAimingMultiplayers =  Object.FindObjectsOfType<CharacterAimingMultiplayer>();
		foreach(CharacterAimingMultiplayer aim in characterAimingMultiplayers)
		{
			Debug.Log(aim.photonView.CreatorActorNr == PhotonNetwork.LocalPlayer.ActorNumber);
			if (aim.photonView.IsMine && aim.photonView.CreatorActorNr == PhotonNetwork.LocalPlayer.ActorNumber)
			{
				characterAimingMultiplayer = aim;
				photon = characterAimingMultiplayer.photonView; break;
			}

               
		}
		this.Hide();
	}

	// Token: 0x060001BF RID: 447 RVA: 0x00009F9A File Offset: 0x0000819A
	public override void Show(object data)
	{
		base.Show(data);
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
			PhotonNetwork.LeaveRoom();
        }
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x0000A004 File Offset: 0x00008204
	public void OnResumeButton()
	{
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
		if (BaseManager<UIManager>.HasInstance())
		{
			BaseManager<UIManager>.Instance.ShowOverlap<ConfirmBox>(null, true);
		}
	}

	// Token: 0x040001C1 RID: 449
	private PhotonView photon;
	public CharacterAimingMultiplayer characterAimingMultiplayer;
}
