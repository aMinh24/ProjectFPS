using Photon.Pun;
using UnityEngine.UI;

// Token: 0x02000069 RID: 105
public class MainMenu : BaseScreen
{
	public Text name;
	// Token: 0x060001E2 RID: 482 RVA: 0x0000AB23 File Offset: 0x00008D23
	public override void Hide()
	{
		base.Hide();
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x0000AB2B File Offset: 0x00008D2B
	public override void Init()
	{
		base.Init();
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x0000AB33 File Offset: 0x00008D33
	public override void Show(object data)
	{
		base.Show(data);
		name.text = PhotonNetwork.NickName;
	}

	// Token: 0x060001E5 RID: 485 RVA: 0x0000AB3C File Offset: 0x00008D3C
	public void OnCampaignButton()
	{
		if (BaseManager<UIManager>.HasInstance())
		{
			BaseManager<UIManager>.Instance.ShowNotify<LoadingGame>("Campaign".ToString(), true);
		}
		this.Hide();
	}

	// Token: 0x060001E6 RID: 486 RVA: 0x0000AB60 File Offset: 0x00008D60
	public void OnMissionButton()
	{
		if (BaseManager<UIManager>.HasInstance())
		{
			if (BaseManager<UIManager>.Instance.CurPopup is MissionPanel && !BaseManager<UIManager>.Instance.CurPopup.IsHide)
			{
				return;
			}
			BaseManager<UIManager>.Instance.ShowPopup<MissionPanel>(null, true);
		}
	}
	public void OnMultiplayerButton()
	{
		PhotonNetwork.JoinLobby();
	}
	// Token: 0x060001E7 RID: 487 RVA: 0x0000AB98 File Offset: 0x00008D98
	public void OnExitButton()
	{
		if (BaseManager<UIManager>.HasInstance())
		{
			BaseManager<UIManager>.Instance.ShowOverlap<ConfirmBox>(null, false);
		}
	}
}
