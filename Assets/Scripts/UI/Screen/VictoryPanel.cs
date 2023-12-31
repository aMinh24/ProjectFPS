﻿
using System.Collections;
using UnityEngine;

// Token: 0x0200006A RID: 106
public class VictoryPanel : BaseScreen
{
	// Token: 0x060001E9 RID: 489 RVA: 0x0000ABB5 File Offset: 0x00008DB5
	public override void Hide()
	{
		base.Hide();
	}

	// Token: 0x060001EA RID: 490 RVA: 0x0000ABBD File Offset: 0x00008DBD
	public override void Init()
	{
		base.Init();
		base.StartCoroutine(this.ReturnMenu());
	}

	// Token: 0x060001EB RID: 491 RVA: 0x0000ABD2 File Offset: 0x00008DD2
	public override void Show(object data)
	{
		base.Show(data);
	}

	// Token: 0x060001EC RID: 492 RVA: 0x0000ABDB File Offset: 0x00008DDB
	public IEnumerator ReturnMenu()
	{
		yield return new WaitForSeconds(5f);
		if (BaseManager<UIManager>.HasInstance())
		{
			BaseManager<UIManager>.Instance.ShowNotify<LoadingGame>("Main", false);
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
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		yield break;
	}
}
