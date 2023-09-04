
using Photon.Pun;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000060 RID: 96
public class LoadingGameMulti : BaseNotify
{
	// Token: 0x060001A4 RID: 420 RVA: 0x00009C52 File Offset: 0x00007E52
	public override void Hide()
	{
		base.Hide();
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x00009C5A File Offset: 0x00007E5A
	public override void Init()
	{
		base.Init();
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x00009C64 File Offset: 0x00007E64
	public override void Show(object data)
	{
		base.Show(data);
		base.StartCoroutine(LoadScene());
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x00009CBF File Offset: 0x00007EBF
	private IEnumerator LoadScene()
	{
		while ((PhotonNetwork.LevelLoadingProgress<1f))
		{
			Debug.Log(PhotonNetwork.LevelLoadingProgress);
			this.loadingSlider.value = PhotonNetwork.LevelLoadingProgress;

            this.loadingPercentText.SetText(string.Format("LOADING: {0}%", PhotonNetwork.LevelLoadingProgress * 100f), true);
			if (PhotonNetwork.LevelLoadingProgress >= 0.9f)
			{
				this.loadingSlider.value = 1f;
				this.loadingPercentText.SetText(string.Format("LOADING: {0}%", this.loadingSlider.value * 100f), true);
				this.Hide();
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x040001BD RID: 445
	public TextMeshProUGUI loadingPercentText;

	// Token: 0x040001BE RID: 446
	public Slider loadingSlider;
}
