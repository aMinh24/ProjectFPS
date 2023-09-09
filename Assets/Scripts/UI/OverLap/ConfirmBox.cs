
using UnityEngine;

// Token: 0x02000062 RID: 98
public class ConfirmBox : BaseOverlap
{
	// Token: 0x060001AD RID: 429 RVA: 0x00009D3C File Offset: 0x00007F3C
	public override void Hide()
	{
		base.Hide();
	}

	// Token: 0x060001AE RID: 430 RVA: 0x00009D44 File Offset: 0x00007F44
	public override void Init()
	{
		base.Init();
	}

	// Token: 0x060001AF RID: 431 RVA: 0x00009D4C File Offset: 0x00007F4C
	public override void Show(object data)
	{
		base.Show(data);
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x00009D55 File Offset: 0x00007F55
	public void OnCancelButton()
	{
		this.Hide();
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x00009D5D File Offset: 0x00007F5D
	public void OnConfirmButton()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
		Application.Quit();
	}
}
