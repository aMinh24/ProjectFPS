
using UnityEngine;

// Token: 0x0200005E RID: 94
public class BaseUIElement : MonoBehaviour
{
	// Token: 0x1700000D RID: 13
	// (get) Token: 0x06000196 RID: 406 RVA: 0x0000990F File Offset: 0x00007B0F
	public bool IsInited
	{
		get
		{
			return this.isInited;
		}
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x06000197 RID: 407 RVA: 0x00009917 File Offset: 0x00007B17
	public bool IsHide
	{
		get
		{
			return this.isHide;
		}
	}

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x06000198 RID: 408 RVA: 0x0000991F File Offset: 0x00007B1F
	public CanvasGroup CanvasGroup
	{
		get
		{
			return this.canvasGroup;
		}
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x06000199 RID: 409 RVA: 0x00009927 File Offset: 0x00007B27
	public UIType UIType
	{
		get
		{
			return this.uiType;
		}
	}

	// Token: 0x0600019A RID: 410 RVA: 0x00009930 File Offset: 0x00007B30
	public virtual void Init()
	{
		this.isInited = true;
		if (!base.gameObject.GetComponent<CanvasGroup>())
		{
			base.gameObject.AddComponent<CanvasGroup>();
		}
		this.canvasGroup = base.gameObject.GetComponent<CanvasGroup>();
		base.gameObject.SetActive(true);
	}

	// Token: 0x0600019B RID: 411 RVA: 0x0000997F File Offset: 0x00007B7F
	public virtual void Show(object data)
	{
		base.gameObject.SetActive(true);
		this.isHide = false;
		this.SetActiveCanvasGroup(true);
	}

	// Token: 0x0600019C RID: 412 RVA: 0x0000999B File Offset: 0x00007B9B
	public virtual void Hide()
	{
		this.isHide = true;
		this.SetActiveCanvasGroup(false);
	}

	// Token: 0x0600019D RID: 413 RVA: 0x000099AB File Offset: 0x00007BAB
	private void SetActiveCanvasGroup(bool isActive)
	{
		if (this.CanvasGroup != null)
		{
			this.CanvasGroup.blocksRaycasts = isActive;
			this.CanvasGroup.alpha = (float)(isActive ? 1 : 0);
		}
	}

	// Token: 0x0600019E RID: 414 RVA: 0x000099DA File Offset: 0x00007BDA
	public static void OnPointerEnter()
	{
		if (BaseManager<AudioManager>.HasInstance())
		{
			BaseManager<AudioManager>.Instance.PlaySE("OnPointerEnter", 0f);
		}
	}

	// Token: 0x0600019F RID: 415 RVA: 0x000099F7 File Offset: 0x00007BF7
	public static void OnPointerDown()
	{
		if (BaseManager<AudioManager>.HasInstance())
		{
			BaseManager<AudioManager>.Instance.PlaySE("OnPointerDown", 0f);
		}
	}

	// Token: 0x040001B5 RID: 437
	protected CanvasGroup canvasGroup;

	// Token: 0x040001B6 RID: 438
	protected UIType uiType;

	// Token: 0x040001B7 RID: 439
	protected bool isHide;

	// Token: 0x040001B8 RID: 440
	protected bool isInited;
}
