using System;
using System.Collections.Generic;
using UnityEngine;
// Token: 0x02000051 RID: 81
public class UIManager : BaseManager<UIManager>
{
	// Token: 0x17000005 RID: 5
	// (get) Token: 0x0600012B RID: 299 RVA: 0x000076CD File Offset: 0x000058CD
	public Dictionary<string, BaseScreen> Screens
	{
		get
		{
			return this.screens;
		}
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x0600012C RID: 300 RVA: 0x000076D5 File Offset: 0x000058D5
	public Dictionary<string, BasePopup> Popups
	{
		get
		{
			return this.popups;
		}
	}

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x0600012D RID: 301 RVA: 0x000076DD File Offset: 0x000058DD
	public Dictionary<string, BaseOverlap> Overlaps
	{
		get
		{
			return this.overlaps;
		}
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x0600012E RID: 302 RVA: 0x000076E5 File Offset: 0x000058E5
	public Dictionary<string, BaseNotify> Notifies
	{
		get
		{
			return this.notifies;
		}
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x0600012F RID: 303 RVA: 0x000076ED File Offset: 0x000058ED
	public BaseScreen CurScreen
	{
		get
		{
			return this.curScreen;
		}
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x06000130 RID: 304 RVA: 0x000076F5 File Offset: 0x000058F5
	public BasePopup CurPopup
	{
		get
		{
			return this.curPopup;
		}
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x06000131 RID: 305 RVA: 0x000076FD File Offset: 0x000058FD
	public BaseNotify CurNotify
	{
		get
		{
			return this.curNotify;
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x06000132 RID: 306 RVA: 0x00007705 File Offset: 0x00005905
	public BaseOverlap CurOverlap
	{
		get
		{
			return this.curOverlap;
		}
	}

	// Token: 0x06000133 RID: 307 RVA: 0x00007710 File Offset: 0x00005910
	private BaseScreen GetNewScreen<T>() where T : BaseScreen
	{
		string name = typeof(T).Name;
		GameObject uiprefabs = this.GetUIPrefabs(UIType.Screen, name);
		if (uiprefabs == null || !uiprefabs.GetComponent<BaseScreen>())
		{
			throw new MissingReferenceException("Can not found" + name + "scenn. !!!");
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(uiprefabs);
		gameObject.transform.SetParent(this.cScreen.transform);
		gameObject.transform.localScale = Vector3.one;
		gameObject.transform.localPosition = Vector3.zero;
		BaseScreen component = gameObject.GetComponent<BaseScreen>();
		component.Init();
		return component;
	}

	// Token: 0x06000134 RID: 308 RVA: 0x000077AC File Offset: 0x000059AC
	public void HideAllScreens()
	{
		foreach (KeyValuePair<string, BaseScreen> keyValuePair in this.screens)
		{
			BaseScreen value = keyValuePair.Value;
			if (!(value == null) && !value.IsHide)
			{
				value.Hide();
				if (this.screens.Count <= 0)
				{
					break;
				}
			}
		}
	}

	// Token: 0x06000135 RID: 309 RVA: 0x0000782C File Offset: 0x00005A2C
	public T GetExistScreen<T>() where T : BaseScreen
	{
		string name = typeof(T).Name;
		if (this.screens.ContainsKey(name))
		{
			return this.screens[name] as T;
		}
		return default(T);
	}

	// Token: 0x06000136 RID: 310 RVA: 0x00007878 File Offset: 0x00005A78
	private void RemoveScreen(string v)
	{
		for (int i = 0; i < this.rmScreens.Count; i++)
		{
			if (this.rmScreens[i].Equals(v) && this.screens.ContainsKey(v))
			{
				UnityEngine.Object.Destroy(this.screens[v].gameObject);
				this.screens.Remove(v);
				Resources.UnloadUnusedAssets();
				GC.Collect();
			}
		}
	}

	// Token: 0x06000137 RID: 311 RVA: 0x000078EC File Offset: 0x00005AEC
	public void ShowScreen<T>(object data = null, bool forceShowData = false) where T : BaseScreen
	{
		string name = typeof(T).Name;
		BaseScreen baseScreen = null;
		if (this.curScreen != null)
		{
			string name2 = this.curScreen.GetType().Name;
			if (name2.Equals(name))
			{
				baseScreen = this.curScreen;
			}
			else
			{
				this.rmScreens.Add(name2);
				this.RemoveScreen(name2);
			}
		}
		if (baseScreen == null)
		{
			if (!this.screens.ContainsKey(name))
			{
				BaseScreen newScreen = this.GetNewScreen<T>();
				if (newScreen != null)
				{
					this.screens.Add(name, newScreen);
				}
			}
			if (this.screens.ContainsKey(name))
			{
				baseScreen = this.screens[name];
			}
		}
		bool flag = false;
		if (baseScreen != null)
		{
			if (forceShowData)
			{
				flag = true;
			}
			else if (baseScreen.IsHide)
			{
				flag = true;
			}
		}
		if (flag)
		{
			this.curScreen = baseScreen;
			baseScreen.transform.SetAsFirstSibling();
			baseScreen.Show(data);
		}
	}

	// Token: 0x06000138 RID: 312 RVA: 0x000079DC File Offset: 0x00005BDC
	private BasePopup GetNewPopup<T>() where T : BasePopup
	{
		string name = typeof(T).Name;
		GameObject uiprefabs = this.GetUIPrefabs(UIType.Popup, name);
		if (uiprefabs == null || !uiprefabs.GetComponent<BasePopup>())
		{
			throw new MissingReferenceException("Can not found" + name + "scenn. !!!");
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(uiprefabs);
		gameObject.transform.SetParent(this.cPopup.transform);
		gameObject.transform.localScale = Vector3.one;
		gameObject.transform.localPosition = Vector3.zero;
		BasePopup component = gameObject.GetComponent<BasePopup>();
		component.Init();
		return component;
	}

	// Token: 0x06000139 RID: 313 RVA: 0x00007A78 File Offset: 0x00005C78
	private void RemovePopup(string v)
	{
		for (int i = 0; i < this.rmPopups.Count; i++)
		{
			if (this.rmPopups[i].Equals(v) && this.popups.ContainsKey(v))
			{
				UnityEngine.Object.Destroy(this.popups[v].gameObject);
				this.popups.Remove(v);
				rmPopups.RemoveAt(i);
				Resources.UnloadUnusedAssets();
				GC.Collect();
			}
		}
	}

	// Token: 0x0600013A RID: 314 RVA: 0x00007AEC File Offset: 0x00005CEC
	public T GetExistPopup<T>() where T : BasePopup
	{
		string name = typeof(T).Name;
		if (this.popups.ContainsKey(name))
		{
			return this.popups[name] as T;
		}
		return default(T);
	}

	// Token: 0x0600013B RID: 315 RVA: 0x00007B38 File Offset: 0x00005D38
	public void ShowPopup<T>(object data = null, bool forceShowData = false) where T : BasePopup
	{
		string name = typeof(T).Name;
		BasePopup basePopup = null;
		if (this.curPopup != null)
		{
			string name2 = this.curPopup.GetType().Name;
			if (name2.Equals(name))
			{
				basePopup = this.curPopup;
			}
			else
			{
				rmPopups.Add(name2);
				this.RemovePopup(name2);
			}
		}
		if (basePopup == null)
		{
			if (!this.popups.ContainsKey(name))
			{
				BasePopup newPopup = this.GetNewPopup<T>();
				if (newPopup != null)
				{
					this.popups.Add(name, newPopup);
				}
			}
			if (this.popups.ContainsKey(name))
			{
				basePopup = this.popups[name];
			}
		}
		bool flag = false;
		if (basePopup != null)
		{
			if (forceShowData)
			{
				flag = true;
			}
			else if (basePopup.IsHide)
			{
				flag = true;
			}
		}
		if (flag)
		{
			this.curPopup = basePopup;
			basePopup.transform.SetAsFirstSibling();
			basePopup.Show(data);
		}
	}

	// Token: 0x0600013C RID: 316 RVA: 0x00007C1C File Offset: 0x00005E1C
	public void HideAllPopups()
	{
		foreach (KeyValuePair<string, BasePopup> keyValuePair in this.popups)
		{
			BasePopup value = keyValuePair.Value;
			if (!(value == null) && !value.IsHide)
			{
				value.Hide();
				if (this.popups.Count <= 0)
				{
					break;
				}
			}
		}
	}

	// Token: 0x0600013D RID: 317 RVA: 0x00007C9C File Offset: 0x00005E9C
	private BaseNotify GetNewNotify<T>()
	{
		string name = typeof(T).Name;
		GameObject uiprefabs = this.GetUIPrefabs(UIType.Notify, name);
		if (uiprefabs == null || !uiprefabs.GetComponent<BaseNotify>())
		{
			throw new MissingReferenceException("Can not found" + name + "scenn. !!!");
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(uiprefabs);
		gameObject.transform.SetParent(this.cNotify.transform);
		gameObject.transform.localScale = Vector3.one;
		gameObject.transform.localPosition = Vector3.zero;
		BaseNotify component = gameObject.GetComponent<BaseNotify>();
		component.Init();
		return component;
	}

	// Token: 0x0600013E RID: 318 RVA: 0x00007D38 File Offset: 0x00005F38
	public void HideAllNotifies()
	{
		foreach (KeyValuePair<string, BaseNotify> keyValuePair in this.notifies)
		{
			BaseNotify value = keyValuePair.Value;
			if (!(value == null) && !value.IsHide)
			{
				value.Hide();
				if (this.notifies.Count <= 0)
				{
					break;
				}
			}
		}
	}

	// Token: 0x0600013F RID: 319 RVA: 0x00007DB8 File Offset: 0x00005FB8
	private void RemoveNotify(string v)
	{
		for (int i = 0; i < this.rmNotifies.Count; i++)
		{
			if (this.rmNotifies[i].Equals(v) && this.notifies.ContainsKey(v))
			{
				UnityEngine.Object.Destroy(this.notifies[v].gameObject);
				this.notifies.Remove(v);
				rmNotifies.RemoveAt(i);
				Resources.UnloadUnusedAssets();
				GC.Collect();
			}
		}
	}

	// Token: 0x06000140 RID: 320 RVA: 0x00007E2C File Offset: 0x0000602C
	public T GetExistNotify<T>() where T : BaseNotify
	{
		string name = typeof(T).Name;
		if (this.notifies.ContainsKey(name))
		{
			return this.notifies[name] as T;
		}
		return default(T);
	}

	// Token: 0x06000141 RID: 321 RVA: 0x00007E78 File Offset: 0x00006078
	public void ShowNotify<T>(object data = null, bool forceShowData = false) where T : BaseNotify
	{
		string name = typeof(T).Name;
		BaseNotify baseNotify = null;
		if (this.curNotify != null)
		{
			string name2 = this.curNotify.GetType().Name;
			if (name2.Equals(name))
			{
				baseNotify = this.curNotify;
			}
			else
			{
				rmNotifies.Add(name2);
				this.RemoveNotify(name2);
			}
		}
		if (baseNotify == null)
		{
			if (!this.notifies.ContainsKey(name))
			{
				BaseNotify newNotify = this.GetNewNotify<T>();
				if (newNotify != null)
				{
					this.notifies.Add(name, newNotify);
				}
			}
			if (this.notifies.ContainsKey(name))
			{
				baseNotify = this.notifies[name];
			}
		}
		bool flag = false;
		if (baseNotify != null)
		{
			if (forceShowData)
			{
				flag = true;
			}
			else if (baseNotify.IsHide)
			{
				flag = true;
			}
		}
		if (flag)
		{
			this.curNotify = baseNotify;
			baseNotify.transform.SetAsFirstSibling();
			baseNotify.Show(data);
		}
	}

	// Token: 0x06000142 RID: 322 RVA: 0x00007F5C File Offset: 0x0000615C
	private BaseOverlap GetNewOverLap<T>() where T : BaseOverlap
	{
		string name = typeof(T).Name;
		GameObject uiprefabs = this.GetUIPrefabs(UIType.Overlap, name);
		if (uiprefabs == null || !uiprefabs.GetComponent<BaseOverlap>())
		{
			throw new MissingReferenceException("Can not found" + name + "scenn. !!!");
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(uiprefabs);
		gameObject.transform.SetParent(this.cOverlap.transform);
		gameObject.transform.localScale = Vector3.one;
		gameObject.transform.localPosition = Vector3.zero;
		BaseOverlap component = gameObject.GetComponent<BaseOverlap>();
		component.Init();
		return component;
	}

	// Token: 0x06000143 RID: 323 RVA: 0x00007FF8 File Offset: 0x000061F8
	private void RemoveOverLap(string v)
	{
		for (int i = 0; i < this.rmOverlaps.Count; i++)
		{
			if (this.rmOverlaps[i].Equals(v) && this.overlaps.ContainsKey(v))
			{
				UnityEngine.Object.Destroy(this.overlaps[v]);
				this.overlaps.Remove(v);
				rmOverlaps.RemoveAt(i);
				Resources.UnloadUnusedAssets();
				GC.Collect();
			}
		}
	}

	// Token: 0x06000144 RID: 324 RVA: 0x00008068 File Offset: 0x00006268
	public T GetExistOverlap<T>() where T : BaseOverlap
	{
		string name = typeof(T).Name;
		if (this.overlaps.ContainsKey(name))
		{
			return this.overlaps[name] as T;
		}
		return default(T);
	}

	// Token: 0x06000145 RID: 325 RVA: 0x000080B4 File Offset: 0x000062B4
	public void ShowOverlap<T>(object data = null, bool forceShowData = false) where T : BaseOverlap
	{
		string name = typeof(T).Name;
		BaseOverlap baseOverlap = null;
		if (this.curOverlap != null)
		{
			string name2 = this.curOverlap.GetType().Name;
			if (name2.Equals(name))
			{
				baseOverlap = this.curOverlap;
			}
			else
			{
				rmOverlaps.Add(name2);
				this.RemoveOverLap(name2);
			}
		}
		if (baseOverlap == null)
		{
			if (!this.notifies.ContainsKey(name))
			{
				BaseOverlap newOverLap = this.GetNewOverLap<T>();
				if (newOverLap != null)
				{
					this.overlaps.Add(name, newOverLap);
				}
			}
			if (this.overlaps.ContainsKey(name))
			{
				baseOverlap = this.overlaps[name];
			}
		}
		bool flag = false;
		if (baseOverlap != null)
		{
			if (forceShowData)
			{
				flag = true;
			}
			else if (baseOverlap.IsHide)
			{
				flag = true;
			}
		}
		if (flag)
		{
			this.curOverlap = baseOverlap;
			baseOverlap.transform.SetAsFirstSibling();
			baseOverlap.Show(data);
		}
	}

	// Token: 0x06000146 RID: 326 RVA: 0x00008198 File Offset: 0x00006398
	public void HideAllOverlaps()
	{
		foreach (KeyValuePair<string, BaseOverlap> keyValuePair in this.overlaps)
		{
			BaseOverlap value = keyValuePair.Value;
			if (!(value == null) && !value.IsHide)
			{
				value.Hide();
				if (this.overlaps.Count <= 0)
				{
					break;
				}
			}
		}
	}

	// Token: 0x06000147 RID: 327 RVA: 0x00008218 File Offset: 0x00006418
	private GameObject GetUIPrefabs(UIType t, string uiName)
	{
		GameObject gameObject = null;
		string path = "";
		if (gameObject == null)
		{
			switch (t)
			{
			case UIType.Screen:
				path = "Prefabs/UI/Screen/" + uiName;
				break;
			case UIType.Popup:
				path = "Prefabs/UI/Popup/" + uiName;
				break;
			case UIType.Notify:
				path = "Prefabs/UI/Notify/" + uiName;
				break;
			case UIType.Overlap:
				path = "Prefabs/UI/Overlap/" + uiName;
				break;
			}
			gameObject = (Resources.Load(path) as GameObject);
		}
		return gameObject;
	}

	// Token: 0x0400015B RID: 347
	public GameObject cScreen;

	// Token: 0x0400015C RID: 348
	public GameObject cPopup;

	// Token: 0x0400015D RID: 349
	public GameObject cNotify;

	// Token: 0x0400015E RID: 350
	public GameObject cOverlap;

	// Token: 0x0400015F RID: 351
	private Dictionary<string, BaseScreen> screens = new Dictionary<string, BaseScreen>();

	// Token: 0x04000160 RID: 352
	private Dictionary<string, BasePopup> popups = new Dictionary<string, BasePopup>();

	// Token: 0x04000161 RID: 353
	private Dictionary<string, BaseOverlap> overlaps = new Dictionary<string, BaseOverlap>();

	// Token: 0x04000162 RID: 354
	private Dictionary<string, BaseNotify> notifies = new Dictionary<string, BaseNotify>();

	// Token: 0x04000163 RID: 355
	private BaseScreen curScreen;

	// Token: 0x04000164 RID: 356
	private BasePopup curPopup;

	// Token: 0x04000165 RID: 357
	private BaseNotify curNotify;

	// Token: 0x04000166 RID: 358
	private BaseOverlap curOverlap;

	// Token: 0x04000167 RID: 359
	private const string SCREEN_RESOURCES_PATH = "Prefabs/UI/Screen/";

	// Token: 0x04000168 RID: 360
	private const string POPUP_RESOURCES_PATH = "Prefabs/UI/Popup/";

	// Token: 0x04000169 RID: 361
	private const string NOTIFY_RESOURCES_PATH = "Prefabs/UI/Notify/";

	// Token: 0x0400016A RID: 362
	private const string OVERLAP_RESOURCES_PATH = "Prefabs/UI/Overlap/";

	// Token: 0x0400016B RID: 363
	private List<string> rmScreens = new List<string>();

	// Token: 0x0400016C RID: 364
	private List<string> rmPopups = new List<string>();

	// Token: 0x0400016D RID: 365
	private List<string> rmNotifies = new List<string>();

	// Token: 0x0400016E RID: 366
	private List<string> rmOverlaps = new List<string>();
}
