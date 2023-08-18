
using UnityEngine.UI;

// Token: 0x02000061 RID: 97
public class NotifyMission : BaseNotify
{
	// Token: 0x060001A9 RID: 425 RVA: 0x00009CDD File Offset: 0x00007EDD
	public override void Hide()
	{
		base.Hide();
	}

	// Token: 0x060001AA RID: 426 RVA: 0x00009CE5 File Offset: 0x00007EE5
	public override void Init()
	{
		base.Init();
	}

	// Token: 0x060001AB RID: 427 RVA: 0x00009CF0 File Offset: 0x00007EF0
	public override void Show(object data)
	{
		base.Show(data);
		string text = data as string;
		if (text != null)
		{
			this.notifyText.text = "Done " + text;
		}
		base.Invoke("Hide", 5f);
	}

	// Token: 0x040001BF RID: 447
	public Text notifyText;
}
