
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000063 RID: 99
public class OverlapFade : BaseOverlap
{
	// Token: 0x060001B3 RID: 435 RVA: 0x00009D6C File Offset: 0x00007F6C
	public override void Hide()
	{
		base.Hide();
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x00009D74 File Offset: 0x00007F74
	public override void Init()
	{
		base.Init();
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x00009D7C File Offset: 0x00007F7C
	public override void Show(object data)
	{
		base.Show(data);
		string text = data as string;
		if (text != null)
		{
			if (text == "Main")
			{
				this.FadeShowMenu(1f);
				return;
			}
			if (!(text == "CampaignOffline"))
			{
				return;
			}
			this.FadeShowGameUI(1f);
		}
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x00009DCC File Offset: 0x00007FCC
	public void FadeShowMenu(float fadeTime)
	{
		this.imgFade.color = Color.black;
		this.SetAlpha(0f);
		Sequence sequence = DOTween.Sequence();
		if (BaseManager<AudioManager>.HasInstance())
		{
			BaseManager<AudioManager>.Instance.AttachBGMSource.Play();
			BaseManager<AudioManager>.Instance.StopGT();
		}
		sequence.Append(this.imgFade.DOFade(1f, fadeTime));
		if (BaseManager<UIManager>.HasInstance())
		{
			BaseManager<UIManager>.Instance.ShowScreen<MainMenu>(null, true);
		}
		sequence.AppendInterval(fadeTime / 2f);
		sequence.Append(this.imgFade.DOFade(0f, fadeTime));
		sequence.OnComplete(delegate
		{
			this.OnFinish();
		});
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x00009E7C File Offset: 0x0000807C
	public void FadeShowGameUI(float fadeTime)
	{
		this.imgFade.color = Color.black;
		if (BaseManager<AudioManager>.HasInstance())
		{
			BaseManager<AudioManager>.Instance.AttachBGMSource.Stop();
			BaseManager<AudioManager>.Instance.PlayGT();
		}
		this.SetAlpha(0f);
		Sequence sequence = DOTween.Sequence();
		Debug.Log("fade start");
		sequence.Append(this.imgFade.DOFade(1f, fadeTime));
		Debug.Log("Gameui");
		if (BaseManager<UIManager>.HasInstance())
		{
			BaseManager<UIManager>.Instance.ShowScreen<GameUI>(null, true);
		}
		sequence.AppendInterval(fadeTime / 2f);
		sequence.Append(this.imgFade.DOFade(0f, fadeTime));
		sequence.OnComplete(delegate
		{
			this.OnFinish();
		});
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x00009F2C File Offset: 0x0000812C
	private void SetAlpha(float alp)
	{
		Color color = this.imgFade.color;
		color.a = alp;
		this.imgFade.color = color;
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x00009F59 File Offset: 0x00008159
	private void OnFinish()
	{

		this.Hide();
    }

	// Token: 0x040001C0 RID: 448
	[SerializeField]
	private Image imgFade;
}
