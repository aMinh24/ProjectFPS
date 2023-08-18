
using UnityEngine;

// Token: 0x0200004B RID: 75
public class BaseManager<T> : MonoBehaviour where T : BaseManager<T>
{
	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000103 RID: 259 RVA: 0x00006BA0 File Offset: 0x00004DA0
	public static T Instance
	{
		get
		{
			if (BaseManager<T>.instance == null)
			{
				BaseManager<T>.instance = UnityEngine.Object.FindObjectOfType<T>();
				if (BaseManager<T>.instance == null)
				{
					Debug.Log("No " + typeof(T).Name + " Singleton Instance");
				}
			}
			return BaseManager<T>.instance;
		}
	}

	// Token: 0x06000104 RID: 260 RVA: 0x00006C03 File Offset: 0x00004E03
	public static bool HasInstance()
	{
		return BaseManager<T>.instance != null;
	}

	// Token: 0x06000105 RID: 261 RVA: 0x00006C15 File Offset: 0x00004E15
	protected virtual void Awake()
	{
		this.CheckInstance();
	}

	// Token: 0x06000106 RID: 262 RVA: 0x00006C20 File Offset: 0x00004E20
	protected bool CheckInstance()
	{
		if (BaseManager<T>.instance == null)
		{
			BaseManager<T>.instance = (T)((object)this);
			Object.DontDestroyOnLoad(this);
			return true;
		}
		if (BaseManager<T>.instance == this)
		{
			Object.DontDestroyOnLoad(this);
			return true;
		}
		Object.Destroy(base.gameObject);
		return false;
	}

	// Token: 0x04000143 RID: 323
	private static T instance;
}
