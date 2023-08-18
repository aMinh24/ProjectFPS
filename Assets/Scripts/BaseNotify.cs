

// Token: 0x0200005A RID: 90
public class BaseNotify : BaseUIElement
{
	// Token: 0x06000186 RID: 390 RVA: 0x0000986F File Offset: 0x00007A6F
	public override void Hide()
	{
		base.Hide();
	}

	// Token: 0x06000187 RID: 391 RVA: 0x00009877 File Offset: 0x00007A77
	public override void Init()
	{
		base.Init();
		this.uiType = UIType.Notify;
	}

	// Token: 0x06000188 RID: 392 RVA: 0x00009886 File Offset: 0x00007A86
	public override void Show(object data)
	{
		base.Show(data);
	}
}
