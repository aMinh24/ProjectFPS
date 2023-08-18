using System;
using System.Collections.Generic;

// Token: 0x0200004F RID: 79
public class ListenerManager : BaseManager<ListenerManager>
{
	// Token: 0x0600011D RID: 285 RVA: 0x000070D7 File Offset: 0x000052D7
	public void BroadCast(ListenType listenType, object value = null)
	{
		if (this.listeners.ContainsKey(listenType) && this.listeners[listenType] != null)
		{
			this.listeners[listenType].BroadCast(value);
		}
	}

	// Token: 0x0600011E RID: 286 RVA: 0x00007108 File Offset: 0x00005308
	public void Register(ListenType listenType, Action<object> action)
	{
		if (!this.listeners.ContainsKey(listenType))
		{
			this.listeners.Add(listenType, new ListenerGroup());
		}
		if (this.listeners.ContainsKey(listenType))
		{
			this.listeners[listenType].Attach(action);
		}
	}

	// Token: 0x0600011F RID: 287 RVA: 0x00007154 File Offset: 0x00005354
	public void Unregister(ListenType listenType, Action<object> action)
	{
		if (this.listeners.ContainsKey(listenType))
		{
			this.listeners[listenType].Detach(action);
		}
	}

	// Token: 0x06000120 RID: 288 RVA: 0x00007178 File Offset: 0x00005378
	public void UnregisterAll(Action<object> action)
	{
		foreach (ListenType listenType in this.listeners.Keys)
		{
			this.Unregister(listenType, action);
		}
	}

	// Token: 0x04000152 RID: 338
	public Dictionary<ListenType, ListenerGroup> listeners = new Dictionary<ListenType, ListenerGroup>();
}
