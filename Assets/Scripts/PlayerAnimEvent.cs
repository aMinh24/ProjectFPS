
using UnityEngine;

// Token: 0x02000056 RID: 86
public class PlayerAnimEvent : MonoBehaviour
{
	// Token: 0x06000177 RID: 375 RVA: 0x00009561 File Offset: 0x00007761
	public void SEAudio(string name)
	{
		if (BaseManager<AudioManager>.HasInstance())
		{
			BaseManager<AudioManager>.Instance.PlaySE(name, 0f);
		}
	}
}
