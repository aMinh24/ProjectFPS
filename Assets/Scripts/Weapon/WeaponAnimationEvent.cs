
using UnityEngine;

// Token: 0x02000049 RID: 73
public class WeaponAnimationEvent : MonoBehaviour
{
	// Token: 0x060000F2 RID: 242 RVA: 0x000066AB File Offset: 0x000048AB
	public void OnAnimationEvent(string eventName)
	{
		this.WeaponAnimEvent.Invoke(eventName);
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x000066B9 File Offset: 0x000048B9
	public void OnHolsterEvent(string eventName)
	{
		this.WeaponAnimEvent.Invoke(eventName);
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x000066C7 File Offset: 0x000048C7
	public void OnAudioEvent(string audioName)
	{
		if (BaseManager<AudioManager>.HasInstance())
		{
			if (audioName.Equals("walk"))
			{
				audioName += Random.Range(0, 5);
			}
			BaseManager<AudioManager>.Instance.PlaySE(audioName, 0f);
		}
	}

	// Token: 0x04000134 RID: 308
	public _AnimationEvent WeaponAnimEvent = new _AnimationEvent();
}
