
using Photon.Pun;
using System.Collections;
using TMPro;
using UnityEngine;

// Token: 0x02000066 RID: 102
public class DefeatPanelMulti : BaseScreen
{
    public TextMeshProUGUI allyScore;
    public TextMeshProUGUI enemyScore;
    // Token: 0x060001CA RID: 458 RVA: 0x0000A335 File Offset: 0x00008535
    public override void Hide()
	{
		base.Hide();
	}

	// Token: 0x060001CB RID: 459 RVA: 0x0000A33D File Offset: 0x0000853D
	public override void Init()
	{
		base.Init();
		base.StartCoroutine(this.ReturnMenu());
	}

	// Token: 0x060001CC RID: 460 RVA: 0x0000A352 File Offset: 0x00008552
	public IEnumerator ReturnMenu()
	{
		yield return new WaitForSeconds(5f);
		if (BaseManager<UIManager>.HasInstance())
		{
			BaseManager<UIManager>.Instance.ShowNotify<LoadingGame>("Main", true);
			BaseManager<UIManager>.Instance.HideAllPopups();
		}
        if (MultiplayerManager.HasInstance())
        {
            Destroy(MultiplayerManager.Instance.gameObject);
        }
        PhotonNetwork.LocalPlayer.CustomProperties.Clear();
        PhotonNetwork.LeaveRoom();
        if (BaseManager<CameraManager>.HasInstance())
		{
			Object.Destroy(BaseManager<CameraManager>.Instance.gameObject);
		}
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		yield break;
	}

	// Token: 0x060001CD RID: 461 RVA: 0x0000A35A File Offset: 0x0000855A
	public override void Show(object data)
	{
		base.Show(data);
        if (data is int[] values)
        {
            allyScore.SetText(values[0].ToString());
            enemyScore.SetText(values[1].ToString());
        }
    }
}
