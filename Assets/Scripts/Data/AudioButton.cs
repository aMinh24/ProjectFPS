
using UnityEngine;

// Token: 0x02000059 RID: 89
public class AudioButton : MonoBehaviour
{
	// Token: 0x06000183 RID: 387 RVA: 0x0000982D File Offset: 0x00007A2D
	public static void OnPointerEnter()
	{
		if (BaseManager<AudioManager>.HasInstance())
		{
			BaseManager<AudioManager>.Instance.PlaySE("OnPointerEnter", 0f);
		}
	}

	// Token: 0x06000184 RID: 388 RVA: 0x0000984A File Offset: 0x00007A4A
	public static void OnPointerDown()
	{
		if (BaseManager<AudioManager>.HasInstance())
		{
			BaseManager<AudioManager>.Instance.PlaySE("OnPointerDown", 0f);
		}
	}
}
