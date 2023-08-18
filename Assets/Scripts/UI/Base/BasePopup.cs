

// Token: 0x0200005C RID: 92
public class BasePopup : BaseUIElement
{
	// Token: 0x0600018E RID: 398 RVA: 0x000098BF File Offset: 0x00007ABF
	public override void Hide()
	{
		base.Hide();
	}

	// Token: 0x0600018F RID: 399 RVA: 0x000098C7 File Offset: 0x00007AC7
	public override void Init()
	{
		base.Init();
		this.uiType = UIType.Popup;
	}

	// Token: 0x06000190 RID: 400 RVA: 0x000098D6 File Offset: 0x00007AD6
	public override void Show(object data)
	{
		base.Show(data);
	}
}
