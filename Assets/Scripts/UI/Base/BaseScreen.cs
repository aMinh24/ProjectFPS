

// Token: 0x0200005D RID: 93
public class BaseScreen : BaseUIElement
{
	// Token: 0x06000192 RID: 402 RVA: 0x000098E7 File Offset: 0x00007AE7
	public override void Hide()
	{
		base.Hide();
	}

	// Token: 0x06000193 RID: 403 RVA: 0x000098EF File Offset: 0x00007AEF
	public override void Init()
	{
		base.Init();
		this.uiType = UIType.Screen;
	}

	// Token: 0x06000194 RID: 404 RVA: 0x000098FE File Offset: 0x00007AFE
	public override void Show(object data)
	{
		base.Show(data);
	}
}
